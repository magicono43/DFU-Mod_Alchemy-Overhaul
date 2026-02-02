using AlchemyOverhaul.Data.Enums;

namespace AlchemyOverhaul.Potions
{
    public class CustomPotionEffect
    {
        // Must map to DFU effect key
        public PotionEffectKey EffectKey;

        // Final resolved values (NO DFU scaling)
        public int Magnitude;
        public int DurationSeconds;

        public PotionEffectDurationType DurationType;
    }
}