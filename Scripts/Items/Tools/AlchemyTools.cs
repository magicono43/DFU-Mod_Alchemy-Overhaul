using DaggerfallWorkshop.Game.Items;
using DaggerfallWorkshop.Game.Serialization;

namespace AlchemyOverhaul.Items.Tools
{
    //Mortar And Pestle
    public class ItemMortarAndPestle : DaggerfallUnityItem
    {
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