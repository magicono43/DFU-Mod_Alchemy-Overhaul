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
            public const int TestPotion = 88880000;
            public const int MortarAndPestle = 88880001;
            public const int Retort = 88880002;
            public const int Calcinator = 88880003;
            public const int Alembic = 88880004;
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