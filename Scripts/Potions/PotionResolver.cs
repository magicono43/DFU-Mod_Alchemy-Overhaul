using System.Collections.Generic;
using AlchemyOverhaul.Data.Enums;
using AlchemyOverhaul.Data.Runtime;
using AlchemyOverhaul.Data.Definitions;

namespace AlchemyOverhaul.Potions
{
    public static class PotionResolver
    {
        private static readonly Dictionary<string, PotionDefinition> potionDefinitions =
            new Dictionary<string, PotionDefinition>
            {
            {
                AOConstants.PotionIds.TestHealRegenV1,
                new PotionDefinition
                {
                    Id = AOConstants.PotionIds.TestHealRegenV1,
                    Effects = new PotionEffectInstance[]
                    {
                        new PotionEffectInstance
                        (
                            PotionEffectKey.RestoreHealth.ToString(),
                            20,
                            0,
                            PotionEffectDurationType.Instant,
                            EffectScalingModel.Additive,
                            EffectIdentificationLevel.Full
                        ),
                        new PotionEffectInstance
                        (
                            PotionEffectKey.RegenerateHealth.ToString(),
                            3,
                            3,
                            PotionEffectDurationType.Timed,
                            EffectScalingModel.Additive,
                            EffectIdentificationLevel.Full
                        )
                    }
                }
            }
            };

        public static PotionDefinition ResolveById(string potionId)
        {
            potionDefinitions.TryGetValue(potionId, out PotionDefinition potion);
            return potion;
        }
    }
}