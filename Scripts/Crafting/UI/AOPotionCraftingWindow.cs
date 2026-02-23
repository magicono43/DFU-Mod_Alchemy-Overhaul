using UnityEngine;
using DaggerfallWorkshop.Game.UserInterface;
using DaggerfallWorkshop.Game.Entity;
using DaggerfallWorkshop.Game.Items;
using System.Collections.Generic;
using DaggerfallConnect.Arena2;
using System;
using System.Linq;
using AlchemyOverhaul;

namespace DaggerfallWorkshop.Game.UserInterfaceWindows
{
    /// <summary>
    /// Implements Alchemy Overhaul's Custom Potion Crafting/Brewing Window.
    /// </summary>
    public class AOPotionCraftingWindow : DaggerfallPopupWindow
    {
        public static AOPotionCraftingWindow Instance;

        PlayerEntity player;

        PlayerEntity Player
        {
            get { return (player != null) ? player : player = GameManager.Instance.PlayerEntity; }
        }

        #region Testing Properties

        public static Rect butt1 = new Rect(0, 0, 0, 0);
        public static Rect butt2 = new Rect(0, 0, 0, 0);
        public static Rect butt3 = new Rect(0, 0, 0, 0);
        public float testNum1 = 100f;

        #endregion

        #region Constructors

        public AOPotionCraftingWindow(IUserInterfaceManager uiManager)
            : base(uiManager)
        {
            Instance = this;
        }

        #endregion

        #region UI Textures

        Texture2D localSearchBarTexture;
        Texture2D localInventoryPanelTexture;
        Texture2D primaryHoverTextPanelTexture;

        Texture2D alchemyToolsPanelTexture;
        Texture2D resultInfoPanelTexture;
        Texture2D ingredientInputPanelTexture;
        Texture2D brewButtonActiveTexture;
        Texture2D heatActiveIconTexture;
        Texture2D heatSettingButtonTexture;

        Texture2D miscInfoPanelTexture;
        Texture2D recipeSearchBarTexture;
        Texture2D recipeListPanelTexture;
        Texture2D helpButtonActiveTexture;
        Texture2D exitButtonTexture;

        Texture2D rightGreenUpArrowTexture;
        Texture2D rightGreenDownArrowTexture;
        Texture2D leftGreenUpArrowTexture;
        Texture2D leftGreenDownArrowTexture;
        Texture2D rightRedUpArrowTexture;
        Texture2D rightRedDownArrowTexture;
        Texture2D leftRedUpArrowTexture;
        Texture2D leftRedDownArrowTexture;
        Texture2D sortButtonBackgroundTexture;
        Texture2D sortButtonActiveBorderTexture;
        Texture2D sortIconCheckmarkTexture;
        Texture2D sortIconXmarkTexture;
        Texture2D sortIconPercentTexture;
        Texture2D sortIconAscendingTexture;
        Texture2D sortIconDescendingTexture;

        #endregion

        Panel leftMainPanel;
        Panel localSearchBarPanel;
        Panel localSortButtonOnePanel;
        Panel localSortButtonTwoPanel;
        Panel localSortButtonThreePanel;
        Panel localSortButtonFourPanel;
        Panel localSortButtonFivePanel;
        Panel localSortButtonSixPanel;
        Panel localInventoryPanel;
        Panel primaryHoverTextPanel;

        Panel middleMainPanel;
        Panel alchemyToolsPanel;
        Panel resultInfoPanel;
        Panel ingredientInputPanel;
        Panel brewButtonActivePanel;
        Panel heatActiveIconPanel;
        Panel heatSettingButtonPanel;

        Panel rightMainPanel;
        Panel miscInfoPanel;
        Panel recipeSearchBarPanel;
        Panel recipeSortButtonOnePanel;
        Panel recipeSortButtonTwoPanel;
        Panel recipeSortButtonThreePanel;
        Panel recipeSortButtonFourPanel;
        Panel recipeSortButtonFivePanel;
        Panel recipeListPanel;
        Panel helpButtonActivePanel;
        Panel exitButtonPanel;

        Panel rightGreenUpArrowPanel;
        Panel rightGreenDownArrowPanel;
        Panel leftGreenUpArrowPanel;
        Panel leftGreenDownArrowPanel;
        Panel rightRedUpArrowPanel;
        Panel rightRedDownArrowPanel;
        Panel leftRedUpArrowPanel;
        Panel leftRedDownArrowPanel;
        Panel sortButtonBackgroundPanel;
        Panel sortButtonActiveBorderPanel;
        Panel sortIconCheckmarkPanel;
        Panel sortIconXmarkPanel;
        Panel sortIconPercentPanel;
        Panel sortIconAscendingPanel;
        Panel sortIconDescendingPanel;

        Button exitButton;

        AOBrewingLocalItemListScroller localAOItemListScroller;

        ItemCollection localItems = null;
        List<DaggerfallUnityItem> localItemsFiltered = new List<DaggerfallUnityItem>();

        protected override void Setup()
        {
            base.Setup();

            // Load textures
            LoadTextures();

            // This makes the background "transparent" instead of a blank black screen when opening this window.
            ParentPanel.BackgroundColor = ScreenDimColor;

            // Setup native panel background
            NativePanel.BackgroundColor = new Color32(0, 0, 0, 170);

            //SetupChestChoiceButtons();

            localItems = Player.Items;

            SetupTestPanelPositions();
            SetupTextPanelBackgroundTextures();
            SetupLocalItemListScroller();
            SetupButtons();
        }

        protected virtual void LoadTextures()
        {
            localSearchBarTexture = AlchemyOverhaulMain.Instance.BrewingLocalSearchBarTexture;
            localInventoryPanelTexture = AlchemyOverhaulMain.Instance.BrewingLocalInventoryPanelTexture;
            primaryHoverTextPanelTexture = AlchemyOverhaulMain.Instance.BrewingPrimaryHoverTextPanelTexture;

            alchemyToolsPanelTexture = AlchemyOverhaulMain.Instance.BrewingAlchemyToolsPanelTexture;
            resultInfoPanelTexture = AlchemyOverhaulMain.Instance.BrewingResultInfoPanelTexture;
            ingredientInputPanelTexture = AlchemyOverhaulMain.Instance.BrewingIngredientInputPanelTexture;
            brewButtonActiveTexture = AlchemyOverhaulMain.Instance.BrewingBrewButtonActiveTexture;
            heatActiveIconTexture = AlchemyOverhaulMain.Instance.BrewingHeatActiveIconTexture;
            heatSettingButtonTexture = AlchemyOverhaulMain.Instance.BrewingHeatSettingButtonTexture;

            miscInfoPanelTexture = AlchemyOverhaulMain.Instance.BrewingMiscInfoPanelTexture;
            recipeSearchBarTexture = AlchemyOverhaulMain.Instance.BrewingRecipeSearchBarTexture;
            recipeListPanelTexture = AlchemyOverhaulMain.Instance.BrewingRecipeListPanelTexture;
            helpButtonActiveTexture = AlchemyOverhaulMain.Instance.BrewingHelpButtonActiveTexture;
            exitButtonTexture = AlchemyOverhaulMain.Instance.BrewingExitButtonTexture;

            rightGreenUpArrowTexture = AlchemyOverhaulMain.Instance.ExtraRightGreenUpArrowTexture;
            rightGreenDownArrowTexture = AlchemyOverhaulMain.Instance.ExtraRightGreenDownArrowTexture;
            leftGreenUpArrowTexture = AlchemyOverhaulMain.Instance.ExtraLeftGreenUpArrowTexture;
            leftGreenDownArrowTexture = AlchemyOverhaulMain.Instance.ExtraLeftGreenDownArrowTexture;
            rightRedUpArrowTexture = AlchemyOverhaulMain.Instance.ExtraRightRedUpArrowTexture;
            rightRedDownArrowTexture = AlchemyOverhaulMain.Instance.ExtraRightRedDownArrowTexture;
            leftRedUpArrowTexture = AlchemyOverhaulMain.Instance.ExtraLeftRedUpArrowTexture;
            leftRedDownArrowTexture = AlchemyOverhaulMain.Instance.ExtraLeftRedDownArrowTexture;
            sortButtonBackgroundTexture = AlchemyOverhaulMain.Instance.SortButtonBackgroundTexture;
            sortButtonActiveBorderTexture = AlchemyOverhaulMain.Instance.SortButtonActiveBorderTexture;
            sortIconCheckmarkTexture = AlchemyOverhaulMain.Instance.SortIconCheckmarkTexture;
            sortIconXmarkTexture = AlchemyOverhaulMain.Instance.SortIconXmarkTexture;
            sortIconPercentTexture = AlchemyOverhaulMain.Instance.SortIconPercentTexture;
            sortIconAscendingTexture = AlchemyOverhaulMain.Instance.SortIconAscendingTexture;
            sortIconDescendingTexture = AlchemyOverhaulMain.Instance.SortIconDescendingTexture;
        }

        protected void SetupTestPanelPositions()
        {
            leftMainPanel = DaggerfallUI.AddPanel(new Rect(0, 0, 100, 200), NativePanel);
            localSearchBarPanel = DaggerfallUI.AddPanel(new Rect(0, 0, 100, 9), leftMainPanel);
            localSortButtonOnePanel = DaggerfallUI.AddPanel(new Rect(0, 10, 15, 12), leftMainPanel);
            localSortButtonTwoPanel = DaggerfallUI.AddPanel(new Rect(15, 10, 15, 12), leftMainPanel);
            localSortButtonThreePanel = DaggerfallUI.AddPanel(new Rect(30, 10, 15, 12), leftMainPanel);
            localSortButtonFourPanel = DaggerfallUI.AddPanel(new Rect(45, 10, 15, 12), leftMainPanel);
            localSortButtonFivePanel = DaggerfallUI.AddPanel(new Rect(60, 10, 15, 12), leftMainPanel);
            localSortButtonSixPanel = DaggerfallUI.AddPanel(new Rect(75, 10, 15, 12), leftMainPanel);
            localInventoryPanel = DaggerfallUI.AddPanel(new Rect(0, 22, 100, 110), leftMainPanel);
            primaryHoverTextPanel = DaggerfallUI.AddPanel(new Rect(0, 133, 100, 67), leftMainPanel);

            middleMainPanel = DaggerfallUI.AddPanel(new Rect(118, 0, 101, 200), NativePanel);
            alchemyToolsPanel = DaggerfallUI.AddPanel(new Rect(0, 6, 101, 22), middleMainPanel);
            resultInfoPanel = DaggerfallUI.AddPanel(new Rect(0, 32, 101, 63), middleMainPanel);
            ingredientInputPanel = DaggerfallUI.AddPanel(new Rect(0, 99, 101, 69), middleMainPanel);
            brewButtonActivePanel = DaggerfallUI.AddPanel(new Rect(33, 123, 35, 13), middleMainPanel);
            heatActiveIconPanel = DaggerfallUI.AddPanel(new Rect(46, 163, 9, 15), middleMainPanel);
            heatSettingButtonPanel = DaggerfallUI.AddPanel(new Rect(33, 180, 34, 14), middleMainPanel);

            rightMainPanel = DaggerfallUI.AddPanel(new Rect(237, 0, 83, 200), NativePanel);
            miscInfoPanel = DaggerfallUI.AddPanel(new Rect(0, 0, 83, 87), rightMainPanel);
            recipeSearchBarPanel = DaggerfallUI.AddPanel(new Rect(0, 92, 83, 9), rightMainPanel);
            recipeSortButtonOnePanel = DaggerfallUI.AddPanel(new Rect(8, 102, 15, 12), rightMainPanel);
            recipeSortButtonTwoPanel = DaggerfallUI.AddPanel(new Rect(23, 102, 15, 12), rightMainPanel);
            recipeSortButtonThreePanel = DaggerfallUI.AddPanel(new Rect(38, 102, 15, 12), rightMainPanel);
            recipeSortButtonFourPanel = DaggerfallUI.AddPanel(new Rect(53, 102, 15, 12), rightMainPanel);
            recipeSortButtonFivePanel = DaggerfallUI.AddPanel(new Rect(68, 102, 15, 12), rightMainPanel);
            recipeListPanel = DaggerfallUI.AddPanel(new Rect(0, 114, 83, 65), rightMainPanel);
            helpButtonActivePanel = DaggerfallUI.AddPanel(new Rect(0, 185, 35, 15), rightMainPanel);
            exitButtonPanel = DaggerfallUI.AddPanel(new Rect(48, 185, 35, 15), rightMainPanel);
        }

        protected void SetupTextPanelBackgroundTextures()
        {
            localSearchBarPanel.BackgroundTexture = localSearchBarTexture;
            localSortButtonOnePanel.BackgroundTexture = sortButtonBackgroundTexture;
            localSortButtonTwoPanel.BackgroundTexture = sortButtonBackgroundTexture;
            localSortButtonThreePanel.BackgroundTexture = sortButtonBackgroundTexture;
            localSortButtonFourPanel.BackgroundTexture = sortButtonBackgroundTexture;
            localSortButtonFivePanel.BackgroundTexture = sortButtonBackgroundTexture;
            localSortButtonSixPanel.BackgroundTexture = sortButtonBackgroundTexture;
            localInventoryPanel.BackgroundTexture = localInventoryPanelTexture;
            primaryHoverTextPanel.BackgroundTexture = primaryHoverTextPanelTexture;

            alchemyToolsPanel.BackgroundTexture = alchemyToolsPanelTexture;
            resultInfoPanel.BackgroundTexture = resultInfoPanelTexture;
            ingredientInputPanel.BackgroundTexture = ingredientInputPanelTexture;
            brewButtonActivePanel.BackgroundTexture = brewButtonActiveTexture;
            heatActiveIconPanel.BackgroundTexture = heatActiveIconTexture;
            heatSettingButtonPanel.BackgroundTexture = heatSettingButtonTexture;

            miscInfoPanel.BackgroundTexture = miscInfoPanelTexture;
            recipeSearchBarPanel.BackgroundTexture = recipeSearchBarTexture;
            recipeSortButtonOnePanel.BackgroundTexture = sortButtonBackgroundTexture;
            recipeSortButtonTwoPanel.BackgroundTexture = sortButtonBackgroundTexture;
            recipeSortButtonThreePanel.BackgroundTexture = sortButtonBackgroundTexture;
            recipeSortButtonFourPanel.BackgroundTexture = sortButtonBackgroundTexture;
            recipeSortButtonFivePanel.BackgroundTexture = sortButtonBackgroundTexture;
            recipeListPanel.BackgroundTexture = recipeListPanelTexture;
            helpButtonActivePanel.BackgroundTexture = helpButtonActiveTexture;
            exitButtonPanel.BackgroundTexture = exitButtonTexture;
        }

        private void SetupLocalItemListScroller()
        {
            localAOItemListScroller = new AOBrewingLocalItemListScroller(defaultToolTip)
            {
                Position = new Vector2(0, 0),
                Size = new Vector2(100, 110),
                //BackgroundColourHandler = ItemBackgroundColourHandler,
                //ForegroundAnimationHandler = MagicItemForegroundAnimationHander,
                //ForegroundAnimationDelay = magicAnimationDelay
            };

            localInventoryPanel.Components.Clear();

            localInventoryPanel.Components.Add(localAOItemListScroller);

            localAOItemListScroller.OnItemClick += LocalItemListScroller_OnItemLeftClick;
            //localAOItemListScroller.OnItemRightClick += LocalItemListScroller_OnItemRightClick;
            //localAOItemListScroller.OnItemMiddleClick += LocalItemListScroller_OnItemMiddleClick;
            if (primaryHoverTextPanel != null) { localAOItemListScroller.OnItemHover += LocalItemListScroller_OnHover; }

            FilterLocalItems();
            //SortBasedOnButtonStates();
            localAOItemListScroller.Items = localItemsFiltered;
        }

        private void FilterLocalItems()
        {
            // Clear current references
            localItemsFiltered.Clear();

            if (localItems != null)
            {
                // Add items to list
                for (int i = 0; i < localItems.Count; i++)
                {
                    DaggerfallUnityItem item = localItems.GetItem(i);
                    // Add if item is an ingredient
                    if (item.IsIngredient)
                    {
                        AddLocalItem(item);
                    }
                }
            }
        }

        private void AddLocalItem(DaggerfallUnityItem item)
        {
            localItemsFiltered.Add(item);
        }

        /*
        private void SortBasedOnButtonStates()
        {
            if (percentItemConditionSortState == 1)
            {
                localItemsFiltered.Sort((a, b) =>
                {
                    int aPercent = a.ConditionPercentage;
                    int bPercent = b.ConditionPercentage;
                    return bPercent.CompareTo(aPercent); // descending i.e. highest to lowest
                });
            }
            else if (percentItemConditionSortState == 2)
            {
                localItemsFiltered.Sort((a, b) =>
                {
                    int aPercent = a.ConditionPercentage;
                    int bPercent = b.ConditionPercentage;
                    return aPercent.CompareTo(bPercent); // ascending i.e. lowest to highest
                });
            }

            if (itemEffectivenessSortState == 1)
            {
                localItemsFiltered.Sort((item1, item2) => CompareItemsByEffectiveness(item1, item2, true)); // descending
            }
            else if (itemEffectivenessSortState == 2)
            {
                localItemsFiltered.Sort((item1, item2) => CompareItemsByEffectiveness(item1, item2, false)); // ascending
            }
        }
        */

        protected void SetupButtons()
        {
            // Exit button
            exitButton = DaggerfallUI.AddButton(new Rect(0, 0, 35, 15), exitButtonPanel);
            exitButton.OnMouseClick += ExitButton_OnMouseClick;
            exitButton.ClickSound = DaggerfallUI.Instance.GetAudioClip(SoundClips.ButtonClick);
        }

        public static TextLabel CreateCenteredTextLabel(string text, Vector2 position, int maxWidth, Panel parentPanel, float textScale = 1, Color32? color = null)
        {
            if (color == null) { color = DaggerfallUI.DaggerfallDefaultTextColor; }

            TextLabel label = DaggerfallUI.AddTextLabel(DaggerfallUI.DefaultFont, position, text, parentPanel);
            label.TextColor = (Color)color;
            label.MaxWidth = maxWidth;
            label.TextScale = textScale;
            label.HorizontalAlignment = HorizontalAlignment.Center;
            return label;
        }

        /*

        private void SetupLocalAOItemListScroller(bool rightSide, EquipSlots slot)
        {
            localAOItemListScroller = new AOItemListScroller(defaultToolTip, slot, rightSide)
            {
                Position = new Vector2(0, 0),
                Size = new Vector2(54, 176),
                //BackgroundColourHandler = ItemBackgroundColourHandler,
                //ForegroundAnimationHandler = MagicItemForegroundAnimationHander,
                //ForegroundAnimationDelay = magicAnimationDelay
            };

            rightExtraEquipPanel.Components.Clear();
            leftExtraEquipPanel.Components.Clear();

            if (rightSide) { rightExtraEquipPanel.Components.Add(localAOItemListScroller); }
            else { leftExtraEquipPanel.Components.Add(localAOItemListScroller); }

            localAOItemListScroller.OnItemClick += LocalItemListScroller_OnItemLeftClick;
            //localAOItemListScroller.OnItemRightClick += LocalItemListScroller_OnItemRightClick;
            //localAOItemListScroller.OnItemMiddleClick += LocalItemListScroller_OnItemMiddleClick;
            if (extraInfoTextPanel != null) { localAOItemListScroller.OnItemHover += LocalItemListScroller_OnHover; }

            FilterLocalItems(slot);
            SortBasedOnButtonStates();
            localAOItemListScroller.Items = localItemsFiltered;
        }

        private void FilterLocalItems(EquipSlots slot)
        {
            // Clear current references
            localItemsFiltered.Clear();

            if (localItems != null)
            {
                // Add items to list
                for (int i = 0; i < localItems.Count; i++)
                {
                    DaggerfallUnityItem item = localItems.GetItem(i);
                    // Add if not equipped
                    if (!item.IsEquipped)
                    {
                        if (restrictedItemFilterState)
                        {
                            if (ProhibitedItemCheck(item)) { continue; }
                        }

                        AddLocalItem(item, slot);
                    }
                }
            }
        }

        private void AddLocalItem(DaggerfallUnityItem item, EquipSlots slot)
        {
            bool isWeaponOrArmor = (item.ItemGroup == ItemGroups.Weapons || item.ItemGroup == ItemGroups.Armor);

            if (isWeaponOrArmor)
            {
                ItemHands whichHand = ItemEquipTable.GetItemHands(item);

                if (slot == EquipSlots.LeftHand)
                {
                    if (whichHand == ItemHands.LeftOnly)
                    {
                        localItemsFiltered.Add(item);
                        return;
                    }
                }

                if (slot == EquipSlots.RightHand)
                {
                    if (whichHand == ItemHands.Either || whichHand == ItemHands.Both || whichHand == ItemHands.RightOnly)
                    {
                        localItemsFiltered.Add(item);
                        return;
                    }
                }

                if (slot == Player.ItemEquipTable.GetEquipSlot(item))
                {
                    localItemsFiltered.Add(item);
                }
            }
        }

        protected virtual void LocalItemListScroller_OnItemLeftClick(DaggerfallUnityItem item)
        {
            LocalItemListScroller_OnItemClick(item);
        }

        protected virtual void LocalItemListScroller_OnItemClick(DaggerfallUnityItem item)
        {
            EquipItem(item);
        }

        public void RefreshEquipScreen(EquipSlots slot = EquipSlots.None)
        {
            if (localAOItemListScroller != null)
            {
                if (slot == EquipSlots.None)
                {
                    slot = localAOItemListScroller.AssociatedSlot;
                }

                if (slot == localAOItemListScroller.AssociatedSlot)
                {
                    FilterLocalItems(slot);
                    SortBasedOnButtonStates();
                    localAOItemListScroller.Items = localItemsFiltered;
                }
            }

            switch (slot)
            {
                case EquipSlots.Head:
                    headItemIconPanel.Components.Clear();
                    DrawEquipItemToIconPanel(headItemIconPanel, EquipSlots.Head);
                    AddItemDurabilityBar(headItemDurabilityBarPanel, EquipSlots.Head);
                    headItemTextPanel.Components.Clear();
                    AddItemTextLabels(headItemTextPanel, EquipSlots.Head, "Head"); break;
                case EquipSlots.RightArm:
                    rightArmItemIconPanel.Components.Clear();
                    DrawEquipItemToIconPanel(rightArmItemIconPanel, EquipSlots.RightArm);
                    AddItemDurabilityBar(rightArmItemDurabilityBarPanel, EquipSlots.RightArm);
                    rightArmItemTextPanel.Components.Clear();
                    AddItemTextLabels(rightArmItemTextPanel, EquipSlots.RightArm, "Right Arm"); break;
                case EquipSlots.ChestArmor:
                    chestItemIconPanel.Components.Clear();
                    DrawEquipItemToIconPanel(chestItemIconPanel, EquipSlots.ChestArmor);
                    AddItemDurabilityBar(chestItemDurabilityBarPanel, EquipSlots.ChestArmor);
                    chestItemTextPanel.Components.Clear();
                    AddItemTextLabels(chestItemTextPanel, EquipSlots.ChestArmor, "Chest"); break;
                case EquipSlots.Gloves:
                    glovesItemIconPanel.Components.Clear();
                    DrawEquipItemToIconPanel(glovesItemIconPanel, EquipSlots.Gloves);
                    AddItemDurabilityBar(glovesItemDurabilityBarPanel, EquipSlots.Gloves);
                    glovesItemTextPanel.Components.Clear();
                    AddItemTextLabels(glovesItemTextPanel, EquipSlots.Gloves, "Gloves"); break;
                case EquipSlots.LeftArm:
                    leftArmItemIconPanel.Components.Clear();
                    DrawEquipItemToIconPanel(leftArmItemIconPanel, EquipSlots.LeftArm);
                    AddItemDurabilityBar(leftArmItemDurabilityBarPanel, EquipSlots.LeftArm);
                    leftArmItemTextPanel.Components.Clear();
                    AddItemTextLabels(leftArmItemTextPanel, EquipSlots.LeftArm, "Left Arm"); break;
                case EquipSlots.LegsArmor:
                    legsItemIconPanel.Components.Clear();
                    DrawEquipItemToIconPanel(legsItemIconPanel, EquipSlots.LegsArmor);
                    AddItemDurabilityBar(legsItemDurabilityBarPanel, EquipSlots.LegsArmor);
                    legsItemTextPanel.Components.Clear();
                    AddItemTextLabels(legsItemTextPanel, EquipSlots.LegsArmor, "Legs"); break;
                case EquipSlots.Feet:
                    bootsItemIconPanel.Components.Clear();
                    DrawEquipItemToIconPanel(bootsItemIconPanel, EquipSlots.Feet);
                    AddItemDurabilityBar(bootsItemDurabilityBarPanel, EquipSlots.Feet);
                    bootsItemTextPanel.Components.Clear();
                    AddItemTextLabels(bootsItemTextPanel, EquipSlots.Feet, "Feet"); break;
                case EquipSlots.RightHand:
                case EquipSlots.LeftHand:
                    rightHandItemIconPanel.Components.Clear();
                    DrawEquipItemToIconPanel(rightHandItemIconPanel, EquipSlots.RightHand);
                    AddItemDurabilityBar(rightHandItemDurabilityBarPanel, EquipSlots.RightHand);
                    rightHandItemTextPanel.Components.Clear();
                    AddItemTextLabels(rightHandItemTextPanel, EquipSlots.RightHand, "Right Hand");
                    leftHandItemIconPanel.Components.Clear();
                    DrawEquipItemToIconPanel(leftHandItemIconPanel, EquipSlots.LeftHand);
                    AddItemDurabilityBar(leftHandItemDurabilityBarPanel, EquipSlots.LeftHand);
                    leftHandItemTextPanel.Components.Clear();
                    AddItemTextLabels(leftHandItemTextPanel, EquipSlots.LeftHand, "Left Hand"); break;
                default: return;
            }

            UpdateItemInfoPanel(null);
        }

        protected virtual void LocalItemListScroller_OnHover(DaggerfallUnityItem item)
        {
            ItemListScroller_OnHover(item);
            //RaiseOnItemHoverEvent(item, ItemHoverLocation.LocalList);
        }

        protected virtual void ItemListScroller_OnHover(DaggerfallUnityItem item)
        {
            // Update the info panel if used
            if (extraInfoTextPanel != null)
            {
                UpdateItemInfoPanel(item);
            }

            if (rightItemComparisonPanel.Enabled == true)
            {
                if (rightComparisonMainTextPanel != null)
                {
                    UpdateItemComparisonPanel(item, true);
                }
            }

            if (leftItemComparisonPanel.Enabled == true)
            {
                if (leftComparisonMainTextPanel != null)
                {
                    UpdateItemComparisonPanel(item, false);
                }
            }
        }

        */

        protected virtual void LocalItemListScroller_OnItemLeftClick(DaggerfallUnityItem item)
        {
            LocalItemListScroller_OnItemClick(item);
        }

        protected virtual void LocalItemListScroller_OnItemClick(DaggerfallUnityItem item)
        {
            //EquipItem(item);
        }

        protected virtual void LocalItemListScroller_OnHover(DaggerfallUnityItem item)
        {
            ItemListScroller_OnHover(item);
            //RaiseOnItemHoverEvent(item, ItemHoverLocation.LocalList);
        }

        protected virtual void ItemListScroller_OnHover(DaggerfallUnityItem item)
        {
            // Update the info panel
            if (primaryHoverTextPanel != null)
            {
                //UpdateItemInfoPanel(item);
            }
        }

        private void ExitButton_OnMouseClick(BaseScreenComponent sender, Vector2 position)
        {
            CloseWindow();
        }

        public void UpdatePanels()
        {
            //AddItemDurabilityBar(headItemDurabilityBarPanel, EquipSlots.Head, 0);

            //rightSortButtonsPanel.Position = new Vector2(butt1.x, butt1.y);
            //rightSortButtonsPanel.Size = new Vector2(butt1.width, butt1.height);

            //secondCategoryPanel.Position = new Vector2(butt2.x, butt2.y);
            //secondCategoryPanel.Size = new Vector2(butt2.width, butt2.height);
        }
    }
}
