using System;
using AlchemyOverhaul.Data.Enums;

namespace AlchemyOverhaul.Data.Runtime
{
    [Serializable]
    public struct IngredientContribution
    {
        public int IngredientId;                // Your custom ingredient registry ID
        public int CountUsed;

        public IngredientEffectMask ExpressedEffects;
        public bool SpecialEffectTriggered;
    }
}
