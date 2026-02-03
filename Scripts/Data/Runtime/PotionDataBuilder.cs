using System;
using DaggerfallWorkshop;
using System.Collections.Generic;
using AlchemyOverhaul.Data.Enums;
using AlchemyOverhaul.Data.Definitions;

namespace AlchemyOverhaul.Data.Runtime
{
    public static class PotionDataBuilder
    {
        public static PotionData BuildFromDefinition(PotionDefinition definition, int brewerEntityId = -1, int alchemySkill = 0, PotionBrewFlags brewFlags = PotionBrewFlags.None)
        {
            PotionData data = new PotionData
            {
                // ===== Identity =====
                PotionInstanceId = Guid.NewGuid().ToString(),
                PotionDefinitionId = definition.Id,
                CreatedGameTime = DaggerfallUnity.Instance.WorldTime.DaggerfallDateTime.ToSeconds(),
                BrewerEntityId = brewerEntityId,
                AlchemySkillAtBrew = alchemySkill,
                BrewFlags = brewFlags,

                // ===== Provenance =====
                Ingredients = new List<IngredientContribution>(),

                // ===== Effects =====
                Effects = new List<PotionEffectInstance>(),

                // ===== Spoilage =====
                IsSpoiled = false,
                LastSpoilageCheckTime = 0
            };

            // Resolve effects into runtime instances
            foreach (PotionEffectInstance effect in definition.Effects)
            {
                data.Effects.Add(new PotionEffectInstance
                (
                    effect.EffectKey,
                    effect.Magnitude,
                    effect.Duration,
                    effect.DurationType,
                    effect.ScalingModel,
                    effect.IdentificationLevel
                ));
            }

            return data;
        }
    }
}