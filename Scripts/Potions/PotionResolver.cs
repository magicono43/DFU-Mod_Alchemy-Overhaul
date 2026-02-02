using System.Collections.Generic;
using AlchemyOverhaul.Data.Enums;

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
                    Effects = new CustomPotionEffect[]
                    {
                        new CustomPotionEffect
                        {
                            EffectKey = PotionEffectKey.RestoreHealth,
                            Magnitude = 20,
                            DurationType = PotionEffectDurationType.Instant
                        },
                        new CustomPotionEffect
                        {
                            EffectKey = PotionEffectKey.RegenerateHealth,
                            Magnitude = 3,
                            DurationSeconds = 3,
                            DurationType = PotionEffectDurationType.Timed
                        }
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