// Project:         Alchemy Overhaul mod for Daggerfall Unity (http://www.dfworkshop.net)
// Copyright:       Copyright (C) 2026 Kirk.O
// License:         MIT License (http://www.opensource.org/licenses/mit-license.php)
// Author:          Kirk.O
// Created On: 	    1/13/2026, 10:00 PM
// Last Edit:		2/21/2026, 8:00 AM
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
using DaggerfallWorkshop.Game.UserInterfaceWindows;
using DaggerfallWorkshop.Game.Serialization;

namespace AlchemyOverhaul
{
    public partial class AlchemyOverhaulMain : MonoBehaviour
    {
        public static AlchemyOverhaulMain Instance;
        public static AlchemyOverhaulSaveData ModSaveData;

        static Mod mod;

        public const KeyCode InfoWindowKey = KeyCode.G;

        // Mod Textures || GUI
        public Texture2D BrewingLocalSearchBarTexture;
        public Texture2D BrewingLocalInventoryPanelTexture;
        public Texture2D BrewingPrimaryHoverTextPanelTexture;
        public Texture2D BrewingAlchemyToolsPanelTexture;
        public Texture2D BrewingResultInfoPanelTexture;
        public Texture2D BrewingIngredientInputPanelTexture;
        public Texture2D BrewingBrewButtonActiveTexture;
        public Texture2D BrewingHeatActiveIconTexture;
        public Texture2D BrewingHeatSettingButtonTexture;
        public Texture2D BrewingMiscInfoPanelTexture;
        public Texture2D BrewingRecipeSearchBarTexture;
        public Texture2D BrewingRecipeListPanelTexture;
        public Texture2D BrewingHelpButtonActiveTexture;
        public Texture2D BrewingExitButtonTexture;

        public Texture2D ExtraRightGreenUpArrowTexture;
        public Texture2D ExtraRightGreenDownArrowTexture;
        public Texture2D ExtraLeftGreenUpArrowTexture;
        public Texture2D ExtraLeftGreenDownArrowTexture;
        public Texture2D ExtraRightRedUpArrowTexture;
        public Texture2D ExtraRightRedDownArrowTexture;
        public Texture2D ExtraLeftRedUpArrowTexture;
        public Texture2D ExtraLeftRedDownArrowTexture;
        public Texture2D SortButtonBackgroundTexture;
        public Texture2D SortButtonActiveBorderTexture;
        public Texture2D SortIconCheckmarkTexture;
        public Texture2D SortIconXmarkTexture;
        public Texture2D SortIconPercentTexture;
        public Texture2D SortIconAscendingTexture;
        public Texture2D SortIconDescendingTexture;

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

            // Load Resources
            LoadTextures();

            DaggerfallUnity.Instance.ItemHelper.RegisterCustomItem(AOConstants.ItemIds.TestPotion, ItemGroups.UselessItems1, typeof(ItemTestPotion)); // Register Test Potion item.

            RegisterConsoleCommands();

            WorldTime.OnNewDay += ModSaveData.RemoveStalePotionRecords_OnNewDay;

            Debug.Log("Finished mod init: Alchemy Overhaul");
        }

        private void Update()
        {
            if (!GameManager.Instance.StateManager.GameInProgress || SaveLoadManager.Instance.LoadInProgress)
                return;

            // Handle key presses
            if (InputManager.Instance.GetKeyUp(InfoWindowKey))
            {
                if (DaggerfallUI.Instance.UserInterfaceManager.TopWindow is AOPotionCraftingWindow)
                {
                    (DaggerfallUI.Instance.UserInterfaceManager.TopWindow as AOPotionCraftingWindow).CloseWindow();
                }
                else if (!GameManager.IsGamePaused && GameManager.Instance.PlayerObject != null && DaggerfallUI.UIManager.WindowCount <= 0)
                {
                    AOPotionCraftingWindow infoWindow = new AOPotionCraftingWindow(DaggerfallUI.UIManager);
                    DaggerfallUI.UIManager.PushWindow(infoWindow);
                }
            }
        }

        private void LoadTextures() // Example taken from Penwick Papers Mod
        {
            ModManager modManager = ModManager.Instance;
            bool success = true;

            success &= modManager.TryGetAsset("AO_Potion_Crafting_Search_Bar_1", false, out BrewingLocalSearchBarTexture);
            success &= modManager.TryGetAsset("AO_Potion_Crafting_Ingredient_Inventory_Panel_1", false, out BrewingLocalInventoryPanelTexture);
            success &= modManager.TryGetAsset("AO_Potion_Crafting_Primary_Info_Panel", false, out BrewingPrimaryHoverTextPanelTexture);
            success &= modManager.TryGetAsset("AO_Potion_Crafting_Brewing_Tool_Panel", false, out BrewingAlchemyToolsPanelTexture);
            success &= modManager.TryGetAsset("AO_Potion_Crafting_Brewing_Result_Panel", false, out BrewingResultInfoPanelTexture);
            success &= modManager.TryGetAsset("AO_Potion_Crafting_Brewing_Input_Panel_1", false, out BrewingIngredientInputPanelTexture);
            success &= modManager.TryGetAsset("AO_Potion_Crafting_Brew_Button", false, out BrewingBrewButtonActiveTexture);
            success &= modManager.TryGetAsset("AO_Potion_Crafting_Brewing_Heat_Icon_Active", false, out BrewingHeatActiveIconTexture);
            success &= modManager.TryGetAsset("AO_Potion_Crafting_Brewing_Heat_Setting_Panel", false, out BrewingHeatSettingButtonTexture);
            success &= modManager.TryGetAsset("AO_Potion_Crafting_Misc_Info_Panel", false, out BrewingMiscInfoPanelTexture);
            success &= modManager.TryGetAsset("AO_Potion_Crafting_Search_Bar_2", false, out BrewingRecipeSearchBarTexture);
            success &= modManager.TryGetAsset("AO_Potion_Crafting_Recipe_Panel_1", false, out BrewingRecipeListPanelTexture);
            success &= modManager.TryGetAsset("AO_Potion_Crafting_Help_Button", false, out BrewingHelpButtonActiveTexture);
            success &= modManager.TryGetAsset("AO_Potion_Crafting_Exit_Button", false, out BrewingExitButtonTexture);

            success &= modManager.TryGetAsset("Right_Green_Arrow_Up_1", false, out ExtraRightGreenUpArrowTexture);
            success &= modManager.TryGetAsset("Right_Green_Arrow_Down_1", false, out ExtraRightGreenDownArrowTexture);
            success &= modManager.TryGetAsset("Left_Green_Arrow_Up_1", false, out ExtraLeftGreenUpArrowTexture);
            success &= modManager.TryGetAsset("Left_Green_Arrow_Down_1", false, out ExtraLeftGreenDownArrowTexture);
            success &= modManager.TryGetAsset("Right_Red_Arrow_Up_1", false, out ExtraRightRedUpArrowTexture);
            success &= modManager.TryGetAsset("Right_Red_Arrow_Down_1", false, out ExtraRightRedDownArrowTexture);
            success &= modManager.TryGetAsset("Left_Red_Arrow_Up_1", false, out ExtraLeftRedUpArrowTexture);
            success &= modManager.TryGetAsset("Left_Red_Arrow_Down_1", false, out ExtraLeftRedDownArrowTexture);
            success &= modManager.TryGetAsset("Sort_Button_Background_And_Border_1", false, out SortButtonBackgroundTexture);
            success &= modManager.TryGetAsset("Sort_Button_Active_Border_1", false, out SortButtonActiveBorderTexture);
            success &= modManager.TryGetAsset("Sort_Button_Icon_Checkmark_1", false, out SortIconCheckmarkTexture);
            success &= modManager.TryGetAsset("Sort_Button_Icon_X_Mark_1", false, out SortIconXmarkTexture);
            success &= modManager.TryGetAsset("Sort_Button_Icon_Percent_1", false, out SortIconPercentTexture);
            success &= modManager.TryGetAsset("Sort_Button_Icon_Ascending_Arrow_1", false, out SortIconAscendingTexture);
            success &= modManager.TryGetAsset("Sort_Button_Icon_Descending_Arrow_1", false, out SortIconDescendingTexture);

            if (!success)
                throw new Exception("AlchemyOverhaul: Missing texture asset");
        }

        public static void RegisterConsoleCommands()
        {
            Debug.Log("[AlchemyOverhaul] Trying to register console commands.");
            try
            {
                ConsoleCommandsDatabase.RegisterCommand(GiveTestPotion.name, GiveTestPotion.description, GiveTestPotion.usage, GiveTestPotion.Execute);
                ConsoleCommandsDatabase.RegisterCommand(ChangeTestNumber.command, ChangeTestNumber.description, ChangeTestNumber.usage, ChangeTestNumber.Execute);
                ConsoleCommandsDatabase.RegisterCommand(ChangeButtonRect.command, ChangeButtonRect.description, ChangeButtonRect.usage, ChangeButtonRect.Execute);
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

        private static class ChangeTestNumber
        {
            public static readonly string command = "num";
            public static readonly string description = "Changes this specific test value.";
            public static readonly string usage = "num [value]";

            public static string Execute(params string[] args)
            {
                if (args.Length < 1 || args.Length > 1) return "Invalid entry, see usage notes.";

                if (!int.TryParse(args[0], out int num))
                    return string.Format("`{0}` is not a number, please use a number for [value].", args[0]);

                if (num <= 100 && num >= 0)
                {
                    AOPotionCraftingWindow.Instance.testNum1 = num;
                    AOPotionCraftingWindow.Instance.UpdatePanels();
                    return string.Format("Test Number Adjusted To: {0}", num);
                }
                else
                    return "Error: Something went wrong.";
            }
        }

        private static class ChangeButtonRect
        {
            public static readonly string command = "butt";
            public static readonly string description = "Changes the dimensions of this GUI button.";
            public static readonly string usage = "butt [butt#] [x] [y] [w] [h]";

            public static string Execute(params string[] args)
            {
                if (args.Length < 5 || args.Length > 5) return "Invalid entry, see usage notes.";

                if (!int.TryParse(args[0], out int buttNum))
                    return string.Format("`{0}` is not a number, please use a number for [butt#].", args[0]);
                if (!int.TryParse(args[1], out int x))
                    return string.Format("`{0}` is not a number, please use a number for [x].", args[1]);
                if (!int.TryParse(args[2], out int y))
                    return string.Format("`{0}` is not a number, please use a number for [y].", args[2]);
                if (!int.TryParse(args[3], out int w))
                    return string.Format("`{0}` is not a number, please use a number for [w].", args[3]);
                if (!int.TryParse(args[4], out int h))
                    return string.Format("`{0}` is not a number, please use a number for [h].", args[4]);

                if (buttNum == 1)
                    AOPotionCraftingWindow.butt1 = new Rect(x, y, w, h);
                else if (buttNum == 2)
                    AOPotionCraftingWindow.butt2 = new Rect(x, y, w, h);
                else if (buttNum == 3)
                    AOPotionCraftingWindow.butt3 = new Rect(x, y, w, h);
                else
                    return "Error: Something went wrong.";
                AOPotionCraftingWindow.Instance.UpdatePanels();
                return string.Format("Button {0} Rect Adjusted.", buttNum);
            }
        }
    }
}
