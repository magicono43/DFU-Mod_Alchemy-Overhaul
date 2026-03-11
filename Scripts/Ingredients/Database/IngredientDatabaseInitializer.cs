using System.Collections.Generic;
using AlchemyOverhaul.Ingredients.Definitions;

namespace AlchemyOverhaul.Ingredients.Database
{
    public static class IngredientDatabaseInitializer
    {
        public static void Initialize()
        {
            IngredientDatabase.Register(
                new IngredientDefinition(
                    templateIndex: 32,
                    ingredientId: "cactus",
                    displayName: "Cactus",
                    primaryEffects: new List<IngredientEffectEntry>
                    {
                        new IngredientEffectEntry("Restore Health", 1f),
                        new IngredientEffectEntry("Paralysis", 1f)
                    },
                    secretEffect: new IngredientEffectEntry("Restore Health", 1f)
                )
            );

            IngredientDatabase.Register(
                new IngredientDefinition(
                    templateIndex: 18,
                    ingredientId: "clover",
                    displayName: "Clover",
                    primaryEffects: new List<IngredientEffectEntry>
                    {
                        new IngredientEffectEntry("Paralysis", 1f),
                        new IngredientEffectEntry("Restore Health", 1f)
                    },
                    secretEffect: new IngredientEffectEntry("Paralysis", 1f)
                )
            );
        }
    }
}