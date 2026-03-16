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
                case PotionEffectKey.Paralysis:       return "Paralyze";
                case PotionEffectKey.Jumping:         return "Jumping";
                case PotionEffectKey.Slowfall:        return "Slowfall";
                case PotionEffectKey.InvisibilityTrue:return "Invisibility-True";
                case PotionEffectKey.FortifySpeed:    return "Fortify-Speed";
                // add other mappings here
                default: throw new System.ArgumentOutOfRangeException(nameof(key), key, "Unknown PotionEffectKey");
            }
        }
    }
}
