// Project:         Alchemy Overhaul mod for Daggerfall Unity (http://www.dfworkshop.net)
// Copyright:       Copyright (C) 2026 Kirk.O
// License:         MIT License (http://www.opensource.org/licenses/mit-license.php)
// Author:          Kirk.O
// Created On: 	    1/13/2026, 10:00 PM
// Last Edit:		2/8/2026, 7:15 PM
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
        public Texture2D EquipInfoGUITexture;
        public Texture2D EquipInfoSlotBorderTexture;
        public Texture2D EquipInfoExtraRightPanelTexture;
        public Texture2D EquipInfoExtraLeftPanelTexture;
        public Texture2D EquipInfoExtraRightGreenUpArrowTexture;
        public Texture2D EquipInfoExtraRightGreenDownArrowTexture;
        public Texture2D EquipInfoExtraLeftGreenUpArrowTexture;
        public Texture2D EquipInfoExtraLeftGreenDownArrowTexture;
        public Texture2D EquipInfoExtraRightRedUpArrowTexture;
        public Texture2D EquipInfoExtraRightRedDownArrowTexture;
        public Texture2D EquipInfoExtraLeftRedUpArrowTexture;
        public Texture2D EquipInfoExtraLeftRedDownArrowTexture;
        public Texture2D EquipInfoRightComparisonPanelTexture;
        public Texture2D EquipInfoLeftComparisonPanelTexture;
        public Texture2D EquipInfoSortButtonBackgroundTexture;
        public Texture2D EquipInfoSortButtonActiveBorderTexture;
        public Texture2D EquipInfoSortIconCheckmarkTexture;
        public Texture2D EquipInfoSortIconXmarkTexture;
        public Texture2D EquipInfoSortIconPercentTexture;
        public Texture2D EquipInfoSortIconSwordTexture;
        public Texture2D EquipInfoSortIconShieldTexture;
        public Texture2D EquipInfoSortIconAscendingTexture;
        public Texture2D EquipInfoSortIconDescendingTexture;

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
                if (DaggerfallUI.Instance.UserInterfaceManager.TopWindow is AOInfoWindow)
                {
                    (DaggerfallUI.Instance.UserInterfaceManager.TopWindow as AOInfoWindow).CloseWindow();
                }
                else if (!GameManager.IsGamePaused && GameManager.Instance.PlayerObject != null && DaggerfallUI.UIManager.WindowCount <= 0)
                {
                    AOInfoWindow infoWindow = new AOInfoWindow(DaggerfallUI.UIManager);
                    DaggerfallUI.UIManager.PushWindow(infoWindow);
                }
            }
        }

        private void LoadTextures() // Example taken from Penwick Papers Mod
        {
            ModManager modManager = ModManager.Instance;
            bool success = true;

            success &= modManager.TryGetAsset("PCO_Equip_Info_GUI_1", false, out EquipInfoGUITexture);
            success &= modManager.TryGetAsset("Slot_Selected_Border_1", false, out EquipInfoSlotBorderTexture);
            success &= modManager.TryGetAsset("Extra_Panel_Right_1", false, out EquipInfoExtraRightPanelTexture);
            success &= modManager.TryGetAsset("Extra_Panel_Left_1", false, out EquipInfoExtraLeftPanelTexture);
            success &= modManager.TryGetAsset("Right_Green_Arrow_Up_1", false, out EquipInfoExtraRightGreenUpArrowTexture);
            success &= modManager.TryGetAsset("Right_Green_Arrow_Down_1", false, out EquipInfoExtraRightGreenDownArrowTexture);
            success &= modManager.TryGetAsset("Left_Green_Arrow_Up_1", false, out EquipInfoExtraLeftGreenUpArrowTexture);
            success &= modManager.TryGetAsset("Left_Green_Arrow_Down_1", false, out EquipInfoExtraLeftGreenDownArrowTexture);
            success &= modManager.TryGetAsset("Right_Red_Arrow_Up_1", false, out EquipInfoExtraRightRedUpArrowTexture);
            success &= modManager.TryGetAsset("Right_Red_Arrow_Down_1", false, out EquipInfoExtraRightRedDownArrowTexture);
            success &= modManager.TryGetAsset("Left_Red_Arrow_Up_1", false, out EquipInfoExtraLeftRedUpArrowTexture);
            success &= modManager.TryGetAsset("Left_Red_Arrow_Down_1", false, out EquipInfoExtraLeftRedDownArrowTexture);
            success &= modManager.TryGetAsset("Item_Comparison_Panel_Right", false, out EquipInfoRightComparisonPanelTexture);
            success &= modManager.TryGetAsset("Item_Comparison_Panel_Left", false, out EquipInfoLeftComparisonPanelTexture);
            success &= modManager.TryGetAsset("Sort_Button_Background_And_Border_1", false, out EquipInfoSortButtonBackgroundTexture);
            success &= modManager.TryGetAsset("Sort_Button_Active_Border_1", false, out EquipInfoSortButtonActiveBorderTexture);
            success &= modManager.TryGetAsset("Sort_Button_Icon_Checkmark_1", false, out EquipInfoSortIconCheckmarkTexture);
            success &= modManager.TryGetAsset("Sort_Button_Icon_X_Mark_1", false, out EquipInfoSortIconXmarkTexture);
            success &= modManager.TryGetAsset("Sort_Button_Icon_Percent_1", false, out EquipInfoSortIconPercentTexture);
            success &= modManager.TryGetAsset("Sort_Button_Icon_Sword_1", false, out EquipInfoSortIconSwordTexture);
            success &= modManager.TryGetAsset("Sort_Button_Icon_Shield_1", false, out EquipInfoSortIconShieldTexture);
            success &= modManager.TryGetAsset("Sort_Button_Icon_Ascending_Arrow_1", false, out EquipInfoSortIconAscendingTexture);
            success &= modManager.TryGetAsset("Sort_Button_Icon_Descending_Arrow_1", false, out EquipInfoSortIconDescendingTexture);

            if (!success)
                throw new Exception("PhysicalCombatOverhaul: Missing texture asset");
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
                    AOInfoWindow.Instance.testNum1 = num;
                    AOInfoWindow.Instance.UpdatePanels();
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
                    AOInfoWindow.butt1 = new Rect(x, y, w, h);
                else if (buttNum == 2)
                    AOInfoWindow.butt2 = new Rect(x, y, w, h);
                else if (buttNum == 3)
                    AOInfoWindow.butt3 = new Rect(x, y, w, h);
                else
                    return "Error: Something went wrong.";
                AOInfoWindow.Instance.UpdatePanels();
                return string.Format("Button {0} Rect Adjusted.", buttNum);
            }
        }
    }
}
