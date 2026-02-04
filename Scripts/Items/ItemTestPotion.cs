using DaggerfallWorkshop.Game.Items;
using DaggerfallWorkshop.Game.Serialization;
using AlchemyOverhaul.Potions;
using AlchemyOverhaul.Execution;
using AlchemyOverhaul.Data.Runtime;

namespace AlchemyOverhaul.Items
{
    //Test Potion
    public class ItemTestPotion : DaggerfallUnityItem
    {
        public ItemTestPotion() : base(ItemGroups.UselessItems1, AOConstants.ItemIds.TestPotion)
        {
            shortName = "Test Potion (AO)";
        }

        public override ItemData_v1 GetSaveData()
        {
            ItemData_v1 data = base.GetSaveData();
            data.className = typeof(ItemTestPotion).ToString();
            return data;
        }

        public override bool UseItem(ItemCollection collection)
        {
            if (!AlchemyOverhaulMain.ModSaveData.TryGetPotionData(this.UID, out PotionData data))
                return true;

            foreach (PotionEffectInstance effect in data.Effects)
            {
                if (effect.DurationType == PotionEffectDurationType.Instant)
                    AlchemyExecutionAdapter.ApplyInstantEffect(effect.EffectKey, effect.Magnitude);
                else
                    AlchemyExecutionAdapter.ApplyPotionEffect(effect.EffectKey, effect.Magnitude, effect.Duration);
            }

            AlchemyOverhaulMain.ModSaveData.RemovePotion(this.UID);
            collection.RemoveItem(this);
            return true;
        }
    }
}