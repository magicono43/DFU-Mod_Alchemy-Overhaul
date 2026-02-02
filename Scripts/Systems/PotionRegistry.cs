using System.Collections.Generic;
using AlchemyOverhaul.Data.Runtime;
using AlchemyOverhaul.Data.Save;
using AlchemyOverhaul.Potions;

namespace AlchemyOverhaul.Systems
{
    public static class PotionRegistry
    {
        // Runtime cache
        private static readonly Dictionary<ulong, PotionData> potionsByUid = new Dictionary<ulong, PotionData>();

        // ===== Creation =====

        public static PotionData CreatePotion(string potionId)
        {
            CustomPotion definition = PotionResolver.ResolveById(potionId);
            if (definition == null)
                return null;

            return PotionDataBuilder.BuildFromDefinition(definition);
        }

        // ===== Registration =====

        public static void RegisterPotion(ulong itemUid, PotionData data)
        {
            potionsByUid[itemUid] = data;
        }

        public static bool TryGetPotion(ulong itemUid, out PotionData data)
        {
            return potionsByUid.TryGetValue(itemUid, out data);
        }

        public static void UnregisterPotion(ulong itemUid)
        {
            potionsByUid.Remove(itemUid);
        }

        // ===== Enumeration =====

        public static IEnumerable<KeyValuePair<ulong, PotionData>> AllPotions()
        {
            return potionsByUid;
        }

        // ===== Save Integration =====

        public static Dictionary<ulong, PotionDataSave> ToSaveData()
        {
            var dict = new Dictionary<ulong, PotionDataSave>();

            foreach (var kvp in potionsByUid)
                dict[kvp.Key] = PotionDataConverter.ToSave(kvp.Value);

            return dict;
        }

        public static void LoadFromSave(Dictionary<ulong, PotionDataSave> saveData)
        {
            potionsByUid.Clear();

            foreach (var kvp in saveData)
                potionsByUid[kvp.Key] = PotionDataConverter.FromSave(kvp.Value);
        }
    }
}
