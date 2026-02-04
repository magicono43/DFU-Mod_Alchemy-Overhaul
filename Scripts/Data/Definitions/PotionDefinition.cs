using AlchemyOverhaul.Data.Runtime;

namespace AlchemyOverhaul.Data.Definitions
{
    public sealed class PotionDefinition
    {
        public string Id;

        // Effect blueprint
        public PotionEffectBlueprint[] Effects;

        // Design-time attributes
        public float Toxicity;
        public float Palatability;
        public float SpoilageRate;

        public bool TryValidateEffect(PotionEffectInstance instance)
        {
            foreach (var blueprint in Effects)
            {
                if (blueprint.EffectKey != instance.EffectKey)
                    continue;

                // Validate without mutation
                if (instance.Magnitude < blueprint.MinMagnitude ||
                    instance.Magnitude > blueprint.MaxMagnitude)
                    return false;

                if (instance.Duration < blueprint.MinDuration ||
                    instance.Duration > blueprint.MaxDuration)
                    return false;

                return true;
            }

            // Effect no longer exists in definition
            return false;
        }
    }
}