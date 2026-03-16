using System.Collections.Generic;
using AlchemyOverhaul.Data.Enums;
using AlchemyOverhaul.Data.Runtime;
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
                        new IngredientEffectEntry("Paralysis", 1f),
                        new IngredientEffectEntry("Jumping", 1f),
                        new IngredientEffectEntry("Invisibility True", 1f)
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
                        new IngredientEffectEntry(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.Paralysis), 1f),
                        new IngredientEffectEntry("Restore Health", 1f),
                        new IngredientEffectEntry("Slowfall", 1f),
                        new IngredientEffectEntry(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.FortifySpeed), 10f)
                    },
                    secretEffect: new IngredientEffectEntry("Paralysis", 1f)
                )
            );

            IngredientDatabase.Register(
                new IngredientDefinition(
                    templateIndex: 12,
                    ingredientId: "root tendrils",
                    displayName: "Root Tendrils",
                    primaryEffects: new List<IngredientEffectEntry>
                    {
                        new IngredientEffectEntry("Regenerate Health", 3f),
                        new IngredientEffectEntry(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.Paralysis), 2f),
                        new IngredientEffectEntry("Jumping", 2f),
                        new IngredientEffectEntry(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.FortifySpeed), 5f)
                    },
                    secretEffect: new IngredientEffectEntry("Paralysis", 3f)
                )
            );

            IngredientDatabase.Register(
                new IngredientDefinition(
                    templateIndex: 8,
                    ingredientId: "twigs",
                    displayName: "Twigs",
                    primaryEffects: new List<IngredientEffectEntry>
                    {
                        new IngredientEffectEntry("Invisibility True", 2f),
                        new IngredientEffectEntry("Restore Health", 4f),
                        new IngredientEffectEntry("Regenerate Health", 2f),
                        new IngredientEffectEntry("Slowfall", 3f)
                    },
                    secretEffect: new IngredientEffectEntry("Slowfall", 4f)
                )
            );
        }
    }
}