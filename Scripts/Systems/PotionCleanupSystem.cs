using UnityEngine;
using DaggerfallWorkshop;
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

            validUids = CollectAllValidItemUids(player);

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

        public static HashSet<ulong> CollectAllValidItemUids(PlayerEntity player)
        {
            HashSet<ulong> validUids = new HashSet<ulong>();

            // Player inventory
            ItemCollection items = player.Items;
            for (int i = 0; i < items.Count; i++)
            {
                DaggerfallUnityItem item = items.GetItem(i);
                if (item != null)
                    validUids.Add(item.UID);
            }

            // Player wagon
            ItemCollection wagonItems = player.WagonItems;
            for (int i = 0; i < wagonItems.Count; i++)
            {
                DaggerfallUnityItem item = wagonItems.GetItem(i);
                if (item != null)
                    validUids.Add(item.UID);
            }

            // Loot piles in scene
            DaggerfallLoot[] lootPiles =
                Object.FindObjectsOfType<DaggerfallLoot>();

            for (int i = 0; i < lootPiles.Length; i++)
            {
                ItemCollection lootItems = lootPiles[i].Items;
                for (int j = 0; j < lootItems.Count; j++)
                {
                    DaggerfallUnityItem item = lootItems.GetItem(j);
                    if (item != null)
                        validUids.Add(item.UID);
                }
            }

            return validUids;
        }
    }
}

