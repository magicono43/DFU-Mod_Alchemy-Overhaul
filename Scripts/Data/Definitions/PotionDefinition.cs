using AlchemyOverhaul.Data.Runtime;

namespace AlchemyOverhaul.Data.Definitions
{
    public sealed class PotionDefinition
    {
        public string Id;

        // Effect blueprint
        public PotionEffectInstance[] Effects;

        // Design-time attributes
        public float Toxicity;
        public float Palatability;
        public float SpoilageRate;
    }
}