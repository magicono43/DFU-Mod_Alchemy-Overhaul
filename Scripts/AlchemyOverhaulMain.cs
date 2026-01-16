// Project:         Alchemy Overhaul mod for Daggerfall Unity (http://www.dfworkshop.net)
// Copyright:       Copyright (C) 2020 Kirk.O
// License:         MIT License (http://www.opensource.org/licenses/mit-license.php)
// Author:          Kirk.O
// Created On: 	    1/13/2026, 10:00 PM
// Last Edit:		1/15/2026, 12:30 AM
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

    //Test Potion
    public class ItemTestPotion : DaggerfallUnityItem
    {
        public ItemTestPotion() : base(ItemGroups.UselessItems1, 1234588311)
        {
            shortName = "Test Potion (AO)";
        }

        public override bool UseItem(ItemCollection collection)
        {
            // Resolve DFU state safely at runtime
            PlayerEntity player = GameManager.Instance.PlayerEntity;
            EntityEffectBroker broker = GameManager.Instance.EntityEffectBroker;

            // --- Phase 1: hardcoded Restore Health over time ---
            IEntityEffect effect = broker.GetEffectTemplate("Regenerate");

            EffectSettings newSettings = new EffectSettings
            {
                MagnitudeBaseMin = 10,
                MagnitudeBaseMax = 10,
                MagnitudePerLevel = 1, // Had to add this because it would cause a divide by zero error otherwise.
                DurationBase = 5,
                DurationPerLevel = 1 // Had to add this because it would cause a divide by zero error otherwise.
            };

            // I'll continue playing around with this tomorrow, atleast got the simple regeneration effect to work without errors.

            effect.Settings = newSettings;

            EffectEntry[] potionEffects = new EffectEntry[] { new EffectEntry("Regenerate", effect.Settings) };

            // Create the effect bundle settings.
            EffectBundleSettings bundleSettings = new EffectBundleSettings()
            {
                Version = EntityEffectBroker.CurrentSpellVersion,
                Name = "TestPotion",
                BundleType = BundleTypes.Potion,
                TargetType = TargetTypes.CasterOnly,
                Effects = potionEffects,
            };
            // Assign effect bundle.
            EntityEffectBundle bundle = new EntityEffectBundle(bundleSettings, GameManager.Instance.PlayerEntityBehaviour);
            GameManager.Instance.PlayerEffectManager.AssignBundle(bundle, AssignBundleFlags.BypassSavingThrows | AssignBundleFlags.BypassChance);

            //effect.Start(player.EntityBehaviour.GetComponent<EntityEffectManager>(), GameManager.Instance.PlayerEntityBehaviour);

            // Consume the item manually
            collection.RemoveItem(this);

            // Returning true suppresses vanilla behavior
            return true;
        }
    }
}
