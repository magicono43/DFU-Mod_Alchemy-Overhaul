using System.Collections.Generic;
using AlchemyOverhaul.Data.Enums;
using AlchemyOverhaul.Data.Runtime;

namespace AlchemyOverhaul.Potions
{
    public static class PotionResolver
    {
        private static readonly Dictionary<string, CustomPotion> potionDefinitions =
            new Dictionary<string, CustomPotion>
            {
            {
                AOConstants.PotionIds.TestHealRegenV1,
                new CustomPotion
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

        public static CustomPotion ResolveById(string potionId)
        {
            potionDefinitions.TryGetValue(potionId, out CustomPotion potion);
            return potion;
        }
    }
}