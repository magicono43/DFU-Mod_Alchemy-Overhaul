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

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.WaterBreathing),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.WaterBreathing), "Water Breathing")
                {
                    Kind = PotionEffectKey.WaterBreathing,
                    IsHostile = false,
                    UsesDuration = true,
                    UsesMagnitude = false,
                    BaseDurationSeconds = 7f,
                    SkillScalingFactor = 0.6f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.ContinuousDamageFatigue),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.ContinuousDamageFatigue), "Continuous Damage Fatigue")
                {
                    Kind = PotionEffectKey.ContinuousDamageFatigue,
                    IsHostile = true,
                    UsesDuration = true,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 7f,
                    SkillScalingFactor = 0.6f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.ContinuousDamageHealth),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.ContinuousDamageHealth), "Continuous Damage Health")
                {
                    Kind = PotionEffectKey.ContinuousDamageHealth,
                    IsHostile = true,
                    UsesDuration = true,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 7f,
                    SkillScalingFactor = 0.6f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.ContinuousDamageSpellPoints),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.ContinuousDamageSpellPoints), "Continuous Damage Spell Points")
                {
                    Kind = PotionEffectKey.ContinuousDamageSpellPoints,
                    IsHostile = true,
                    UsesDuration = true,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 7f,
                    SkillScalingFactor = 0.6f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.DamageFatigue),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.DamageFatigue), "Damage Fatigue")
                {
                    Kind = PotionEffectKey.DamageFatigue,
                    IsHostile = true,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 7f,
                    SkillScalingFactor = 0.6f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.DamageHealth),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.DamageHealth), "Damage Health")
                {
                    Kind = PotionEffectKey.DamageHealth,
                    IsHostile = true,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 7f,
                    SkillScalingFactor = 0.6f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.DamageSpellPoints),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.DamageSpellPoints), "Damage Spell Points")
                {
                    Kind = PotionEffectKey.DamageSpellPoints,
                    IsHostile = true,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 7f,
                    SkillScalingFactor = 0.6f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.DamageAgility),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.DamageAgility), "Damage Agility")
                {
                    Kind = PotionEffectKey.DamageAgility,
                    IsHostile = true,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 7f,
                    SkillScalingFactor = 0.6f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.DamageEndurance),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.DamageEndurance), "Damage Endurance")
                {
                    Kind = PotionEffectKey.DamageEndurance,
                    IsHostile = true,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 7f,
                    SkillScalingFactor = 0.6f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.DamageIntelligence),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.DamageIntelligence), "Damage Intelligence")
                {
                    Kind = PotionEffectKey.DamageIntelligence,
                    IsHostile = true,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 7f,
                    SkillScalingFactor = 0.6f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.DamageLuck),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.DamageLuck), "Damage Luck")
                {
                    Kind = PotionEffectKey.DamageLuck,
                    IsHostile = true,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 7f,
                    SkillScalingFactor = 0.6f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.DamagePersonality),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.DamagePersonality), "Damage Personality")
                {
                    Kind = PotionEffectKey.DamagePersonality,
                    IsHostile = true,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 7f,
                    SkillScalingFactor = 0.6f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.DamageSpeed),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.DamageSpeed), "Damage Speed")
                {
                    Kind = PotionEffectKey.DamageSpeed,
                    IsHostile = true,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 7f,
                    SkillScalingFactor = 0.6f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.DamageStrength),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.DamageStrength), "Damage Strength")
                {
                    Kind = PotionEffectKey.DamageStrength,
                    IsHostile = true,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 7f,
                    SkillScalingFactor = 0.6f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.DamageWillpower),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.DamageWillpower), "Damage Willpower")
                {
                    Kind = PotionEffectKey.DamageWillpower,
                    IsHostile = true,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 7f,
                    SkillScalingFactor = 0.6f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.ChameleonNormal),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.ChameleonNormal), "Chameleon Normal")
                {
                    Kind = PotionEffectKey.ChameleonNormal,
                    IsHostile = false,
                    UsesDuration = true,
                    UsesMagnitude = false,
                    BaseDurationSeconds = 7f,
                    SkillScalingFactor = 0.2f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.ChameleonTrue),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.ChameleonTrue), "Chameleon True")
                {
                    Kind = PotionEffectKey.ChameleonTrue,
                    IsHostile = false,
                    UsesDuration = true,
                    UsesMagnitude = false,
                    BaseDurationSeconds = 7f,
                    SkillScalingFactor = 0.2f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.InvisibilityNormal),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.InvisibilityNormal), "Invisibility Normal")
                {
                    Kind = PotionEffectKey.InvisibilityNormal,
                    IsHostile = false,
                    UsesDuration = true,
                    UsesMagnitude = false,
                    BaseDurationSeconds = 7f,
                    SkillScalingFactor = 0.2f,
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

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.ShadowNormal),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.ShadowNormal), "Shadow Normal")
                {
                    Kind = PotionEffectKey.ShadowNormal,
                    IsHostile = false,
                    UsesDuration = true,
                    UsesMagnitude = false,
                    BaseDurationSeconds = 7f,
                    SkillScalingFactor = 0.2f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.ShadowTrue),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.ShadowTrue), "Shadow True")
                {
                    Kind = PotionEffectKey.ShadowTrue,
                    IsHostile = false,
                    UsesDuration = true,
                    UsesMagnitude = false,
                    BaseDurationSeconds = 7f,
                    SkillScalingFactor = 0.2f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.ComprehendLanguages),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.ComprehendLanguages), "Comprehend Languages") // The display name size needs to be reduced to fit.
                {
                    Kind = PotionEffectKey.ComprehendLanguages,
                    IsHostile = false,
                    UsesDuration = true,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 7f,
                    SkillScalingFactor = 0.2f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.DispelMagic),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.DispelMagic), "Dispel Magic") // Acts a bit weird, so will have to keep an eye on this effect for later.
                {
                    Kind = PotionEffectKey.DispelMagic,
                    IsHostile = true,
                    UsesDuration = false,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 7f,
                    SkillScalingFactor = 0.2f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.Silence),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.Silence), "Silence")
                {
                    Kind = PotionEffectKey.Silence,
                    IsHostile = true,
                    UsesDuration = true,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 7f,
                    SkillScalingFactor = 0.2f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.CureDisease),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.CureDisease), "Cure Disease")
                {
                    Kind = PotionEffectKey.CureDisease,
                    IsHostile = false,
                    UsesDuration = false,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 3f,
                    SkillScalingFactor = 0.5f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.CureParalysis),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.CureParalysis), "Cure Paralysis")
                {
                    Kind = PotionEffectKey.CureParalysis,
                    IsHostile = false,
                    UsesDuration = false,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 3f,
                    SkillScalingFactor = 0.5f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.CurePoison),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.CurePoison), "Cure Poison")
                {
                    Kind = PotionEffectKey.CurePoison,
                    IsHostile = false,
                    UsesDuration = false,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 3f,
                    SkillScalingFactor = 0.5f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.FortifyAgility),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.FortifyAgility), "Fortify Agility")
                {
                    Kind = PotionEffectKey.FortifyAgility,
                    IsHostile = false,
                    UsesDuration = true,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 3f,
                    SkillScalingFactor = 0.5f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.FortifyEndurance),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.FortifyEndurance), "Fortify Endurance")
                {
                    Kind = PotionEffectKey.FortifyEndurance,
                    IsHostile = false,
                    UsesDuration = true,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 3f,
                    SkillScalingFactor = 0.5f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.FortifyIntelligence),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.FortifyIntelligence), "Fortify Intelligence")
                {
                    Kind = PotionEffectKey.FortifyIntelligence,
                    IsHostile = false,
                    UsesDuration = true,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 3f,
                    SkillScalingFactor = 0.5f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.FortifyLuck),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.FortifyLuck), "Fortify Luck")
                {
                    Kind = PotionEffectKey.FortifyLuck,
                    IsHostile = false,
                    UsesDuration = true,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 3f,
                    SkillScalingFactor = 0.5f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.FortifyPersonality),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.FortifyPersonality), "Fortify Personality")
                {
                    Kind = PotionEffectKey.FortifyPersonality,
                    IsHostile = false,
                    UsesDuration = true,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 3f,
                    SkillScalingFactor = 0.5f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.FortifySpeed),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.FortifySpeed), "Fortify Speed")
                {
                    Kind = PotionEffectKey.FortifySpeed,
                    IsHostile = false,
                    UsesDuration = true,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 3f,
                    SkillScalingFactor = 0.5f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.FortifyStrength),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.FortifyStrength), "Fortify Strength")
                {
                    Kind = PotionEffectKey.FortifyStrength,
                    IsHostile = false,
                    UsesDuration = true,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 3f,
                    SkillScalingFactor = 0.5f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.FortifyWillpower),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.FortifyWillpower), "Fortify Willpower")
                {
                    Kind = PotionEffectKey.FortifyWillpower,
                    IsHostile = false,
                    UsesDuration = true,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 3f,
                    SkillScalingFactor = 0.5f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.FreeAction),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.FreeAction), "Free Action")
                {
                    Kind = PotionEffectKey.FreeAction,
                    IsHostile = false,
                    UsesDuration = true,
                    UsesMagnitude = false,
                    BaseDurationSeconds = 3f,
                    SkillScalingFactor = 0.5f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.HealAgility),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.HealAgility), "Heal Agility")
                {
                    Kind = PotionEffectKey.HealAgility,
                    IsHostile = false,
                    UsesDuration = false,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 3f,
                    SkillScalingFactor = 0.5f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.HealEndurance),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.HealEndurance), "Heal Endurance")
                {
                    Kind = PotionEffectKey.HealEndurance,
                    IsHostile = false,
                    UsesDuration = false,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 3f,
                    SkillScalingFactor = 0.5f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.RestoreFatigue),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.RestoreFatigue), "Restore Fatigue")
                {
                    Kind = PotionEffectKey.RestoreFatigue,
                    IsHostile = false,
                    UsesDuration = false,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 3f,
                    SkillScalingFactor = 0.5f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.RestoreHealth),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.RestoreHealth), "Restore Health")
                {
                    Kind = PotionEffectKey.RestoreHealth,
                    IsHostile = false,
                    UsesDuration = false,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 3f,
                    SkillScalingFactor = 0.5f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.HealIntelligence),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.HealIntelligence), "Heal Intelligence")
                {
                    Kind = PotionEffectKey.HealIntelligence,
                    IsHostile = false,
                    UsesDuration = false,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 3f,
                    SkillScalingFactor = 0.5f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.HealLuck),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.HealLuck), "Heal Luck")
                {
                    Kind = PotionEffectKey.HealLuck,
                    IsHostile = false,
                    UsesDuration = false,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 3f,
                    SkillScalingFactor = 0.5f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.HealPersonality),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.HealPersonality), "Heal Personality")
                {
                    Kind = PotionEffectKey.HealPersonality,
                    IsHostile = false,
                    UsesDuration = false,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 3f,
                    SkillScalingFactor = 0.5f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.HealSpeed),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.HealSpeed), "Heal Speed")
                {
                    Kind = PotionEffectKey.HealSpeed,
                    IsHostile = false,
                    UsesDuration = false,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 3f,
                    SkillScalingFactor = 0.5f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.RestoreSpellPoints),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.RestoreSpellPoints), "Restore Spell Points")
                {
                    Kind = PotionEffectKey.RestoreSpellPoints,
                    IsHostile = false,
                    UsesDuration = false,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 3f,
                    SkillScalingFactor = 0.5f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.HealStrength),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.HealStrength), "Heal Strength")
                {
                    Kind = PotionEffectKey.HealStrength,
                    IsHostile = false,
                    UsesDuration = false,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 3f,
                    SkillScalingFactor = 0.5f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.HealWillpower),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.HealWillpower), "Heal Willpower")
                {
                    Kind = PotionEffectKey.HealWillpower,
                    IsHostile = false,
                    UsesDuration = false,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 3f,
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

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.SpellAbsorption),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.SpellAbsorption), "Spell Absorption")
                {
                    Kind = PotionEffectKey.SpellAbsorption,
                    IsHostile = false,
                    UsesDuration = true,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 3f,
                    SkillScalingFactor = 0.7f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.DetectEnemy),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.DetectEnemy), "Detect Enemy")
                {
                    Kind = PotionEffectKey.DetectEnemy,
                    IsHostile = false,
                    UsesDuration = true,
                    UsesMagnitude = false,
                    BaseDurationSeconds = 3f,
                    SkillScalingFactor = 0.7f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.DetectMagic),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.DetectMagic), "Detect Magic")
                {
                    Kind = PotionEffectKey.DetectMagic,
                    IsHostile = false,
                    UsesDuration = true,
                    UsesMagnitude = false,
                    BaseDurationSeconds = 3f,
                    SkillScalingFactor = 0.7f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.DetectTreasure),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.DetectTreasure), "Detect Treasure")
                {
                    Kind = PotionEffectKey.DetectTreasure,
                    IsHostile = false,
                    UsesDuration = true,
                    UsesMagnitude = false,
                    BaseDurationSeconds = 3f,
                    SkillScalingFactor = 0.7f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.Levitate),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.Levitate), "Levitation")
                {
                    Kind = PotionEffectKey.Levitate,
                    IsHostile = false,
                    UsesDuration = true,
                    UsesMagnitude = false,
                    BaseDurationSeconds = 3f,
                    SkillScalingFactor = 0.7f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.PacifyAnimal),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.PacifyAnimal), "Pacify Animal")
                {
                    Kind = PotionEffectKey.PacifyAnimal,
                    IsHostile = true,
                    UsesDuration = false,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 3f,
                    SkillScalingFactor = 0.7f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.PacifyDaedra),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.PacifyDaedra), "Pacify Daedra")
                {
                    Kind = PotionEffectKey.PacifyDaedra,
                    IsHostile = true,
                    UsesDuration = false,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 3f,
                    SkillScalingFactor = 0.7f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.PacifyHumanoid),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.PacifyHumanoid), "Pacify Humanoid")
                {
                    Kind = PotionEffectKey.PacifyHumanoid,
                    IsHostile = true,
                    UsesDuration = false,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 3f,
                    SkillScalingFactor = 0.7f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.PacifyUndead),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.PacifyUndead), "Pacify Undead")
                {
                    Kind = PotionEffectKey.PacifyUndead,
                    IsHostile = true,
                    UsesDuration = false,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 3f,
                    SkillScalingFactor = 0.7f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.SpellReflection),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.SpellReflection), "Spell Reflection")
                {
                    Kind = PotionEffectKey.SpellReflection,
                    IsHostile = false,
                    UsesDuration = true,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 3f,
                    SkillScalingFactor = 0.7f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.SpellResistance),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.SpellResistance), "Spell Resistance")
                {
                    Kind = PotionEffectKey.SpellResistance,
                    IsHostile = false,
                    UsesDuration = true,
                    UsesMagnitude = true,
                    BaseDurationSeconds = 3f,
                    SkillScalingFactor = 0.7f,
                });

            dict.Add(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.WaterWalking),
                new EffectDefinition(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.WaterWalking), "Water Walking")
                {
                    Kind = PotionEffectKey.WaterWalking,
                    IsHostile = false,
                    UsesDuration = true,
                    UsesMagnitude = false,
                    BaseDurationSeconds = 3f,
                    SkillScalingFactor = 0.7f,
                });

            return dict;
        }

        public static bool TryGet(string effectId, out EffectDefinition def)
        {
            return effects.TryGetValue(effectId, out def);
        }
    }
}