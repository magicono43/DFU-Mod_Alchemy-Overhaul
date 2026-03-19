using AlchemyOverhaul.Data.Enums;

namespace AlchemyOverhaul.Data.Runtime
{
    public static class PotionEffectKeyMapper
    {
        public static string ToDFUEffectKey(PotionEffectKey key)
        {
            switch (key)
            {
                case PotionEffectKey.Climbing:        return "Climbing";
                case PotionEffectKey.FireResist:      return "ElementalResistance-Fire";
                case PotionEffectKey.FrostResist:     return "ElementalResistance-Frost";
                case PotionEffectKey.PoisonResist:    return "ElementalResistance-Poison";
                case PotionEffectKey.ShockResist:     return "ElementalResistance-Shock";
                case PotionEffectKey.MagicResist:     return "ElementalResistance-Magicka";
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
