using UnityEngine;
using DaggerfallWorkshop.Game;
using DaggerfallWorkshop;
using DaggerfallWorkshop.Game.Items;
using DaggerfallWorkshop.Utility;
using DaggerfallWorkshop.Game.Utility.ModSupport;
using System;
using System.Collections.Generic;
using DaggerfallWorkshop.Game.Serialization;

namespace AlchemyOverhaul
{
    [FullSerializer.fsObject("v1")]
    public class AlchemyOverhaulSaveData : IHasModSaveData
    {
        internal class AlchemyOverhaulSaveState
        {
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