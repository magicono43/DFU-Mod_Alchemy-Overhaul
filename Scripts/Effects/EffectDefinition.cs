using AlchemyOverhaul.Data.Enums;

namespace AlchemyOverhaul.Effects
{
    public sealed class EffectDefinition
    {
        public string EffectId { get; }
        public string DisplayName { get; }

        public PotionEffectKey Kind;

        public bool IsHostile;
        public bool UsesDuration;
        public bool UsesMagnitude;

        public float BaseDurationSeconds;
        public float SkillScalingFactor;

        public EffectDefinition(
            string effectId,
            string displayName)
        {
            EffectId = effectId;
            DisplayName = displayName;
        }
    }
}