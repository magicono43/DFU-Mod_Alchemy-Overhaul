using AlchemyOverhaul.Data.Enums;
using AlchemyOverhaul.Potions;

namespace AlchemyOverhaul.Data.Definitions
{
    public sealed class PotionEffectBlueprint
    {
        public string EffectKey;

        public int MinMagnitude;
        public int MaxMagnitude;

        public int MinDuration;
        public int MaxDuration;

        public PotionEffectDurationType DurationType;
        public EffectScalingModel ScalingModel;
        public EffectIdentificationLevel IdentificationLevel;

        public bool AllowStacking;
    }
}