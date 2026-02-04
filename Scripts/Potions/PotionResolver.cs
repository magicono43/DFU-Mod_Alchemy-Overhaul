using System.Collections.Generic;
using AlchemyOverhaul.Data.Enums;
using AlchemyOverhaul.Data.Definitions;
using AlchemyOverhaul.Data.Runtime;

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
                        Effects = new PotionEffectBlueprint[]
                        {
                            new PotionEffectBlueprint
                            {
                                EffectKey = PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.RestoreHealth),
                                MinMagnitude = 20,
                                MaxMagnitude = 20,
                                MinDuration = 0,
                                MaxDuration = 0,
                                DurationType = PotionEffectDurationType.Instant,
                                ScalingModel = EffectScalingModel.Additive,
                                IdentificationLevel = EffectIdentificationLevel.Full
                            },
                            new PotionEffectBlueprint
                            {
                                EffectKey = PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.RegenerateHealth),
                                MinMagnitude = 3,
                                MaxMagnitude = 3,
                                MinDuration = 3,
                                MaxDuration = 3,
                                DurationType = PotionEffectDurationType.Timed,
                                ScalingModel = EffectScalingModel.Additive,
                                IdentificationLevel = EffectIdentificationLevel.Full
                            }
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