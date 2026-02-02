using System;
using AlchemyOverhaul.Data.Enums;
using AlchemyOverhaul.Potions;

namespace AlchemyOverhaul.Data.Runtime
{
    [Serializable]
    public struct PotionEffectInstance
    {
        public PotionEffectKey EffectKey;

        public float Magnitude;
        public float Duration;

        public PotionEffectDurationType DurationType;

        public EffectScalingModel ScalingModel;

        public bool IsPrimary;

        public EffectIdentificationLevel IdentificationLevel;
    }
}
