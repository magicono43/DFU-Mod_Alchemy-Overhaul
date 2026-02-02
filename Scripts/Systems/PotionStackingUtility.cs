using UnityEngine;
using AlchemyOverhaul.Data.Runtime;

namespace AlchemyOverhaul.Systems
{
    public static class PotionStackingUtility
    {
        public static bool AreStackable(PotionData a, PotionData b)
        {
            if (a.IsSpoiled != b.IsSpoiled) return false;

            if (!Mathf.Approximately(a.Toxicity, b.Toxicity)) return false;
            if (!Mathf.Approximately(a.Palatability, b.Palatability)) return false;
            if (!Mathf.Approximately(a.SpoilageRate, b.SpoilageRate)) return false;

            if (a.Effects.Count != b.Effects.Count) return false;

            for (int i = 0; i < a.Effects.Count; i++)
            {
                if (!a.Effects[i].Equals(b.Effects[i]))
                    return false;
            }

            return true;
        }
    }
}
