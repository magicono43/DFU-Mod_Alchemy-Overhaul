namespace AlchemyOverhaul
{
    /// <summary>
    /// Centralized constants for the Alchemy Overhaul mod.
    /// Nothing in here should ever contain logic.
    /// </summary>
    public static class AOConstants
    {
        public static class ItemIds
        {
            // Custom DFU item template IDs
            public const int TestPotion = 1234588311;
        }

        public static class PotionIds
        {
            // Resolver keys (data-driven)
            public const string TestHealRegenV1 = "ao:potion:test_heal_regen_v1";
        }

        public static class BundleNames
        {
            // Effect bundle names (debug / identification only)
            public const string PotionEffect = "AO_PotionEffect";
            public const string InstantPotion = "AO_InstantPotion";
        }
    }
}