using DaggerfallWorkshop.Game;
using DaggerfallWorkshop.Game.Entity;
using DaggerfallWorkshop.Game.MagicAndEffects;
using AlchemyOverhaul.Data.Enums;

namespace AlchemyOverhaul.Execution
{
    public static class AlchemyExecutionAdapter
    {
        /// <summary>
        /// Executes a fully-resolved potion effect.
        /// All scaling, randomness, stacking, and validation MUST be done before calling this.
        /// </summary>
        public static void ApplyPotionEffect(string effectKey, int magnitude, int duration)
        {
            PlayerEntity player = GameManager.Instance.PlayerEntity;
            EntityEffectBroker broker = GameManager.Instance.EntityEffectBroker;

            // Pull template (execution only)
            IEntityEffect template = broker.GetEffectTemplate(effectKey.ToString());
            if (template == null)
                return;

            // Hard-freeze effect math
            EffectSettings settings = new EffectSettings
            {
                MagnitudeBaseMin = magnitude,
                MagnitudeBaseMax = magnitude,
                DurationBase = duration,
                ChanceBase = 100,

                // Safe per-level values
                MagnitudePerLevel = 1,
                DurationPerLevel = 1,
                ChancePerLevel = 1,

                // Neutralize scaling
                MagnitudePlusMin = 0,
                MagnitudePlusMax = 0,
                DurationPlus = 0,
                ChancePlus = 0,
            };

            EffectEntry[] entries = new EffectEntry[]
            {
                new EffectEntry(effectKey.ToString(), settings)
            };

            EffectBundleSettings bundleSettings = new EffectBundleSettings
            {
                Version = EntityEffectBroker.CurrentSpellVersion,
                Name = AOConstants.BundleNames.PotionEffect,
                BundleType = BundleTypes.Potion,
                TargetType = TargetTypes.CasterOnly,
                Effects = entries,
            };

            EntityEffectBundle bundle =
                new EntityEffectBundle(bundleSettings, GameManager.Instance.PlayerEntityBehaviour);

            // Force application — no DFU logic allowed
            GameManager.Instance.PlayerEffectManager.AssignBundle(
                bundle,
                AssignBundleFlags.BypassChance |
                AssignBundleFlags.BypassSavingThrows
            );
        }

        public static void ApplyInstantEffect(string effectKey, int magnitude)
        {
            PlayerEntity player = GameManager.Instance.PlayerEntity;
            EntityEffectBroker broker = GameManager.Instance.EntityEffectBroker;

            IEntityEffect template = broker.GetEffectTemplate(effectKey.ToString());
            if (template == null)
                return;

            EffectSettings settings = new EffectSettings
            {
                MagnitudeBaseMin = magnitude,
                MagnitudeBaseMax = magnitude,
                ChanceBase = 100,

                // Safe defaults
                MagnitudePerLevel = 1,
                ChancePerLevel = 1,
                MagnitudePlusMin = 0,
                MagnitudePlusMax = 0,
                ChancePlus = 0,
            };

            EffectEntry[] entries = new EffectEntry[]
            {
                new EffectEntry(effectKey.ToString(), settings)
            };

            EffectBundleSettings bundleSettings = new EffectBundleSettings
            {
                Version = EntityEffectBroker.CurrentSpellVersion,
                Name = AOConstants.BundleNames.InstantPotion,
                BundleType = BundleTypes.Potion,
                TargetType = TargetTypes.CasterOnly,
                Effects = entries,
            };

            EntityEffectBundle bundle =
                new EntityEffectBundle(bundleSettings, GameManager.Instance.PlayerEntityBehaviour);

            GameManager.Instance.PlayerEffectManager.AssignBundle(
                bundle,
                AssignBundleFlags.BypassChance |
                AssignBundleFlags.BypassSavingThrows
            );
        }
    }
}