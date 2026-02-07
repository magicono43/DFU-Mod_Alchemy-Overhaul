using System;

namespace AlchemyOverhaul.Ingredients.Knowledge
{
    [Flags]
    public enum IngredientEffectKnowledge
    {
        None        = 0,
        Effect1     = 1 << 0,
        Effect2     = 1 << 1,
        Effect3     = 1 << 2,
        Effect4     = 1 << 3,
        Secret      = 1 << 4,

        AllPrimary  = Effect1 | Effect2 | Effect3 | Effect4,
        All         = AllPrimary | Secret,
    }
}