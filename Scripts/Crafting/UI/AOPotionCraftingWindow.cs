using UnityEngine;
using DaggerfallWorkshop.Game.UserInterface;
using DaggerfallWorkshop.Game.Entity;
using DaggerfallWorkshop.Game.Items;
using System.Collections.Generic;
using DaggerfallConnect.Arena2;
using System;
using System.Linq;
using AlchemyOverhaul;
using AlchemyOverhaul.Ingredients.Database;
using AlchemyOverhaul.Ingredients.Definitions;
using AlchemyOverhaul.Data.Definitions;
using AlchemyOverhaul.Effects;
using AlchemyOverhaul.Potions;
using AlchemyOverhaul.Data.Enums;
using AlchemyOverhaul.Systems;
using AlchemyOverhaul.Data.Runtime;
using AlchemyOverhaul.Crafting.UI;
using AlchemyOverhaul.Player.Skills;

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

        Texture2D unknownEffectIconTexture;

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

        Rect[] primaryInfoTextPanelRects = primaryHoverInfoTextPanelRects;
        Panel[] primaryHoverInfoTextPanels;
        Panel[] ingredientEffectImageIconPanels;
        Panel[] ingredientEffectInfoTextPanels;

        Rect[] brewingResultTextPanelRects = brewResultTextPanelRects;
        Panel[] brewingResultTextPanels;

        Panel heatSettingTextDisplayPanel;

        Button exitButton;
        Button reduceHeatButton;
        Button increaseHeatButton;
        Button brewButton;

        AOBrewingLocalItemListScroller localAOItemListScroller;

        Rect[] ingredientInputItemButtonRects = inputItemButtonRects;
        Button[] ingredientInputItemButtons;
        Panel[] ingredientInputItemImagePanels;

        Rect[] alchemyToolInputButtonRects = toolButtonRects;
        Button[] alchemyToolInputButtons;
        Panel[] alchemyToolItemImagePanels;
        Panel[] alchemyToolSlotDisabledPanels;

        static Rect[] inputItemButtonRects = new Rect[]
        {
            new Rect(26, 0, 23, 22), new Rect(52, 0, 23, 22), new Rect(0, 22, 23, 22), new Rect(78, 22, 23, 22), new Rect(0, 47, 23, 22), new Rect(78, 47, 23, 22), new Rect(39, 39, 23, 22)
        };

        static Rect[] primaryHoverInfoTextPanelRects = new Rect[]
        {
            new Rect(5, 5, 44, 16), new Rect(51, 5, 44, 16), new Rect(5, 22, 44, 16), new Rect(51, 22, 44, 16), new Rect(5, 39, 44, 16), new Rect(51, 39, 44, 16), new Rect(5, 56, 90, 7)
        };

        /*
        static Rect[] primaryHoverInfoTextPanelRects = new Rect[]
        {
            new Rect(6, 6, 43, 13), new Rect(51, 6, 43, 13), new Rect(6, 21, 43, 13), new Rect(51, 21, 43, 13), new Rect(6, 36, 43, 13), new Rect(51, 36, 43, 13), new Rect(6, 51, 88, 11)
        };
        */

        static Rect[] toolButtonRects = new Rect[]
        {
            new Rect(0, 0, 23, 22), new Rect(26, 0, 23, 22), new Rect(52, 0, 23, 22), new Rect(78, 0, 23, 22)
        };

        static Rect[] brewResultTextPanelRects = new Rect[]
        {
            new Rect(5, 5, 91, 10), new Rect(5, 16, 91, 10), new Rect(5, 27, 91, 10), new Rect(5, 38, 91, 10), new Rect(5, 49, 91, 10)
        };

        ItemCollection localItems = new ItemCollection();
        List<DaggerfallUnityItem> localItemsFiltered = new List<DaggerfallUnityItem>();

        DaggerfallUnityItem[] brewingSlots = new DaggerfallUnityItem[7];
        DaggerfallUnityItem[] alchemyToolSlots = new DaggerfallUnityItem[4];
        bool[] alchemyToolActiveSlots = new bool[] { true, true, true, true};

        DaggerfallUnityItem hoveredItem;

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
            SetupIngredientInputPanels();
            SetupPrimaryHoverInfoText();
            SetupAlchemyToolPanels();
            SetupBrewingResultInfoText();
            SetupHeatSettingsPanels();
            SetupButtons();
        }

        public override void OnPush()
        {
            if (!IsSetup)
                return;

            Refresh();
        }

        public override void OnPop()
        {
            localItemsFiltered.Clear();
            FreeBrewingInputSlots();
        }

        protected virtual void Refresh()
        {
            /*
            // Update labels
            goldLabel.Text = GameManager.Instance.PlayerEntity.GetGoldAmount().ToString();

            // Add ingredient items to list and gather recipes - from inventory and wagon
            ingredients.Clear();
            List<DaggerfallUnityItem> recipeItems = new List<DaggerfallUnityItem>();
            foreach (ItemCollection playerItems in new ItemCollection[] { GameManager.Instance.PlayerEntity.Items, GameManager.Instance.PlayerEntity.WagonItems })
            {
                for (int i = 0; i < playerItems.Count; i++)
                {
                    DaggerfallUnityItem item = playerItems.GetItem(i);
                    if (item.IsIngredient && !item.IsEnchanted)
                        ingredients.AddItem(item.Clone());
                    else if (item.IsPotionRecipe)
                        recipeItems.Add(item);
                }
            }
            RefreshIngredientsList();
            ingredientsListScroller.Items = ingredientsList;

            // Clear cauldron and assign to scroller
            cauldron.Clear();
            cauldronListScroller.Items = cauldron;

            // Populate picker from recipe items
            recipes.Clear();
            recipePicker.ListBox.ClearItems();
            foreach (DaggerfallUnityItem recipeItem in recipeItems)
            {
                PotionRecipe potionRecipe = GameManager.Instance.EntityEffectBroker.GetPotionRecipe(recipeItem.PotionRecipeKey);
                if (!recipes.Contains(potionRecipe))
                    recipes.Add(potionRecipe);
            }
            recipes.Sort((x, y) => (x.DisplayName.CompareTo(y.DisplayName)));
            foreach (PotionRecipe potionRecipe in recipes)
                recipePicker.ListBox.AddItem(potionRecipe.DisplayName);
            */
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

            unknownEffectIconTexture = AlchemyOverhaulMain.Instance.UnknownEffectIconTexture;
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
                    if (item.IsIngredient && !item.IsEquipped && !item.IsEnchanted)
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

        protected void RefreshLocalItemsFilteredList()
        {
            localItemsFiltered.Clear();
            FilterLocalItems();
            localAOItemListScroller.Items = localItemsFiltered;
        }

        protected void SetupIngredientInputPanels()
        {
            ingredientInputItemButtons = new Button[7];
            ingredientInputItemImagePanels = new Panel[7];
            for (int i = 0; i < brewingSlots.Length; i++) { brewingSlots[i] = null; } // Set brewing input slots to null, just in case.

            for (int i = 0; i < 7; i++)
            {
                // Buttons (also handle highlight colours)
                ingredientInputItemButtons[i] = DaggerfallUI.AddButton(ingredientInputItemButtonRects[i], ingredientInputPanel);
                ingredientInputItemButtons[i].Name = i.ToString();
                ingredientInputItemButtons[i].Tag = null; // Use the tag to store reference to the DaggerfallUnityItem, if none than keep this null.
                ingredientInputItemButtons[i].ToolTip = defaultToolTip;
                ingredientInputItemButtons[i].ToolTipText = ""; // Make this the name of the item, and if not that, show the name of the slot, such as "Solvent" etc.
                ingredientInputItemButtons[i].BackgroundColor = Color.clear;
                ingredientInputItemButtons[i].OnMouseClick += BrewingInputItems_OnLeftClick; // Likely just clear this ingredient slot and return item to local inventory scroller.
                ingredientInputItemButtons[i].OnMouseEnter += BrewingInputItems_OnMouseEnter; // Likely just update the "Primary Info" hover text box and tooltip text.
                ingredientInputItemButtons[i].OnMouseLeave += BrewingInputItems_OnMouseLeave;

                // Icon image panel
                ingredientInputItemImagePanels[i] = DaggerfallUI.AddPanel(ingredientInputItemButtons[i], AutoSizeModes.ScaleToFit);
                ingredientInputItemImagePanels[i].HorizontalAlignment = HorizontalAlignment.Center;
                ingredientInputItemImagePanels[i].VerticalAlignment = VerticalAlignment.Middle;
                ingredientInputItemImagePanels[i].MaxAutoScale = 1f;
            }

            RefreshBrewingIngredientInputPanels();
        }

        protected void RefreshBrewingIngredientInputPanels()
        {
            // Update images and tooltips
            for (int i = 0; i < brewingSlots.Length; i++)
            {
                // Get item and image
                DaggerfallUnityItem item = brewingSlots[i];
                if (item == null) { ClearBrewingInputSlot(i); continue; }
                ImageData image = DaggerfallUnity.Instance.ItemHelper.GetInventoryImage(item);

                // Set image to button icon
                ingredientInputItemImagePanels[i].BackgroundTexture = image.texture;
                // Use texture size if base image size is zero (i.e. new images that are not present in classic data)
                if (image.width != 0 && image.height != 0)
                    ingredientInputItemImagePanels[i].Size = new Vector2(image.width, image.height);
                else
                    ingredientInputItemImagePanels[i].Size = new Vector2(image.texture.width, image.texture.height);

                // Set Tag to item object
                ingredientInputItemButtons[i].Tag = item;

                // Tooltip text
                ingredientInputItemButtons[i].ToolTipText = item.LongName;
            }
        }

        protected void ClearBrewingInputSlot(int slotIndex)
        {
            ingredientInputItemButtons[slotIndex].ToolTipText = GetBrewingInputSlotName(slotIndex);
            ingredientInputItemButtons[slotIndex].Tag = null;
            ingredientInputItemImagePanels[slotIndex].BackgroundTexture = null;
        }

        protected string GetBrewingInputSlotName(int slotIndex)
        {
            string name = "Ingredient Slot";
            switch(slotIndex)
            {
                case 0: name = "Ingredient 1"; break;
                case 1: name = "Ingredient 2"; break;
                case 2: name = "Ingredient 3"; break;
                case 3: name = "Ingredient 4"; break;
                case 4: name = "Ingredient 5"; break;
                case 5: name = "Ingredient 6"; break;
                case 6: name = "Solvent"; break;
            }
            return name;
        }

        protected void SetupPrimaryHoverInfoText()
        {
            int maxLineWidth = 29;
            float textScale = 0.8f;

            primaryHoverInfoTextPanels = new Panel[7];
            ingredientEffectImageIconPanels = new Panel[7];
            ingredientEffectInfoTextPanels = new Panel[7];

            for (int i = 0; i < 7; i++)
            {
                primaryHoverInfoTextPanels[i] = DaggerfallUI.AddPanel(primaryInfoTextPanelRects[i], primaryHoverTextPanel);
                primaryHoverInfoTextPanels[i].Name = i.ToString();
                primaryHoverInfoTextPanels[i].Tag = null;
                primaryHoverInfoTextPanels[i].BackgroundColor = new Color32(255, 0, 0, 110);

                if (i <= 4)
                {
                    primaryHoverInfoTextPanels[i].BackgroundColor = Color.clear;

                    ingredientEffectImageIconPanels[i] = DaggerfallUI.AddPanel(new Rect(0, 0, 16, 16), primaryHoverInfoTextPanels[i]);
                    //ingredientEffectImageIconPanels[i].BackgroundColor = new Color32(0, 0, 255, 160);
                    ingredientEffectImageIconPanels[i].BackgroundTexture = unknownEffectIconTexture;

                    ingredientEffectInfoTextPanels[i] = DaggerfallUI.AddPanel(new Rect(16, 0, 28, 16), primaryHoverInfoTextPanels[i]);
                    //ingredientEffectInfoTextPanels[i].BackgroundColor = new Color32(0, 255, 0, 160);

                    CreateCenteredTextLabel("Nothing Ever Happens", new Vector2(1, 1), maxLineWidth, ingredientEffectInfoTextPanels[i], textScale);
                }
                else if (i == 5)
                    ingredientEffectInfoTextPanels[i] = DaggerfallUI.AddPanel(new Rect(0, 0, 44, 16), primaryHoverInfoTextPanels[i]);
                else
                    ingredientEffectInfoTextPanels[i] = DaggerfallUI.AddPanel(new Rect(0, 0, 90, 7), primaryHoverInfoTextPanels[i]);
            }

            RefreshPrimaryHoverInfoPanel();
        }

        protected void RefreshPrimaryHoverInfoPanel()
        {
            IngredientDefinition def = null;
            DaggerfallUnityItem item = hoveredItem;
            if (item != null) { def = IngredientDatabase.Get(item.TemplateIndex); }

            for (int i = 0; i < primaryHoverInfoTextPanels.Length; i++)
            {
                ingredientEffectInfoTextPanels[i].Components.Clear();
                if (i <= 4 && def != null && def.PrimaryEffects.Count >= i + 1)
                {
                    string effectDisplayName = "Unknown";
                    EffectDefinition effectDef = null;
                    if (EffectDatabase.TryGet(def.PrimaryEffects[i].EffectId, out effectDef)) { effectDisplayName = effectDef.DisplayName; }
                    else {effectDisplayName = def.PrimaryEffects[i].EffectId; }
                    CreateCenteredTextLabel(effectDisplayName, new Vector2(1, 1), 29, ingredientEffectInfoTextPanels[i], 0.8f);
                }
            }

            // Work on this more later today, I'll want to eventually fix some of this weird logic to make it more simple to use on my side.

            // Maybe see if I can just make this a partial class, then split it all up into separate scripts using the same partial class.
            // That way I don't have to worry about all the instanced BS or whatever, and still have the visual organization atleast of stuff in different scripts, will see.

            // Next, now that I tested all the spell effects appear to be working at a basic level on potions, maybe I should do some cleanup stuff as said in the below line before going further.
            // Try to get some clean up and reorganization going on with this sort of mess. Don't want to get too far in when everything
            // is kind of looking like a confusing mess all scattered around seemingly arbitrarily.
        }

        protected void SetupAlchemyToolPanels()
        {
            alchemyToolInputButtons = new Button[4];
            alchemyToolItemImagePanels = new Panel[4];
            alchemyToolSlotDisabledPanels = new Panel[4];
            for (int i = 0; i < alchemyToolSlots.Length; i++) { alchemyToolSlots[i] = null; }

            for (int i = 0; i < 4; i++)
            {
                // Buttons (also handle highlight colours)
                alchemyToolInputButtons[i] = DaggerfallUI.AddButton(alchemyToolInputButtonRects[i], alchemyToolsPanel);
                alchemyToolInputButtons[i].Name = i.ToString();
                alchemyToolInputButtons[i].Tag = null; // Use the tag to store reference to the DaggerfallUnityItem, if none than keep this null.
                alchemyToolInputButtons[i].ToolTip = defaultToolTip;
                alchemyToolInputButtons[i].ToolTipText = ""; // Make this the name of the item, and if not that, show the name of the slot, such as "Solvent" etc.
                alchemyToolInputButtons[i].BackgroundColor = Color.clear;
                alchemyToolInputButtons[i].OnMouseClick += AlchemyTools_OnLeftClick;

                // Icon image panel
                alchemyToolItemImagePanels[i] = DaggerfallUI.AddPanel(alchemyToolInputButtons[i], AutoSizeModes.ScaleToFit);
                alchemyToolItemImagePanels[i].HorizontalAlignment = HorizontalAlignment.Center;
                alchemyToolItemImagePanels[i].VerticalAlignment = VerticalAlignment.Middle;
                alchemyToolItemImagePanels[i].MaxAutoScale = 1f;

                // Slot visual for being disabled image panel
                alchemyToolSlotDisabledPanels[i] = DaggerfallUI.AddPanel(alchemyToolInputButtons[i], AutoSizeModes.ScaleToFit);
                alchemyToolSlotDisabledPanels[i].Size = new Vector2(alchemyToolInputButtons[i].Size.x / 1.35f, alchemyToolInputButtons[i].Size.y / 1.35f);
                alchemyToolSlotDisabledPanels[i].HorizontalAlignment = HorizontalAlignment.Center;
                alchemyToolSlotDisabledPanels[i].VerticalAlignment = VerticalAlignment.Middle;
                alchemyToolSlotDisabledPanels[i].MaxAutoScale = 1f;
                alchemyToolSlotDisabledPanels[i].BackgroundTexture = sortIconXmarkTexture;
                alchemyToolSlotDisabledPanels[i].Enabled = false;
            }

            RefreshAlchemyToolPanels();
        }

        protected void RefreshAlchemyToolPanels()
        {
            alchemyToolSlots[0] = FindAlchemyTool(AOConstants.ItemIds.MortarAndPestle);
            alchemyToolSlots[1] = FindAlchemyTool(AOConstants.ItemIds.Alembic);
            alchemyToolSlots[2] = FindAlchemyTool(AOConstants.ItemIds.Retort);
            alchemyToolSlots[3] = FindAlchemyTool(AOConstants.ItemIds.Calcinator);

            // Update images and tooltips
            for (int i = 0; i < alchemyToolSlots.Length; i++)
            {
                // Get item and image
                DaggerfallUnityItem item = alchemyToolSlots[i];
                if (item == null) { ClearToolSlot(i); continue; }
                ImageData image = DaggerfallUnity.Instance.ItemHelper.GetInventoryImage(item);

                // Set image to button icon
                alchemyToolItemImagePanels[i].BackgroundTexture = image.texture;
                // Use texture size if base image size is zero (i.e. new images that are not present in classic data)
                if (image.width != 0 && image.height != 0)
                    alchemyToolItemImagePanels[i].Size = new Vector2(image.width, image.height);
                else
                    alchemyToolItemImagePanels[i].Size = new Vector2(image.texture.width, image.texture.height);

                // Set Tag to item object
                alchemyToolInputButtons[i].Tag = item;

                // Tooltip text
                alchemyToolInputButtons[i].ToolTipText = item.LongName;
            }
        }

        public DaggerfallUnityItem FindAlchemyTool(int templateIndex)
        {
            DaggerfallUnityItem bestTool = null;
            List<DaggerfallUnityItem> validTools = localItems.SearchItems(ItemGroups.UselessItems1, templateIndex);

            foreach (DaggerfallUnityItem tool in validTools)
            {
                // Once I implement different qualities of the tools, I'll fully implement this later to account for that and pick the best one.
                bestTool = tool;
            }
            return bestTool;
        }

        protected void ClearToolSlot(int slotIndex)
        {
            alchemyToolInputButtons[slotIndex].ToolTipText = GetToolSlotName(slotIndex);
            alchemyToolInputButtons[slotIndex].Tag = null;
            alchemyToolItemImagePanels[slotIndex].BackgroundTexture = null;
        }

        protected string GetToolSlotName(int slotIndex)
        {
            string name = "Tool Slot";
            switch (slotIndex)
            {
                case 0: name = "Mortar And Pestle"; break;
                case 1: name = "Alembic"; break;
                case 2: name = "Retort"; break;
                case 3: name = "Calcinator"; break;
            }
            return name;
        }

        protected virtual void AlchemyTools_OnLeftClick(BaseScreenComponent sender, Vector2 position)
        {
            AlchemyTools_OnItemClick(sender, position);
        }

        protected virtual void AlchemyTools_OnItemClick(BaseScreenComponent sender, Vector2 position)
        {
            ToggleAlchemyTool(sender, position);
        }

        protected void ToggleAlchemyTool(BaseScreenComponent sender, Vector2 position)
        {
            int slotIndex = int.Parse(sender.Name);
            DaggerfallUnityItem item = (DaggerfallUnityItem)sender.Tag;
            if (item == null) { return; }

            if (!alchemyToolSlotDisabledPanels[slotIndex].Enabled) { alchemyToolSlotDisabledPanels[slotIndex].Enabled = true; }
            else { alchemyToolSlotDisabledPanels[slotIndex].Enabled = false; }

            RefreshAlchemyToolPanels();
            RefreshBrewingIngredientInputPanels();
            RefreshBrewingResultInfoPanel();

            DaggerfallUI.Instance.PlayOneShot(DaggerfallUI.Instance.GetAudioClip(SoundClips.ButtonClick));
        }

        protected void SetupBrewingResultInfoText()
        {
            RefreshBrewingResultInfoPanel();
        }

        protected void RefreshBrewingResultInfoPanel()
        {
            IngredientDefinition def = null;
            DaggerfallUnityItem item = hoveredItem;
            if (item != null) { def = IngredientDatabase.Get(item.TemplateIndex); }

            string[] descripTextList = CollectBrewingResultInfoText();
            int neededLines = descripTextList.Length;

            int resPanH = 55;
            int maxLineWidth = 92;
            float textScale = 0.75f;
            int yOff = 5;
            int height = 10;
            int yAdj = 11;

            if (neededLines > 5)
            {
                height = Mathf.RoundToInt(resPanH / neededLines);
                yAdj = height;
                textScale = textScale - ((neededLines - 5) * 0.07f);
            }

            resultInfoPanel.Components.Clear();
            brewingResultTextPanels = new Panel[neededLines];
            for (int i = 0; i < brewingResultTextPanels.Length; i++)
            {
                brewingResultTextPanels[i] = DaggerfallUI.AddPanel(new Rect(5, yOff, 91, height), resultInfoPanel);
                yOff = yOff + yAdj;

                brewingResultTextPanels[i].Name = i.ToString();
                brewingResultTextPanels[i].Tag = null;
                //brewingResultTextPanels[i].BackgroundColor = new Color32(255, 0, 0, 110);
                brewingResultTextPanels[i].BackgroundColor = Color.clear;
                //CreateCenteredTextLabel("Nothing Ever Happens", new Vector2(1, 0), 92, brewingResultTextPanels[i], fontScale);
            }

            for (int i = 0; i < brewingResultTextPanels.Length; i++)
            {
                brewingResultTextPanels[i].Components.Clear();

                if (descripTextList.Length > i)
                {
                    CreateCenteredTextLabel(descripTextList[i], new Vector2(1, 0), maxLineWidth, brewingResultTextPanels[i], textScale);
                }
            }
        }

        private string[] CollectBrewingResultInfoText()
        {
            List<IngredientDefinition> defs = new List<IngredientDefinition>();
            for (int i = 0; i < brewingSlots.Length - 1; i++)
            {
                DaggerfallUnityItem item = brewingSlots[i];
                if (item == null) { continue; }
                IngredientDefinition def = IngredientDatabase.Get(item.TemplateIndex);
                if (def != null) { defs.Add(def); }
            }

            Dictionary<string, List<IngredientEffectEntry>> effectBuckets = new Dictionary<string, List<IngredientEffectEntry>>();
            foreach (var definition in defs)
            {
                foreach (var effect in definition.PrimaryEffects)
                {
                    if (!effectBuckets.TryGetValue(effect.EffectId, out var list))
                    {
                        list = new List<IngredientEffectEntry>();
                        effectBuckets.Add(effect.EffectId, list);
                    }

                    list.Add(effect);
                }
            }

            List<PotionEffectBlueprint> potionEffects = new List<PotionEffectBlueprint>();
            foreach (var kvp in effectBuckets)
            {
                string effectKey = kvp.Key;
                List<IngredientEffectEntry> contributors = kvp.Value;

                if (contributors.Count < 2)
                    continue;

                potionEffects.Add(BuildPotionEffect(effectKey, contributors));
            }

            return ResultInfoTextConstructor.GetInfoTextBasedOnPotionEffect(potionEffects);
        }

        protected void SetupHeatSettingsPanels()
        {
            heatSettingTextDisplayPanel = DaggerfallUI.AddPanel(new Rect(9, 3, 16, 8), heatSettingButtonPanel);
            heatSettingTextDisplayPanel.ToolTip = defaultToolTip;
            heatSettingTextDisplayPanel.ToolTipText = "Current Heat Setting";

            reduceHeatButton = DaggerfallUI.AddButton(new Rect(0, 0, 8, 14), heatSettingButtonPanel);
            reduceHeatButton.Name = "Reduce Heat";
            reduceHeatButton.ToolTip = defaultToolTip;
            reduceHeatButton.ToolTipText = "Decrease Heat";
            reduceHeatButton.Tag = false;
            reduceHeatButton.OnMouseClick += ChangeHeat_OnMouseClick;
            reduceHeatButton.ClickSound = DaggerfallUI.Instance.GetAudioClip(SoundClips.ButtonClick);

            increaseHeatButton = DaggerfallUI.AddButton(new Rect(26, 0, 8, 14), heatSettingButtonPanel);
            increaseHeatButton.Name = "Increase Heat";
            increaseHeatButton.ToolTip = defaultToolTip;
            increaseHeatButton.ToolTipText = "Increase Heat";
            increaseHeatButton.Tag = true;
            increaseHeatButton.OnMouseClick += ChangeHeat_OnMouseClick;
            increaseHeatButton.ClickSound = DaggerfallUI.Instance.GetAudioClip(SoundClips.ButtonClick);

            //heatActiveIconPanel = DaggerfallUI.AddPanel(new Rect(46, 163, 9, 15), middleMainPanel);
            //heatSettingButtonPanel = DaggerfallUI.AddPanel(new Rect(33, 180, 34, 14), middleMainPanel);

            RefreshHeatSettingsPanel(0, "OFF");
        }

        protected void RefreshHeatSettingsPanel(int currentHeatValue, string settingName)
        {
            heatSettingTextDisplayPanel.Components.Clear();
            heatSettingTextDisplayPanel.Tag = currentHeatValue;
            CreateCenteredTextLabel(settingName, new Vector2(1, 1), 16, heatSettingTextDisplayPanel, 1.00f);
        }

        private void ChangeHeat_OnMouseClick(BaseScreenComponent sender, Vector2 position)
        {
            int currentHeatValue = (int)heatSettingTextDisplayPanel.Tag;
            string settingName = "OFF";

            if ((bool)sender.Tag == true)
                currentHeatValue++;
            else
                currentHeatValue--;

            currentHeatValue = Mathf.Clamp(currentHeatValue, 0, 3);

            heatSettingTextDisplayPanel.Tag = currentHeatValue;

            switch (currentHeatValue)
            {
                default:
                case 0: settingName = "OFF"; break;
                case 1: settingName = "LOW"; break;
                case 2: settingName = "MED"; break;
                case 3: settingName = "HIGH"; break;
            }

            RefreshHeatSettingsPanel(currentHeatValue, settingName);
        }

        protected void SetupButtons()
        {
            // Exit button
            exitButton = DaggerfallUI.AddButton(new Rect(0, 0, 35, 15), exitButtonPanel);
            exitButton.OnMouseClick += ExitButton_OnMouseClick;
            exitButton.ClickSound = DaggerfallUI.Instance.GetAudioClip(SoundClips.ButtonClick);

            brewButton = DaggerfallUI.AddButton(new Rect(0, 0, 35, 13), brewButtonActivePanel);
            brewButton.OnMouseClick += BrewPotion_OnMouseClick;
            brewButton.ClickSound = DaggerfallUI.Instance.GetAudioClip(SoundClips.ButtonClick);
        }

        public static TextLabel CreateCenteredTextLabel(string text, Vector2 position, int maxWidth, Panel parentPanel, float textScale = 1, Color32? color = null)
        {
            if (color == null) { color = DaggerfallUI.DaggerfallDefaultTextColor; }

            TextLabel label = DaggerfallUI.AddTextLabel(DaggerfallUI.DefaultFont, position, text, parentPanel);
            label.TextColor = (Color)color;
            label.ShadowColor = Color.clear;
            label.MaxWidth = maxWidth;
            label.TextScale = textScale;
            label.HorizontalTextAlignment = TextLabel.HorizontalTextAlignmentSetting.Center;
            label.WrapText = true;
            label.WrapWords = true;
            return label;
        }

        protected virtual void LocalItemListScroller_OnItemLeftClick(DaggerfallUnityItem item)
        {
            LocalItemListScroller_OnItemClick(item);
        }

        protected virtual void LocalItemListScroller_OnItemClick(DaggerfallUnityItem item)
        {
            AttemptToAddItemToIngredientInput(item);
        }

        protected void AttemptToAddItemToIngredientInput(DaggerfallUnityItem item)
        {
            // I might change the multiple ingredient rule later, but heavily penalize the result the more ingredients are duplicated, but I'll see on that one.

            if (DetermineIfIngredientIsDuplicate(item)) { return; }

            bool isStack = item.IsAStack();

            if (isStack)
                item = localItems.SplitStack(item, 1);

            if (item.ItemTemplate.isLiquid) // Eventually make a proper list of valid "solvents" that will fit in this slot.
            {
                if (brewingSlots[6] == null) // Final index of the brewingSlots array will be for the solvent, so only have to check that one, which I think would be index 6 in this case.
                {
                    brewingSlots[6] = item;
                    localItems.RemoveItem(item);
                }
                else
                {
                    if (isStack)
                        localItems.AddItem(brewingSlots[6]); // The weird if statement here is to try to prevent the weird unstacking behavior when replacing the same ingredient with another.
                    else
                        localItems.AddItem(brewingSlots[6], noStack: true);
                    brewingSlots[6] = item;
                    localItems.RemoveItem(item);
                }
            }
            else // For all other ingredients, loop through the slots to see which one is open, if full then replace the last slot with this item instead, if valid.
            {
                for (int i = 0; i < brewingSlots.Length - 1; i++)
                {
                    if (brewingSlots[i] == null) // Whichever slot is found empty first, place the clicked item there.
                    {
                        brewingSlots[i] = item;
                        localItems.RemoveItem(item);
                        break;
                    }

                    if (i == 5) // If all slots are occupied, replace the last slot with the clicked local item.
                    {
                        localItems.AddItem(brewingSlots[5], noStack: true);
                        brewingSlots[5] = item;
                        localItems.RemoveItem(item);
                    }
                }
            }

            RefreshBrewingIngredientInputPanels();
            RefreshBrewingResultInfoPanel();
            RefreshLocalItemsFilteredList();
            localAOItemListScroller.Items = localItemsFiltered;

            DaggerfallUI.Instance.PlayOneShot(DaggerfallUI.Instance.GetAudioClip(SoundClips.ButtonClick));
        }

        private bool DetermineIfIngredientIsDuplicate(DaggerfallUnityItem item)
        {
            DaggerfallUI.Instance.PlayOneShot(DaggerfallUI.Instance.GetAudioClip(SoundClips.ButtonClick));

            for (int i = 0; i < brewingSlots.Length - 1; i++)
            {
                if (brewingSlots[i] != null && brewingSlots[i].TemplateIndex == item.TemplateIndex)
                {
                    TextFile.Token[] tokens = DaggerfallUnity.Instance.TextProvider.CreateTokens(
                            TextFile.Formatting.JustifyCenter,
                            "You cannot use multiple of the same",
                            "ingredient when brewing.");
                    DaggerfallMessageBox messageBox = new DaggerfallMessageBox(DaggerfallUI.UIManager, DaggerfallUI.UIManager.TopWindow);

                    messageBox.SetTextTokens(tokens);
                    messageBox.ClickAnywhereToClose = true;
                    messageBox.Show();

                    return true;
                }
            }
            return false;
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
                hoveredItem = item;
                RefreshPrimaryHoverInfoPanel();
                //RefreshBrewingResultInfoPanel();
            }
        }

        protected virtual void BrewingInputItems_OnLeftClick(BaseScreenComponent sender, Vector2 position)
        {
            BrewingInputItems_OnItemClick(sender, position);
        }

        protected virtual void BrewingInputItems_OnItemClick(BaseScreenComponent sender, Vector2 position)
        {
            RemoveItemFromIngredientInput(sender, position);
        }

        protected void RemoveItemFromIngredientInput(BaseScreenComponent sender, Vector2 position)
        {
            int slotIndex = int.Parse(sender.Name);
            DaggerfallUnityItem item = (DaggerfallUnityItem)sender.Tag;
            if (item == null) { return; }

            localItems.AddItem(item);
            brewingSlots[slotIndex] = null;

            RefreshBrewingIngredientInputPanels();
            RefreshBrewingResultInfoPanel();
            RefreshLocalItemsFilteredList();
            localAOItemListScroller.Items = localItemsFiltered;

            DaggerfallUI.Instance.PlayOneShot(DaggerfallUI.Instance.GetAudioClip(SoundClips.ButtonClick));
        }

        protected virtual void BrewingInputItems_OnMouseEnter(BaseScreenComponent sender)
        {
            hoveredItem = (DaggerfallUnityItem)sender.Tag;
            RefreshPrimaryHoverInfoPanel();
            //RefreshBrewingResultInfoPanel();
        }

        protected virtual void BrewingInputItems_OnMouseLeave(BaseScreenComponent sender)
        {
            hoveredItem = null;
            RefreshPrimaryHoverInfoPanel();
            //RefreshBrewingResultInfoPanel();
        }

        protected void FreeBrewingInputSlots()
        {
            for (int i = 0; i < brewingSlots.Length; i++)
            {
                if (brewingSlots[i] == null) { continue; }
                localItems.AddItem(brewingSlots[i]);
                brewingSlots[i] = null;
            }
            RefreshBrewingResultInfoPanel();
        }

        protected void DeleteBrewingInputSlots()
        {
            for (int i = 0; i < brewingSlots.Length; i++)
            {
                brewingSlots[i] = null;
            }
            RefreshBrewingResultInfoPanel();
        }

        private void BrewPotion_OnMouseClick(BaseScreenComponent sender, Vector2 position)
        {
            BrewPotion();
        }

        private void BrewPotion()
        {
            if (!DetermineIfBrewingInputsAreValid()) { return; }

            List<IngredientDefinition> defs = new List<IngredientDefinition>();
            for (int i = 0; i < brewingSlots.Length - 1; i++)
            {
                DaggerfallUnityItem item = brewingSlots[i];
                if (item == null) { continue; }
                IngredientDefinition def = IngredientDatabase.Get(item.TemplateIndex);
                if (def != null) { defs.Add(def); }
            }

            Dictionary<string, List<IngredientEffectEntry>> effectBuckets = new Dictionary<string, List<IngredientEffectEntry>>();
            foreach (var definition in defs)
            {
                foreach (var effect in definition.PrimaryEffects)
                {
                    if (!effectBuckets.TryGetValue(effect.EffectId, out var list))
                    {
                        list = new List<IngredientEffectEntry>();
                        effectBuckets.Add(effect.EffectId, list);
                    }

                    list.Add(effect);
                }
            }

            List<PotionEffectBlueprint> potionEffects = new List<PotionEffectBlueprint>();
            foreach (var kvp in effectBuckets)
            {
                string effectKey = kvp.Key;
                List<IngredientEffectEntry> contributors = kvp.Value;

                if (contributors.Count < 2)
                    continue;

                potionEffects.Add(BuildPotionEffect(effectKey, contributors));
            }

            DaggerfallUnityItem potItem = ItemBuilder.CreateItem(ItemGroups.UselessItems1, AOConstants.ItemIds.TestPotion);

            PotionDefinition potDef = PotionResolver.CreateNewPotionDefinition(potItem.UID.ToString(), potionEffects.ToArray());

            PotionData data = PotionRegistry.CreatePotion(potDef.Id);

            AlchemyOverhaulMain.AddPotionToSaveData(potItem.UID, data);

            AlchemyOverhaulMain.AddPotionToPlayerInventory(potItem);

            DaggerfallUI.Instance.PlayOneShot(DaggerfallUI.Instance.GetAudioClip(SoundClips.MakePotion));

            TextFile.Token[] tokens = DaggerfallUnity.Instance.TextProvider.CreateTokens(
                            TextFile.Formatting.JustifyCenter,
                            "You brewed a potion.");
            DaggerfallMessageBox messageBox = new DaggerfallMessageBox(DaggerfallUI.UIManager, DaggerfallUI.UIManager.TopWindow);

            messageBox.SetTextTokens(tokens);
            messageBox.ClickAnywhereToClose = true;
            messageBox.Show();

            AlchemySkill.AddExperience(1);

            RefreshAfterBrewingPotion();
        }

        public static PotionEffectBlueprint BuildPotionEffect(string effectKey, List<IngredientEffectEntry> contributors)
        {
            EffectDefinition effectDef = null;
            int minMag = 0;
            int maxMag = 0;
            int minDur = 0;
            int maxDur = 0;

            float magMods = 1f;
            float durMods = 1f;

            float skillMod = AlchemySkill.Level * 0.02f;

            int[] toolQualities = new int[4] {5, 5, 5, 5};

            for (int i = 0; i < Instance.alchemyToolSlotDisabledPanels.Length; i++)
            {
                if (Instance.alchemyToolSlotDisabledPanels[i].Enabled) { toolQualities[i] = 0; }
            }

            float mortarMod = toolQualities[0] * 0.075f;
            float alembicMod = toolQualities[1] * 0.2f;
            float retortMod = toolQualities[2] * 0.1f;
            float calcinatorMod = toolQualities[3] * 0.2f;

            // 4-11-2026, 8:40 AM: Tested "Restore Health" on its own, all worked as expected.

            // Test this stuff out tomorrow, then if it is working as expected, start working on probably the UI stuff for the tools, then after maybe actually make them custom items.

            foreach (var entry in contributors)
            {
                if (EffectDatabase.TryGet(effectKey, out effectDef))
                {
                    if (effectDef.UsesDuration)
                    {
                        minDur += Mathf.RoundToInt(effectDef.BaseDurationSeconds);
                        maxDur += Mathf.RoundToInt(effectDef.BaseDurationSeconds + entry.BaseMagnitude);
                    }

                    if (effectDef.UsesMagnitude)
                    {
                        minMag += Mathf.RoundToInt(entry.BaseMagnitude);
                        maxMag += Mathf.RoundToInt(entry.BaseMagnitude);
                    }
                }
            }

            if (effectDef.IsHostile)
            {
                magMods += skillMod + mortarMod + calcinatorMod - alembicMod;
                durMods += skillMod + mortarMod + calcinatorMod - alembicMod;
            }
            else
            {
                magMods += skillMod + mortarMod + calcinatorMod + retortMod;
                durMods += skillMod + mortarMod + calcinatorMod + retortMod;
            }

            minMag = Mathf.FloorToInt(minMag * magMods);
            maxMag = Mathf.FloorToInt(maxMag * magMods);
            minDur = Mathf.FloorToInt(minDur * durMods);
            maxDur = Mathf.FloorToInt(maxDur * durMods);

            if (!effectDef.UsesDuration && !effectDef.UsesMagnitude) { return null; }
            if (maxDur <= 0 && maxMag <= 0) { return null; }
            if (maxDur > 0 && minDur <= 0) { minDur = 1; }
            if (maxMag > 0 && minMag <= 0) { minMag = 1; }

            PotionEffectDurationType durType = PotionEffectDurationType.Timed;
            if (!effectDef.UsesDuration || maxDur <= 0) { durType = PotionEffectDurationType.Instant; }

            return new PotionEffectBlueprint
            {
                EffectKey = effectKey,
                MinMagnitude = minMag,
                MaxMagnitude = maxMag,
                MinDuration = minDur,
                MaxDuration = maxDur,
                DurationType = durType,
                ScalingModel = EffectScalingModel.Additive,
                IdentificationLevel = EffectIdentificationLevel.Full
            };
        }

        public static PotionEffectBlueprint BuildPotionEffect(PotionEffectInstance eff)
        {
            EffectDefinition effectDef = null;

            if (!EffectDatabase.TryGet(eff.EffectKey, out effectDef)) { return null; }

            if (!effectDef.UsesDuration && !effectDef.UsesMagnitude) { return null; }
            if (eff.Duration <= 0 && eff.Magnitude <= 0) { return null; }

            return new PotionEffectBlueprint
            {
                EffectKey = eff.EffectKey,
                MinMagnitude = eff.Magnitude,
                MaxMagnitude = eff.Magnitude,
                MinDuration = eff.Duration,
                MaxDuration = eff.Duration,
                DurationType = eff.DurationType,
                ScalingModel = EffectScalingModel.Additive,
                IdentificationLevel = EffectIdentificationLevel.Full
            };
        }

        private bool DetermineIfBrewingInputsAreValid()
        {
            int ingredCount = 0;
            int solventCount = 0;
            HashSet<int> uniqueTemplates = new HashSet<int>();

            for (int i = 0; i < brewingSlots.Length; i++)
            {
                DaggerfallUnityItem item = brewingSlots[i];
                if (item == null) { continue; }

                uniqueTemplates.Add(brewingSlots[i].TemplateIndex);
                if (i <= 5) { ingredCount++; }
                if (i == 6) { solventCount++; }
            }

            if (solventCount <= 0 || ingredCount <= 1 || uniqueTemplates.Count <= 2)
            {
                TextFile.Token[] tokens = DaggerfallUnity.Instance.TextProvider.CreateTokens(
                            TextFile.Formatting.JustifyCenter,
                            "1 Solvent and atleast 2 unique Ingredients",
                            "are required to brew a potion.");
                DaggerfallMessageBox messageBox = new DaggerfallMessageBox(DaggerfallUI.UIManager, DaggerfallUI.UIManager.TopWindow);

                messageBox.SetTextTokens(tokens);
                messageBox.ClickAnywhereToClose = true;
                messageBox.Show();

                return false;
            }
            return true;
        }

        public void RefreshAfterBrewingPotion()
        {
            DeleteBrewingInputSlots();
            RefreshBrewingIngredientInputPanels();
            RefreshLocalItemsFilteredList();
            RefreshPrimaryHoverInfoPanel();
            RefreshBrewingResultInfoPanel();
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
