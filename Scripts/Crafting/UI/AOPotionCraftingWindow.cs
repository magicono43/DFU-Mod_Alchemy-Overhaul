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

        Rect[] ingredientInputItemButtonRects = inputItemButtonRects;
        Button[] ingredientInputItemButtons;
        Panel[] ingredientInputItemImagePanels;

        static Rect[] inputItemButtonRects = new Rect[]
        {
            new Rect(26, 0, 23, 22), new Rect(52, 0, 23, 22), new Rect(0, 22, 23, 22), new Rect(78, 22, 23, 22), new Rect(0, 47, 23, 22), new Rect(78, 47, 23, 22), new Rect(39, 39, 23, 22)
        };

        ItemCollection localItems = new ItemCollection();
        List<DaggerfallUnityItem> localItemsFiltered = new List<DaggerfallUnityItem>();

        DaggerfallUnityItem[] brewingSlots = new DaggerfallUnityItem[7];

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
            SetupButtons();

            // Next time I work on this, figure out the but that allows for two
            // ingredients of the same type to be in slot 6, but this seems to only happen if in localItems the item being added to slot 6 is the only one
            // left in localItems and is the same type as the item in slot 6 currently, it's strange, when removed they will come back as a stack of 2
            // so not sure why that is happening, will likely need to step through it using breakpoints and checking the values in those situations, etc.
            // I'm guessing it might have to do with the "SplitStack" method being used before it? But I have no clue honestly.
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
                    if (item.IsIngredient && !item.IsEnchanted)
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

                // Item foreground animation panel
                //itemAnimPanels[i] = DaggerfallUI.AddPanel(itemButtonRects[i], itemsListPanel);
                //itemAnimPanels[i].AnimationDelayInSeconds = foregroundAnimationDelay;

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

        protected virtual void LocalItemListScroller_OnItemLeftClick(DaggerfallUnityItem item)
        {
            LocalItemListScroller_OnItemClick(item);
        }

        protected virtual void LocalItemListScroller_OnItemClick(DaggerfallUnityItem item)
        {
            AttemptToAddItemToIngredientInput(item);
            // Make the above a method for adding the clicked ingredient to the ingredient input panel, if valid of course, also remember the solvent thing.
        }

        protected void AttemptToAddItemToIngredientInput(DaggerfallUnityItem item)
        {
            if (item.IsAStack())
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
                    localItems.AddItem(brewingSlots[6]);
                    brewingSlots[6] = item;
                    localItems.RemoveItem(item);
                }
            }
            else // For all other ingredients, loop through the slots to see which one is open, if full than put replace the last slot with this item instead, if valid.
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
                        localItems.AddItem(brewingSlots[5]);
                        brewingSlots[5] = item;
                        localItems.RemoveItem(item);
                    }
                }
            }

            RefreshBrewingIngredientInputPanels();
            RefreshLocalItemsFilteredList();
            localAOItemListScroller.Items = localItemsFiltered;

            DaggerfallUI.Instance.PlayOneShot(DaggerfallUI.Instance.GetAudioClip(SoundClips.ButtonClick));
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
            RefreshLocalItemsFilteredList();
            localAOItemListScroller.Items = localItemsFiltered;

            DaggerfallUI.Instance.PlayOneShot(DaggerfallUI.Instance.GetAudioClip(SoundClips.ButtonClick));
        }

        protected virtual void BrewingInputItems_OnMouseEnter(BaseScreenComponent sender)
        {
            //
        }

        protected void FreeBrewingInputSlots()
        {
            for (int i = 0; i < brewingSlots.Length; i++)
            {
                if (brewingSlots[i] == null) { continue; }
                localItems.AddItem(brewingSlots[i]);
                brewingSlots[i] = null;
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
