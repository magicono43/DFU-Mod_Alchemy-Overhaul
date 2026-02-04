using DaggerfallWorkshop.Game.Items;

namespace AlchemyOverhaul.Systems
{
    public static class PotionConsumptionHook
    {
        public static void OnPotionConsumed(DaggerfallUnityItem item)
        {
            PotionRegistry.UnregisterPotion(item.UID);
        }
    }
}