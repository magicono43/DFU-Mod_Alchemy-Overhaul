using AlchemyOverhaul.Data.Enums;
using System.Collections.Generic;

namespace AlchemyOverhaul.Effects
{
    public static class EffectDatabase
    {
        private static readonly Dictionary<string, EffectDefinition> effects
            = BuildDatabase();

        private static Dictionary<string, EffectDefinition> BuildDatabase()
        {
            var dict = new Dictionary<string, EffectDefinition>();

            dict.Add("restore_health",
                new EffectDefinition("restore_health", "Restore Health")
                {
                    Kind = PotionEffectKey.RestoreHealth,
                    IsHostile = false,
                    UsesDuration = false,
                    UsesMagnitude = true,
                    SkillScalingFactor = 0.5f,
                });

            dict.Add("paralysis",
                new EffectDefinition("paralysis", "Paralysis")
                {
                    Kind = PotionEffectKey.Paralysis,
                    IsHostile = true,
                    UsesDuration = true,
                    UsesMagnitude = false,
                    BaseDurationSeconds = 5f,
                    SkillScalingFactor = 0.2f,
                });

            return dict;
        }

        public static bool TryGet(string effectId, out EffectDefinition def)
        {
            return effects.TryGetValue(effectId, out def);
        }
    }
}