using System.Collections.Generic;
using AlchemyOverhaul.Data.Runtime;
using AlchemyOverhaul.Data.Save;
using AlchemyOverhaul.Potions;
using AlchemyOverhaul.Data.Definitions;

namespace AlchemyOverhaul.Systems
{
    public static class PotionRegistry
    {
        // Runtime cache
        private static readonly Dictionary<ulong, PotionData> potionsById = new Dictionary<ulong, PotionData>();

        // ===== Creation =====

        public static PotionData CreatePotion(string potionId)
        {
            PotionDefinition definition = PotionResolver.ResolveById(potionId);
            if (definition == null)
                return null;

            return PotionDataBuilder.BuildFromDefinition(definition);
        }

        // ===== Registration =====

        public static void RegisterPotion(ulong potionId, PotionData data)
        {
            potionsById[potionId] = data;
        }

        public static bool TryGetPotion(ulong potionId, out PotionData data)
        {
            return potionsById.TryGetValue(potionId, out data);
        }

        public static void UnregisterPotion(ulong potionId)
        {
            potionsById.Remove(potionId);
        }

        // ===== Enumeration =====

        public static IEnumerable<KeyValuePair<ulong, PotionData>> AllPotions()
        {
            return potionsById;
        }

        // ===== Save Integration =====

        public static Dictionary<ulong, PotionDataSave> ToSaveData()
        {
            var dict = new Dictionary<ulong, PotionDataSave>();

            foreach (var kvp in potionsById)
                dict[kvp.Key] = PotionDataConverter.ToSave(kvp.Value);

            return dict;
        }

        public static void LoadFromSave(Dictionary<ulong, PotionDataSave> saveData)
        {
            potionsById.Clear();

            if (saveData == null)
                return;

            foreach (var kvp in saveData)
            {
                ulong potionId = kvp.Key;
                PotionDataSave save = kvp.Value;

                PotionData runtime = PotionDataConverter.FromSave(save);
                if (runtime == null)
                    continue;

                // Definition must exist
                if (string.IsNullOrEmpty(runtime.PotionDefinitionId))
                    continue;

                PotionDefinition definition =
                    PotionResolver.ResolveById(runtime.PotionDefinitionId);

                if (definition == null)
                    continue;

                // Rebind definition-derived data
                runtime.ApplyDefinition(definition);

                // Final validation before registration
                RegisterPotion(potionId, runtime);
            }
        }

        // ===== Helpers =====

        public static void Clear()
        {
            potionsById.Clear();
        }
    }
}