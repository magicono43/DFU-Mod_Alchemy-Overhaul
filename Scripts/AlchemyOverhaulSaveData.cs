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

        internal sealed class AlchemyOverhaulSaveState
        {
            public int Version;
            public ulong TimeOfPreviousStalePotionRecordCleanup = 0;
            
            // UID -> versioned potion save data
            public Dictionary<ulong, PotionDataSave> PotionItems = new Dictionary<ulong, PotionDataSave>();
        }

        internal AlchemyOverhaulSaveState state;

        public Type SaveDataType => typeof(AlchemyOverhaulSaveState);

        public AlchemyOverhaulSaveData()
        {
            state = new AlchemyOverhaulSaveState
            {
                Version = CURRENT_SAVE_VERSION
            };
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
            // Always regenerate from runtime truth
            SyncPotionSaveData();
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
                UpgradeSaveData(state);

            // Runtime registry is authoritative
            PotionRegistry.Clear();
            PotionRegistry.LoadFromSave(state.PotionItems);

            // Immediate reconciliation
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

        // ===== Runtime-facing helpers =====

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

        // ===== Cleanup =====

        public void RemoveStalePotionRecords_OnNewDay()
        {
            if (state.PotionItems.Count == 0)
                return;

            ulong now = DaggerfallUnity.Instance.WorldTime.DaggerfallDateTime.ToSeconds();
            if (now - state.TimeOfPreviousStalePotionRecordCleanup < 259200)
                return;

            HashSet<ulong> validUids = PotionCleanupSystem.CollectAllValidItemUids(GameManager.Instance.PlayerEntity);

            List<ulong> stale = new List<ulong>();

            foreach (ulong uid in state.PotionItems.Keys)
            {
                if (!validUids.Contains(uid))
                    stale.Add(uid);
            }

            foreach (ulong uid in stale)
            {
                state.PotionItems.Remove(uid);
                PotionRegistry.UnregisterPotion(uid);
                Debug.Log($"[AO] Removed stale potion UID {uid}");
            }

            state.TimeOfPreviousStalePotionRecordCleanup = now;
        }
    }
}