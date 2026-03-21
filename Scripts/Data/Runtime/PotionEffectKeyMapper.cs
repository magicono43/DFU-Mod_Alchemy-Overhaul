using AlchemyOverhaul.Data.Enums;

namespace AlchemyOverhaul.Data.Runtime
{
    public static class PotionEffectKeyMapper
    {
        public static string ToDFUEffectKey(PotionEffectKey key)
        {
            switch (key)
            {
                // Alteration
                case PotionEffectKey.Climbing:                    return "Climbing";
                case PotionEffectKey.FireResist:                  return "ElementalResistance-Fire";
                case PotionEffectKey.FrostResist:                 return "ElementalResistance-Frost";
                case PotionEffectKey.PoisonResist:                return "ElementalResistance-Poison";
                case PotionEffectKey.ShockResist:                 return "ElementalResistance-Shock";
                case PotionEffectKey.MagicResist:                 return "ElementalResistance-Magicka";
                case PotionEffectKey.Jumping:                     return "Jumping";
                case PotionEffectKey.Paralysis:                   return "Paralyze";
                case PotionEffectKey.Slowfall:                    return "Slowfall";
                case PotionEffectKey.WaterBreathing:              return "WaterBreathing";

                // Destruction
                case PotionEffectKey.ContinuousDamageFatigue:     return "ContinuousDamage-Fatigue";
                case PotionEffectKey.ContinuousDamageHealth:      return "ContinuousDamage-Health";
                case PotionEffectKey.ContinuousDamageSpellPoints: return "ContinuousDamage-SpellPoints";
                case PotionEffectKey.DamageFatigue:               return "Damage-Fatigue";
                case PotionEffectKey.DamageHealth:                return "Damage-Health";
                case PotionEffectKey.DamageSpellPoints:           return "Damage-SpellPoints";
                case PotionEffectKey.DamageAgility:               return "Drain-Agility";
                case PotionEffectKey.DamageEndurance:             return "Drain-Endurance";
                case PotionEffectKey.DamageIntelligence:          return "Drain-Intelligence";
                case PotionEffectKey.DamageLuck:                  return "Drain-Luck";
                case PotionEffectKey.DamagePersonality:           return "Drain-Personality";
                case PotionEffectKey.DamageSpeed:                 return "Drain-Speed";
                case PotionEffectKey.DamageStrength:              return "Drain-Strength";
                case PotionEffectKey.DamageWillpower:             return "Drain-Willpower";

                // Illusion
                case PotionEffectKey.ChameleonNormal:             return "Chameleon-Normal";
                case PotionEffectKey.ChameleonTrue:               return "Chameleon-True";
                case PotionEffectKey.InvisibilityNormal:          return "Invisibility-Normal";
                case PotionEffectKey.InvisibilityTrue:            return "Invisibility-True";
                case PotionEffectKey.ShadowNormal:                return "Shadow-Normal";
                case PotionEffectKey.ShadowTrue:                  return "Shadow-True";

                // Mysticism
                case PotionEffectKey.ComprehendLanguages:         return "ComprehendLanguages";
                case PotionEffectKey.DispelMagic:                 return "Dispel-Magic";
                case PotionEffectKey.Silence:                     return "Silence";

                // Restoration
                case PotionEffectKey.CureDisease:                 return "Cure-Disease";
                case PotionEffectKey.CureParalysis:               return "Cure-Paralyzation";
                case PotionEffectKey.CurePoison:                  return "Cure-Poison";
                case PotionEffectKey.FortifyAgility:              return "Fortify-Agility";
                case PotionEffectKey.FortifyEndurance:            return "Fortify-Endurance";
                case PotionEffectKey.FortifyIntelligence:         return "Fortify-Intelligence";
                case PotionEffectKey.FortifyLuck:                 return "Fortify-Luck";
                case PotionEffectKey.FortifyPersonality:          return "Fortify-Personality";
                case PotionEffectKey.FortifySpeed:                return "Fortify-Speed";
                case PotionEffectKey.FortifyStrength:             return "Fortify-Strength";
                case PotionEffectKey.FortifyWillpower:            return "Fortify-Willpower";
                case PotionEffectKey.FreeAction:                  return "FreeAction";
                case PotionEffectKey.HealAgility:                 return "Heal-Agility";
                case PotionEffectKey.HealEndurance:               return "Heal-Endurance";
                case PotionEffectKey.RestoreFatigue:              return "Heal-Fatigue";
                case PotionEffectKey.RestoreHealth:               return "Heal-Health";
                case PotionEffectKey.HealIntelligence:            return "Heal-Intelligence";
                case PotionEffectKey.HealLuck:                    return "Heal-Luck";
                case PotionEffectKey.HealPersonality:             return "Heal-Personality";
                case PotionEffectKey.HealSpeed:                   return "Heal-Speed";
                case PotionEffectKey.RestoreSpellPoints:          return "Heal-SpellPoints";
                case PotionEffectKey.HealStrength:                return "Heal-Strength";
                case PotionEffectKey.HealWillpower:               return "Heal-Willpower";
                case PotionEffectKey.RegenerateHealth:            return "Regenerate";
                case PotionEffectKey.SpellAbsorption:             return "SpellAbsorption";

                // Thaumaturgy
                case PotionEffectKey.DetectEnemy:                 return "Detect-Enemy";
                case PotionEffectKey.DetectMagic:                 return "Detect-Magic";
                case PotionEffectKey.DetectTreasure:              return "Detect-Treasure";
                case PotionEffectKey.Levitate:                    return "Levitate";
                case PotionEffectKey.PacifyAnimal:                return "Pacify-Animal";
                case PotionEffectKey.PacifyDaedra:                return "Pacify-Daedra";
                case PotionEffectKey.PacifyHumanoid:              return "Pacify-Humanoid";
                case PotionEffectKey.PacifyUndead:                return "Pacify-Undead";
                case PotionEffectKey.SpellReflection:             return "SpellReflection";
                case PotionEffectKey.SpellResistance:             return "SpellResistance";
                case PotionEffectKey.WaterWalking:                return "WaterWalking";

                default: throw new System.ArgumentOutOfRangeException(nameof(key), key, "Unknown PotionEffectKey");
            }
        }
    }
}
