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

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.Climbing),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.Climbing), "Climbing")
                {
                    Kind = PotionEffectKey.Climbing,
                    IsHostile = false,
                    UsesDuration = true,
                    UsesMagnitude = false,
                    BaseDurationSeconds = 10f,
                    SkillScalingFactor = 0.3f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.FireResist),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.FireResist), "Fire Resistance")
                {
                    Kind = PotionEffectKey.FireResist,
                    IsHostile = false,
                    UsesDuration = true,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 7f,
                    SkillScalingFactor = 0.5f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.FrostResist),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.FrostResist), "Frost Resistance")
                {
                    Kind = PotionEffectKey.FrostResist,
                    IsHostile = false,
                    UsesDuration = true,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 7f,
                    SkillScalingFactor = 0.5f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.PoisonResist),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.PoisonResist), "Poison Resistance")
                {
                    Kind = PotionEffectKey.PoisonResist,
                    IsHostile = false,
                    UsesDuration = true,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 7f,
                    SkillScalingFactor = 0.5f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.ShockResist),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.ShockResist), "Shock Resistance")
                {
                    Kind = PotionEffectKey.ShockResist,
                    IsHostile = false,
                    UsesDuration = true,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 7f,
                    SkillScalingFactor = 0.5f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.MagicResist),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.MagicResist), "Magic Resistance")
                {
                    Kind = PotionEffectKey.MagicResist,
                    IsHostile = false,
                    UsesDuration = true,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 7f,
                    SkillScalingFactor = 0.5f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.RestoreHealth),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.RestoreHealth), "Restore Health")
                {
                    Kind = PotionEffectKey.RestoreHealth,
                    IsHostile = false,
                    UsesDuration = false,
                    UsesMagnitude = true,
                    SkillScalingFactor = 0.5f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.RegenerateHealth),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.RegenerateHealth), "Regenerate Health")
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

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.Jumping),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.Jumping), "Jumping")
                {
                    Kind = PotionEffectKey.Jumping,
                    IsHostile = false,
                    UsesDuration = true,
                    UsesMagnitude = false,
                    BaseDurationSeconds = 8f,
                    SkillScalingFactor = 0.4f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.Slowfall),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.Slowfall), "Slowfall")
                {
                    Kind = PotionEffectKey.Slowfall,
                    IsHostile = false,
                    UsesDuration = true,
                    UsesMagnitude = false,
                    BaseDurationSeconds = 5f,
                    SkillScalingFactor = 0.6f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.InvisibilityTrue),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.InvisibilityTrue), "Invisibility True")
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