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
                        new IngredientEffectEntry(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.PacifyAnimal), 1f),
                        new IngredientEffectEntry(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.PacifyDaedra), 1f),
                        new IngredientEffectEntry(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.PacifyHumanoid), 1f),
                        new IngredientEffectEntry(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.PacifyUndead), 1f)
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
                        new IngredientEffectEntry(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.PacifyAnimal), 1f),
                        new IngredientEffectEntry(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.PacifyDaedra), 1f),
                        new IngredientEffectEntry(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.PacifyHumanoid), 1f),
                        new IngredientEffectEntry(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.PacifyUndead), 1f)
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
                        new IngredientEffectEntry(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.Paralysis), 10f),
                        new IngredientEffectEntry(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.DamageSpeed), 10f),
                        new IngredientEffectEntry(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.DamageStrength), 10f),
                        new IngredientEffectEntry(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.DamageWillpower), 10f)
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
                        new IngredientEffectEntry(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.Paralysis), 10f),
                        new IngredientEffectEntry(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.DamageSpeed), 10f),
                        new IngredientEffectEntry(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.DamageStrength), 10f),
                        new IngredientEffectEntry(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.DamageWillpower), 10f)
                    },
                    secretEffect: new IngredientEffectEntry(PotionEffectKeyMapper.ToDFUEffectKey(PotionEffectKey.Slowfall), 4f)
                )
            );
        }
    }
}