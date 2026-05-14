using AlchemyOverhaul.Data.Enums;
using DaggerfallWorkshop.Game.Items;
using DaggerfallWorkshop.Game.Serialization;

namespace AlchemyOverhaul.Items.Tools
{
    public static class AOToolItemBuilder
    {
        public static DaggerfallUnityItem CreateAlchemyTool(AlchemyToolType type, AlchemyToolQuality qual)
        {
            int templateIndex = 0;
            string qualName = "";
            string toolName = "";
            string finalName = "";

            switch (type)
            {
                case AlchemyToolType.MortarAndPestle: templateIndex = AOConstants.ItemIds.MortarAndPestle; toolName = "Mortar And Pestle"; break;
                case AlchemyToolType.Retort: templateIndex = AOConstants.ItemIds.Retort; toolName = "Retort"; break;
                case AlchemyToolType.Calcinator: templateIndex = AOConstants.ItemIds.Calcinator; toolName = "Calcinator"; break;
                case AlchemyToolType.Alembic: templateIndex = AOConstants.ItemIds.Alembic; toolName = "Alembic"; break;
            }

            if (templateIndex == 0) { return null; }

            switch (qual)
            {
                case AlchemyToolQuality.Novice: qualName = "Novice "; break;
                case AlchemyToolQuality.Apprentice: qualName = "Apprentice "; break;
                case AlchemyToolQuality.Journeyman: qualName = "Journeyman "; break;
                case AlchemyToolQuality.Expert: qualName = "Expert "; break;
                case AlchemyToolQuality.Master: qualName = "Master "; break;
                default: qualName = "Novice "; break;
            }

            if (qualName == "") { finalName = toolName; }
            else { finalName = qualName + toolName; }

            DaggerfallUnityItem item = ItemBuilder.CreateItem(ItemGroups.UselessItems1, templateIndex);
            item.shortName = finalName;
            item.message = (int)qual;

            return item; // Try to add quality variants to these, then add the logic for that and test in-game, etc. Jewelry Additions for an example.
        }
    }

    //Mortar And Pestle
    public class ItemMortarAndPestle : DaggerfallUnityItem
    {
        public const string baseName = "Mortar And Pestle";

        public ItemMortarAndPestle() : base(ItemGroups.UselessItems1, AOConstants.ItemIds.MortarAndPestle)
        {
            shortName = "Mortar And Pestle";
        }

        public override ItemData_v1 GetSaveData()
        {
            ItemData_v1 data = base.GetSaveData();
            data.className = typeof(ItemMortarAndPestle).ToString();
            return data;
        }
    }
    
    //Retort
    public class ItemRetort : DaggerfallUnityItem
    {
        public const string baseName = "Retort";

        public ItemRetort() : base(ItemGroups.UselessItems1, AOConstants.ItemIds.Retort)
        {
            shortName = "Retort";
        }

        public override ItemData_v1 GetSaveData()
        {
            ItemData_v1 data = base.GetSaveData();
            data.className = typeof(ItemRetort).ToString();
            return data;
        }
    }
    
    //Calcinator
    public class ItemCalcinator : DaggerfallUnityItem
    {
        public const string baseName = "Calcinator";

        public ItemCalcinator() : base(ItemGroups.UselessItems1, AOConstants.ItemIds.Calcinator)
        {
            shortName = "Calcinator";
        }

        public override ItemData_v1 GetSaveData()
        {
            ItemData_v1 data = base.GetSaveData();
            data.className = typeof(ItemCalcinator).ToString();
            return data;
        }
    }
    
    //Alembic
    public class ItemAlembic : DaggerfallUnityItem
    {
        public const string baseName = "Alembic";

        public ItemAlembic() : base(ItemGroups.UselessItems1, AOConstants.ItemIds.Alembic)
        {
            shortName = "Alembic";
        }

        public override ItemData_v1 GetSaveData()
        {
            ItemData_v1 data = base.GetSaveData();
            data.className = typeof(ItemAlembic).ToString();
            return data;
        }
    }
}