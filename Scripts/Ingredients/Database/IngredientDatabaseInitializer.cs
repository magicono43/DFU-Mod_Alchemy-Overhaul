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
                        new IngredientEffectEntry(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.RestoreHealth), 1f),
                        new IngredientEffectEntry(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.Paralysis), 1f),
                        new IngredientEffectEntry(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.Jumping), 1f),
                        new IngredientEffectEntry(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.InvisibilityTrue), 1f)
                    },
                    secretEffect: new IngredientEffectEntry(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.RestoreHealth), 1f)
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
                        new IngredientEffectEntry(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.RestoreHealth), 1f),
                        new IngredientEffectEntry(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.Slowfall), 1f),
                        new IngredientEffectEntry(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.FortifySpeed), 10f)
                    },
                    secretEffect: new IngredientEffectEntry(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.Paralysis), 1f)
                )
            );

            IngredientDatabase.Register(
                new IngredientDefinition(
                    templateIndex: 12,
                    ingredientId: "root tendrils",
                    displayName: "Root Tendrils",
                    primaryEffects: new List<IngredientEffectEntry>
                    {
                        new IngredientEffectEntry(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.RegenerateHealth), 3f),
                        new IngredientEffectEntry(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.Paralysis), 2f),
                        new IngredientEffectEntry(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.Jumping), 2f),
                        new IngredientEffectEntry(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.FortifySpeed), 5f)
                    },
                    secretEffect: new IngredientEffectEntry(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.Paralysis), 3f)
                )
            );

            IngredientDatabase.Register(
                new IngredientDefinition(
                    templateIndex: 8,
                    ingredientId: "twigs",
                    displayName: "Twigs",
                    primaryEffects: new List<IngredientEffectEntry>
                    {
                        new IngredientEffectEntry(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.InvisibilityTrue), 2f),
                        new IngredientEffectEntry(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.RestoreHealth), 4f),
                        new IngredientEffectEntry(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.RegenerateHealth), 2f),
                        new IngredientEffectEntry(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.Slowfall), 3f)
                    },
                    secretEffect: new IngredientEffectEntry(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.Slowfall), 4f)
                )
            );
        }
    }
}