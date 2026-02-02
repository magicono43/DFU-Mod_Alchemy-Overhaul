using System;
using DaggerfallWorkshop;
using System.Collections.Generic;
using AlchemyOverhaul.Data.Enums;
using AlchemyOverhaul.Potions;

namespace AlchemyOverhaul.Data.Runtime
{
    public static class PotionDataBuilder
    {
        public static PotionData BuildFromDefinition(CustomPotion definition, int brewerEntityId = -1, int alchemySkill = 0, PotionBrewFlags brewFlags = PotionBrewFlags.None)
        {
            PotionData data = new PotionData
            {
                // ===== Identity =====
                PotionGuid = Guid.NewGuid().ToString(),
                CreatedGameTime = DaggerfallUnity.Instance.WorldTime.DaggerfallDateTime.ToSeconds(),
                BrewerEntityId = brewerEntityId,
                AlchemySkillAtBrew = alchemySkill,
                BrewFlags = brewFlags,

                // ===== Provenance =====
                Ingredients = new List<IngredientContribution>(),

                // ===== Effects =====
                Effects = new List<PotionEffectInstance>(),

                // ===== Secondary =====
                Toxicity = 0f,
                Palatability = 0f,
                SpoilageRate = 0f,

                // ===== Spoilage =====
                IsSpoiled = false,
                LastSpoilageCheckTime = 0
            };

            // Resolve effects into runtime instances
            foreach (CustomPotionEffect effect in definition.Effects)
            {
                data.Effects.Add(new PotionEffectInstance
                {
                    EffectKey = effect.EffectKey,
                    Magnitude = effect.Magnitude,
                    Duration = effect.DurationSeconds,
                    DurationType = effect.DurationType
                });
            }

            return data;
        }
    }
}