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

namespace AlchemyOverhaul
{
    [FullSerializer.fsObject("v1")]
    public class AlchemyOverhaulSaveData : IHasModSaveData
    {
        internal class AlchemyOverhaulSaveState
        {
            public ulong TimeOfPreviousStalePotionRecordCleanup = 0;
            public Dictionary<ulong, PotionItemRecord> PotionItems = new Dictionary<ulong, PotionItemRecord>();
        }

        internal class PotionItemRecord
        {
            public string PotionId;
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
            state = new AlchemyOverhaulSaveState();
            return state;
        }

        public object GetSaveData()
        {
            return state;
        }

        public void RestoreSaveData(object saveData)
        {
            if (saveData is AlchemyOverhaulSaveState loaded)
                state = loaded;
            else
                state = new AlchemyOverhaulSaveState();
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
                Debug.Log($"[AO] Removed stale potion record UID {uid}");
            }

            state.TimeOfPreviousStalePotionRecordCleanup = currentTime;
        }

        public bool TryGetPotionRecord(ulong uid, out string potionId)
        {
            potionId = null;

            if (!state.PotionItems.TryGetValue(uid, out PotionItemRecord record))
                return false;

            potionId = record.PotionId;
            return true;
        }

        public void AddPotionRecord(ulong uid, string potionId)
        {
            state.PotionItems[uid] = new PotionItemRecord
            {
                PotionId = potionId
            };
        }

        public void RemovePotionRecord(ulong uid)
        {
            state.PotionItems.Remove(uid);
        }
    }
}