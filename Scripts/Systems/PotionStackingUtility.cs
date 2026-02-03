using AlchemyOverhaul.Data.Runtime;

namespace AlchemyOverhaul.Systems
{
    public static class PotionStackingUtility
    {
        public static bool AreStackable(PotionData a, PotionData b)
        {
            // Null safety
            if (a == null || b == null)
                return false;

            // 1. Runtime state must match
            if (a.IsSpoiled != b.IsSpoiled)
                return false;

            // 2. Definition identity must match
            if (a.PotionDefinitionId != b.PotionDefinitionId)
                return false;

            // 3. Effect payload must match exactly
            if (a.Effects.Count != b.Effects.Count)
                return false;

            for (int i = 0; i < a.Effects.Count; i++)
            {
                if (!a.Effects[i].Equals(b.Effects[i]))
                    return false;
            }

            return true;
        }
    }
}