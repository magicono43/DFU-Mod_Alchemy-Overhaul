using UnityEngine;
using DaggerfallWorkshop.Game;
using DaggerfallWorkshop;
using DaggerfallWorkshop.Game.Items;
using DaggerfallWorkshop.Utility;
using DaggerfallWorkshop.Game.Utility.ModSupport;
using System;
using System.Collections.Generic;
using DaggerfallWorkshop.Game.Serialization;
using DaggerfallWorkshop.Game.Entity;
using AlchemyOverhaul.Data.Save;
using AlchemyOverhaul.Systems;
using AlchemyOverhaul.Data.Runtime;

namespace AlchemyOverhaul
{
    [FullSerializer.fsObject("v1")]
    public class AlchemyOverhaulSaveData : IHasModSaveData
    {
        public const int CURRENT_SAVE_VERSION = 1;

        internal class AlchemyOverhaulSaveState
        {
            public int Version;
            public ulong TimeOfPreviousStalePotionRecordCleanup = 0;
            
            // UID -> versioned potion save data
            public Dictionary<ulong, PotionDataSave> PotionItems = new Dictionary<ulong, PotionDataSave>();
        }

        public Type SaveDataType
        {
            get { return typeof(AlchemyOverhaulSaveState); }
        }

        internal AlchemyOverhaulSaveState state;

        public AlchemyOverhaulSaveData()
        {
            state = new AlchemyOverhaulSaveState();
        }

        public object NewSaveData()
        {
            return new AlchemyOverhaulSaveState
            {
                Version = CURRENT_SAVE_VERSION
            };
        }

        public object GetSaveData()
        {
            return state;
        }

        public void RestoreSaveData(object saveData)
        {
            state = saveData as AlchemyOverhaulSaveState;

            if (state == null)
            {
                state = new AlchemyOverhaulSaveState
                {
                    Version = CURRENT_SAVE_VERSION
                };
                return;
            }

            if (state.Version < CURRENT_SAVE_VERSION)
            {
                UpgradeSaveData(state);
            }
            
            PotionRegistry.LoadFromSave(state.PotionItems);

            // Immediately reconcile reality
            PotionCleanupSystem.RunCleanup(GameManager.Instance.PlayerEntity);
        }

        private void UpgradeSaveData(AlchemyOverhaulSaveState state)
        {
            while (state.Version < CURRENT_SAVE_VERSION)
            {
                switch (state.Version)
                {
                    case 1:
                        UpgradeFromV1ToV2(state);
                        break;

                    default:
                        Debug.LogWarning(
                            $"[AO] Unknown save version {state.Version}, forcing reset.");
                        state.Version = CURRENT_SAVE_VERSION;
                        break;
                }
            }
        }

        private void UpgradeFromV1ToV2(AlchemyOverhaulSaveState state)
        {
            state.TimeOfPreviousStalePotionRecordCleanup = DaggerfallUnity.Instance.WorldTime.DaggerfallDateTime.ToSeconds();
            state.Version = 2;
        }

        // ---- Runtime helpers ----

        public void RemoveStalePotionRecords_OnNewDay()
        {
            if (state.PotionItems.Count == 0)
                return;

            ulong currentTime = DaggerfallUnity.Instance.WorldTime.DaggerfallDateTime.ToSeconds();

            if (currentTime - state.TimeOfPreviousStalePotionRecordCleanup < 259200)
                return;

            // Cleanup happens here
            HashSet<ulong> validUids = new HashSet<ulong>();

            PlayerEntity player = GameManager.Instance.PlayerEntity;

            // Player inventory
            ItemCollection items = player.Items;

            for (int i = 0; i < items.Count; i++)
            {
                DaggerfallUnityItem item = items.GetItem(i);
                validUids.Add(item.UID);
            }

            // Player Wagon
            ItemCollection wagonItems = player.WagonItems;

            for (int i = 0; i < wagonItems.Count; i++)
            {
                DaggerfallUnityItem item = wagonItems.GetItem(i);
                validUids.Add(item.UID);
            }

            // Make list of loot-piles currently in the interior "scene."
            DaggerfallLoot[] lootPiles = UnityEngine.Object.FindObjectsOfType<DaggerfallLoot>();
            for (int i = 0; i < lootPiles.Length; i++)
            {
                ItemCollection lootPileItems = lootPiles[i].Items;
                for (int f = 0; f < lootPileItems.Count; f++)
                {
                    DaggerfallUnityItem item = lootPileItems.GetItem(f);
                    if (item == null) { continue; }
                    validUids.Add(item.UID);
                }
            }

            List<ulong> stalePotionUIDs = new List<ulong>();
            foreach (var kvp in state.PotionItems)
            {
                ulong uid = kvp.Key;
                if (!validUids.Contains(uid))
                {
                    stalePotionUIDs.Add(uid);
                }
            }

            foreach (ulong uid in stalePotionUIDs)
            {
                state.PotionItems.Remove(uid);
                PotionRegistry.UnregisterPotion(uid); // runtime sync
                Debug.Log($"[AO] Removed stale potion record UID {uid}");
            }

            state.TimeOfPreviousStalePotionRecordCleanup = currentTime;
        }
        
        public bool TryGetPotionData(ulong uid, out PotionData data)
        {
            return PotionRegistry.TryGetPotion(uid, out data);
        }
        
        public void AddPotion(ulong uid, PotionData data)
        {
            PotionRegistry.RegisterPotion(uid, data);
            state.PotionItems[uid] = PotionDataConverter.ToSave(data);
        }
        
        public void RemovePotion(ulong uid)
        {
            PotionRegistry.UnregisterPotion(uid);
            state.PotionItems.Remove(uid);
        }
        
        public void SyncPotionSaveData()
        {
            state.PotionItems = PotionRegistry.ToSaveData();
        }
    }
}