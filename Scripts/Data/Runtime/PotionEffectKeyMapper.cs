using AlchemyOverhaul.Data.Enums;

namespace AlchemyOverhaul.Data.Runtime
{
    public static class PotionEffectKeyMapper
    {
        public static string ToDFUEffectKey(PotionEffectKey key)
        {
            switch (key)
            {
                case PotionEffectKey.RestoreHealth:   return "Heal-Health";
                case PotionEffectKey.RegenerateHealth:return "Regenerate";
                // add other mappings here
                default: throw new System.ArgumentOutOfRangeException(nameof(key), key, "Unknown PotionEffectKey");
            }
        }
    }
}
