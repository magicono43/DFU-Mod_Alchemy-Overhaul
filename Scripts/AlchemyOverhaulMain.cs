// Project:         Alchemy Overhaul mod for Daggerfall Unity (http://www.dfworkshop.net)
// Copyright:       Copyright (C) 2020 Kirk.O
// License:         MIT License (http://www.opensource.org/licenses/mit-license.php)
// Author:          Kirk.O
// Created On: 	    1/13/2026, 10:00 PM
// Last Edit:		1/16/2026, 11:00 PM
// Version:			1.00
// Special Thanks:  
// Modifier:

using UnityEngine;
using DaggerfallWorkshop.Game;
using DaggerfallWorkshop.Game.Utility.ModSupport;
using System;
using DaggerfallWorkshop;
using DaggerfallWorkshop.Game.Items;
using DaggerfallWorkshop.Game.Entity;
using DaggerfallWorkshop.Game.MagicAndEffects;
using Wenzil.Console;
using DaggerfallWorkshop.Game.MagicAndEffects.MagicEffects;

namespace AlchemyOverhaul
{
    public partial class AlchemyOverhaulMain : MonoBehaviour
    {
        public static AlchemyOverhaulMain Instance;

        static Mod mod;

        [Invoke(StateManager.StateTypes.Start, 0)]
        public static void Init(InitParams initParams)
        {
            mod = initParams.Mod;
            var go = new GameObject(mod.Title);
            go.AddComponent<AlchemyOverhaulMain>(); // Add script to the scene.

            mod.IsReady = true;
        }

        private void Start()
        {
            Debug.Log("Begin mod init: Alchemy Overhaul");

            Instance = this;

            DaggerfallUnity.Instance.ItemHelper.RegisterCustomItem(1234588311, ItemGroups.UselessItems1, typeof(ItemTestPotion)); // Register Test Potion item.

            RegisterConsoleCommands();

            Debug.Log("Finished mod init: Alchemy Overhaul");
        }

        public static void RegisterConsoleCommands()
        {
            Debug.Log("[AlchemyOverhaul] Trying to register console commands.");
            try
            {
                ConsoleCommandsDatabase.RegisterCommand(GiveTestPotion.name, GiveTestPotion.description, GiveTestPotion.usage, GiveTestPotion.Execute);
            }
            catch (Exception e)
            {
                Debug.LogError(string.Format("Error Registering AlchemyOverhaul Console commands: {0}", e.Message));
            }
        }

        private static class GiveTestPotion
        {
            public static readonly string name = "addtestpotion";
            public static readonly string description = "Adds a test potion to your inventory.";
            public static readonly string usage = "addtestpotion";

            public static string Execute(params string[] args)
            {
                DaggerfallWorkshop.Game.Entity.PlayerEntity playerEntity = GameManager.Instance.PlayerEntity;

                DaggerfallUnityItem item = ItemBuilder.CreateItem(ItemGroups.UselessItems1, 1234588311);
                playerEntity.Items.AddItem(item);

                return "Gave you a test potion.";
            }
        }
    }

    public static class AlchemyExecutionAdapter
    {
        /// <summary>
        /// Executes a fully-resolved potion effect.
        /// All scaling, randomness, stacking, and validation MUST be done before calling this.
        /// </summary>
        public static void ApplyPotionEffect(string effectKey, int magnitude, int durationSeconds)
        {
            PlayerEntity player = GameManager.Instance.PlayerEntity;
            EntityEffectBroker broker = GameManager.Instance.EntityEffectBroker;

            // Pull template (execution only)
            IEntityEffect template = broker.GetEffectTemplate(effectKey);
            if (template == null)
                return;

            // Hard-freeze effect math
            EffectSettings settings = new EffectSettings
            {
                MagnitudeBaseMin = magnitude,
                MagnitudeBaseMax = magnitude,
                DurationBase = durationSeconds,
                ChanceBase = 100,

                // Safe per-level values
                MagnitudePerLevel = 1,
                DurationPerLevel = 1,
                ChancePerLevel = 1,

                // Neutralize scaling
                MagnitudePlusMin = 0,
                MagnitudePlusMax = 0,
                DurationPlus = 0,
                ChancePlus = 0,
            };

            EffectEntry[] entries = new EffectEntry[]
            {
                new EffectEntry(effectKey, settings)
            };

            EffectBundleSettings bundleSettings = new EffectBundleSettings
            {
                Version = EntityEffectBroker.CurrentSpellVersion,
                Name = "AO_PotionEffect",
                BundleType = BundleTypes.Potion,
                TargetType = TargetTypes.CasterOnly,
                Effects = entries,
            };

            EntityEffectBundle bundle =
                new EntityEffectBundle(bundleSettings, GameManager.Instance.PlayerEntityBehaviour);

            // Force application — no DFU logic allowed
            GameManager.Instance.PlayerEffectManager.AssignBundle(
                bundle,
                AssignBundleFlags.BypassChance |
                AssignBundleFlags.BypassSavingThrows
            );
        }

        public static void ApplyInstantEffect(string effectKey, int magnitude)
        {
            PlayerEntity player = GameManager.Instance.PlayerEntity;
            EntityEffectBroker broker = GameManager.Instance.EntityEffectBroker;

            IEntityEffect template = broker.GetEffectTemplate(effectKey);
            if (template == null)
                return;

            EffectSettings settings = new EffectSettings
            {
                MagnitudeBaseMin = magnitude,
                MagnitudeBaseMax = magnitude,
                ChanceBase = 100,

                // Safe defaults
                MagnitudePerLevel = 1,
                ChancePerLevel = 1,
                MagnitudePlusMin = 0,
                MagnitudePlusMax = 0,
                ChancePlus = 0,
            };

            EffectEntry[] entries = new EffectEntry[]
            {
                new EffectEntry(effectKey, settings)
            };

            EffectBundleSettings bundleSettings = new EffectBundleSettings
            {
                Version = EntityEffectBroker.CurrentSpellVersion,
                Name = "AO_InstantPotion",
                BundleType = BundleTypes.Potion,
                TargetType = TargetTypes.CasterOnly,
                Effects = entries,
            };

            EntityEffectBundle bundle =
                new EntityEffectBundle(bundleSettings, GameManager.Instance.PlayerEntityBehaviour);

            GameManager.Instance.PlayerEffectManager.AssignBundle(
                bundle,
                AssignBundleFlags.BypassChance |
                AssignBundleFlags.BypassSavingThrows
            );
        }
    }

    public class CustomPotion
    {
        public string Id;
        public CustomPotionEffect[] Effects;
    }

    public enum PotionEffectDurationType
    {
        Instant,
        Timed
    }

    public class CustomPotionEffect
    {
        // Must map to DFU effect key
        public string EffectKey;

        // Final resolved values (NO DFU scaling)
        public int Magnitude;
        public int DurationSeconds;

        public PotionEffectDurationType DurationType;
    }

    public static class PotionResolver
    {
        public static CustomPotion ResolveTestPotion()
        {
            return new CustomPotion
            {
                Id = "test_potion_mixed",

                Effects = new CustomPotionEffect[]
                {
            new CustomPotionEffect
            {
                EffectKey = "Heal-Health",
                Magnitude = 20,
                DurationSeconds = 0,
                DurationType = PotionEffectDurationType.Instant
            },
            new CustomPotionEffect
            {
                EffectKey = "Regenerate",
                Magnitude = 5,
                DurationSeconds = 4,
                DurationType = PotionEffectDurationType.Timed
            }
                }
            };
        }

        /*
        public static CustomPotion ResolveTestPotion()
        {
            return new CustomPotion
            {
                Id = "test_potion_regen",

                Effects = new CustomPotionEffect[]
                {
                    new CustomPotionEffect
                    {
                        EffectKey = "Regenerate",
                        Magnitude = 10,
                        DurationSeconds = 5,
                    }
                }
            };
        }
        */
    }

    //Test Potion
    public class ItemTestPotion : DaggerfallUnityItem
    {
        public ItemTestPotion() : base(ItemGroups.UselessItems1, 1234588311)
        {
            shortName = "Test Potion (AO)";
        }

        public override bool UseItem(ItemCollection collection)
        {
            // Resolve potion using YOUR system
            CustomPotion potion = PotionResolver.ResolveTestPotion();

            // Execute resolved effects
            foreach (CustomPotionEffect effect in potion.Effects)
            {
                if (effect.DurationType == PotionEffectDurationType.Instant)
                {
                    AlchemyExecutionAdapter.ApplyInstantEffect(
                        effect.EffectKey,
                        effect.Magnitude
                    );
                }
                else
                {
                    AlchemyExecutionAdapter.ApplyPotionEffect(
                        effect.EffectKey,
                        effect.Magnitude,
                        effect.DurationSeconds
                    );
                }
            }

            // Consume item
            collection.RemoveItem(this);

            // Suppress vanilla behavior
            return true;
        }
    }
}
