// Project:         Alchemy Overhaul mod for Daggerfall Unity (http://www.dfworkshop.net)
// Copyright:       Copyright (C) 2026 Kirk.O
// License:         MIT License (http://www.opensource.org/licenses/mit-license.php)
// Author:          Kirk.O
// Created On: 	    1/13/2026, 10:00 PM
// Last Edit:		2/7/2026, 6:45 PM
// Version:			1.00
// Special Thanks:  
// Modifier:

using UnityEngine;
using DaggerfallWorkshop.Game;
using DaggerfallWorkshop.Game.Utility.ModSupport;
using System;
using DaggerfallWorkshop;
using DaggerfallWorkshop.Game.Items;
using Wenzil.Console;
using AlchemyOverhaul.Items;
using AlchemyOverhaul.Systems;
using AlchemyOverhaul.Data.Runtime;

namespace AlchemyOverhaul
{
    public partial class AlchemyOverhaulMain : MonoBehaviour
    {
        public static AlchemyOverhaulMain Instance;
        public static AlchemyOverhaulSaveData ModSaveData;

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

            ModSaveData = new AlchemyOverhaulSaveData();
            mod.SaveDataInterface = ModSaveData;

            DaggerfallUnity.Instance.ItemHelper.RegisterCustomItem(AOConstants.ItemIds.TestPotion, ItemGroups.UselessItems1, typeof(ItemTestPotion)); // Register Test Potion item.

            RegisterConsoleCommands();

            WorldTime.OnNewDay += ModSaveData.RemoveStalePotionRecords_OnNewDay;

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

                DaggerfallUnityItem item = ItemBuilder.CreateItem(ItemGroups.UselessItems1, AOConstants.ItemIds.TestPotion);

                PotionData data = PotionRegistry.CreatePotion(AOConstants.PotionIds.TestHealRegenV1);

                if (data == null)
                    return "Failed to create potion.";

                ModSaveData.AddPotion(item.UID, data);

                playerEntity.Items.AddItem(item);

                return "Gave you a test potion.";
            }
        }
    }
}
