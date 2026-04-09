using UnityEngine;
using DaggerfallWorkshop.Game;
using DaggerfallWorkshop;
using DaggerfallWorkshop.Game.Utility.ModSupport;
using System;
using System.Collections.Generic;
using AlchemyOverhaul.Data.Save;
using AlchemyOverhaul.Systems;
using AlchemyOverhaul.Data.Runtime;
using AlchemyOverhaul.Ingredients.Knowledge;
using AlchemyOverhaul.Player.Skills;

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
            
            // IngredientId -> learned flags
            public Dictionary<string, IngredientEffectKnowledge> IngredientKnowledge = new Dictionary<string, IngredientEffectKnowledge>();

            // ===== Alchemy Skill =====
            public int AlchemyLevel = 1;
            public int AlchemyExperience = 0;
            public int AlchemyTotalExperience = 0; // optional but useful
        }

        internal AlchemyOverhaulSaveState state;

        public Type SaveDataType => typeof(AlchemyOverhaulSaveState);

        public AlchemyOverhaulSaveData()
        {
            state = new AlchemyOverhaulSaveState
            {
                Version = CURRENT_SAVE_VERSION,
                AlchemyLevel = 1,
                AlchemyExperience = 0,
                AlchemyTotalExperience = 0
            };
        }

        public object NewSaveData()
        {
            return new AlchemyOverhaulSaveState
            {
                Version = CURRENT_SAVE_VERSION,
                AlchemyLevel = 1,
                AlchemyExperience = 0,
                AlchemyTotalExperience = 0
            };
        }

        public object GetSaveData()
        {
            // Always regenerate from runtime truth
            SyncPotionSaveData();
            state.IngredientKnowledge = IngredientKnowledgeSystem.GetSaveData();

            // ===== Save Alchemy Skill =====
            state.AlchemyLevel = AlchemySkill.Level;
            state.AlchemyExperience = AlchemySkill.Experience;
            state.AlchemyTotalExperience = AlchemySkill.TotalExperience;

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
            IngredientKnowledgeSystem.LoadFromSave(state.IngredientKnowledge);

            // ===== Load Alchemy Skill =====
            AlchemySkill.LoadFromSave(
                state.AlchemyLevel,
                state.AlchemyExperience,
                state.AlchemyTotalExperience
            );

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