using System;
using AlchemyOverhaul.Data.Enums;
using AlchemyOverhaul.Potions;

namespace AlchemyOverhaul.Data.Runtime
{
    [Serializable]
    public sealed class PotionEffectInstance
    {
        // ===== Identity =====
        public readonly string EffectKey;

        // ===== Final frozen values =====
        public readonly int Magnitude;
        public readonly int Duration;
        public readonly PotionEffectDurationType DurationType;

        // ===== Other values =====
        public readonly EffectScalingModel ScalingModel;
        public readonly EffectIdentificationLevel IdentificationLevel;

        // ===== Optional execution flags =====
        public readonly bool IsHarmful;
        public readonly bool IsBeneficial;

        // Constructor enforces correctness
        public PotionEffectInstance(string effectKey, int magnitude, int duration, PotionEffectDurationType durationType, EffectScalingModel scalingModel, EffectIdentificationLevel identificationLevel, bool isHarmful = false, bool isBeneficial = true)
        {
            if (string.IsNullOrEmpty(effectKey))
                throw new ArgumentException("EffectKey cannot be null or empty.");

            if (magnitude <= 0)
                throw new ArgumentOutOfRangeException(nameof(magnitude));

            if (durationType == PotionEffectDurationType.Timed && duration <= 0)
                throw new ArgumentOutOfRangeException(nameof(duration));

            EffectKey = effectKey;
            Magnitude = magnitude;
            Duration = duration;
            DurationType = durationType;
            ScalingModel = scalingModel;
            IdentificationLevel = identificationLevel;
            IsHarmful = isHarmful;
            IsBeneficial = isBeneficial;
        }
    }
}
