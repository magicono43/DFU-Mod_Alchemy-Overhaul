using AlchemyOverhaul.Data.Enums;
using AlchemyOverhaul.Data.Runtime;
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

            dict.Add("regenerate_health",
                new EffectDefinition("regenerate_health", "Regenerate Health")
                {
                    Kind = PotionEffectKey.RegenerateHealth,
                    IsHostile = false,
                    UsesDuration = true,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 3f,
                    SkillScalingFactor = 0.7f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.Paralysis),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.Paralysis), "Paralysis")
                {
                    Kind = PotionEffectKey.Paralysis,
                    IsHostile = true,
                    UsesDuration = true,
                    UsesMagnitude = false,
                    BaseDurationSeconds = 5f,
                    SkillScalingFactor = 0.2f,
                });

            dict.Add("jumping",
                new EffectDefinition("jumping", "Jumping")
                {
                    Kind = PotionEffectKey.Jumping,
                    IsHostile = false,
                    UsesDuration = true,
                    UsesMagnitude = false,
                    BaseDurationSeconds = 8f,
                    SkillScalingFactor = 0.4f,
                });

            dict.Add("slowfall",
                new EffectDefinition("slowfall", "Slowfall")
                {
                    Kind = PotionEffectKey.Slowfall,
                    IsHostile = false,
                    UsesDuration = true,
                    UsesMagnitude = false,
                    BaseDurationSeconds = 5f,
                    SkillScalingFactor = 0.6f,
                });

            dict.Add("invisibility_true",
                new EffectDefinition("invisibility_true", "Invisibility True")
                {
                    Kind = PotionEffectKey.InvisibilityTrue,
                    IsHostile = false,
                    UsesDuration = true,
                    UsesMagnitude = false,
                    BaseDurationSeconds = 7f,
                    SkillScalingFactor = 0.2f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.FortifySpeed),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.FortifySpeed), "Fortify Speed")
                {
                    Kind = PotionEffectKey.FortifySpeed,
                    IsHostile = false,
                    UsesDuration = true,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 4f,
                    SkillScalingFactor = 0.8f,
                });

            return dict;
        }

        public static bool TryGet(string effectId, out EffectDefinition def)
        {
            return effects.TryGetValue(effectId, out def);
        }
    }
}