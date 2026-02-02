using System.Linq;
using System.Collections.Generic;
using DaggerfallWorkshop.Game.Items;
using DaggerfallWorkshop.Game.Entity;

namespace AlchemyOverhaul.Systems
{
    public static class PotionCleanupSystem
    {
        public static void RunCleanup(PlayerEntity player)
        {
            HashSet<ulong> validUids = new HashSet<ulong>();

            // Player inventory
            ItemCollection items = player.Items;

            for (int i = 0; i < items.Count; i++)
            {
                DaggerfallUnityItem item = items.GetItem(i);
                validUids.Add(item.UID);
            }

            // Remove orphaned potion data
            List<ulong> toRemove = new List<ulong>();

            foreach (var kvp in PotionRegistry.AllPotions())
            {
                if (!validUids.Contains(kvp.Key))
                    toRemove.Add(kvp.Key);
            }

            foreach (ulong uid in toRemove)
                PotionRegistry.UnregisterPotion(uid);
        }
    }
}

