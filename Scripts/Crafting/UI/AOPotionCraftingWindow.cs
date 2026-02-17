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
    /// Implements Alchemy Overhaul's Custom Potion Crafting Window.
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

        Texture2D baseTexture;
        Texture2D slotBorderTexture;
        Texture2D rightExtraEquipTexture;
        Texture2D leftExtraEquipTexture;
        Texture2D rightItemComparisonTexture;
        Texture2D leftItemComparisonTexture;
        Texture2D sortButtonBackgroundTexture;
        Texture2D sortButtonActiveBorderTexture;
        Texture2D sortIconCheckmarkTexture;
        Texture2D sortIconXmarkTexture;
        Texture2D sortIconPercentTexture;
        Texture2D sortIconSwordTexture;
        Texture2D sortIconShieldTexture;
        Texture2D sortIconAscendArrowTexture;
        Texture2D sortIconDescendArrowTexture;

        #endregion

        AOItemListScroller localAOItemListScroller;

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
            NativePanel.BackgroundColor = ScreenDimColor;
            NativePanel.BackgroundTexture = baseTexture;

            SetupChestChoiceButtons();

            localItems = Player.Items;

            SetupTestItemImagePanels();
        }

        protected virtual void LoadTextures()
        {
            baseTexture = AlchemyOverhaulMain.Instance.EquipInfoGUITexture;
            slotBorderTexture = AlchemyOverhaulMain.Instance.EquipInfoSlotBorderTexture;
            rightExtraEquipTexture = AlchemyOverhaulMain.Instance.EquipInfoExtraRightPanelTexture;
            leftExtraEquipTexture = AlchemyOverhaulMain.Instance.EquipInfoExtraLeftPanelTexture;
            rightItemComparisonTexture = AlchemyOverhaulMain.Instance.EquipInfoRightComparisonPanelTexture;
            leftItemComparisonTexture = AlchemyOverhaulMain.Instance.EquipInfoLeftComparisonPanelTexture;
            sortButtonBackgroundTexture = AlchemyOverhaulMain.Instance.EquipInfoSortButtonBackgroundTexture;
            sortButtonActiveBorderTexture = AlchemyOverhaulMain.Instance.EquipInfoSortButtonActiveBorderTexture;
            sortIconCheckmarkTexture = AlchemyOverhaulMain.Instance.EquipInfoSortIconCheckmarkTexture;
            sortIconXmarkTexture = AlchemyOverhaulMain.Instance.EquipInfoSortIconXmarkTexture;
            sortIconPercentTexture = AlchemyOverhaulMain.Instance.EquipInfoSortIconPercentTexture;
            sortIconSwordTexture = AlchemyOverhaulMain.Instance.EquipInfoSortIconSwordTexture;
            sortIconShieldTexture = AlchemyOverhaulMain.Instance.EquipInfoSortIconShieldTexture;
            sortIconAscendArrowTexture = AlchemyOverhaulMain.Instance.EquipInfoSortIconAscendingTexture;
            sortIconDescendArrowTexture = AlchemyOverhaulMain.Instance.EquipInfoSortIconDescendingTexture;
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
