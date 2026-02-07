using System.Collections.Generic;
using DaggerfallWorkshop.Game.Items;
using AlchemyOverhaul.Ingredients.Definitions;

namespace AlchemyOverhaul.Ingredients.Database
{
    public static class IngredientDatabase
    {
        private static readonly Dictionary<int, IngredientDefinition> byTemplateIndex
            = BuildDatabase();

        private static Dictionary<int, IngredientDefinition> BuildDatabase()
        {
            var dict = new Dictionary<int, IngredientDefinition>();

            // Example ingredient
            dict.Add(512, new IngredientDefinition(
                templateIndex: 512,
                ingredientId: "nimroot",
                displayName: "Nimroot",
                primaryEffects: new List<IngredientEffectEntry>
                {
                    new IngredientEffectEntry("restore_health", 5f),
                    new IngredientEffectEntry("fortify_agility", 3f),
                    new IngredientEffectEntry("resist_fire", 10f),
                    new IngredientEffectEntry("drain_fatigue", 2f),
                },
                secretEffect: new IngredientEffectEntry("paralysis", 1f)
            ));

            return dict; // Eventually turn these into a .json list or something, to be more easily edited later on. 
        }

        public static bool TryGet(
            DaggerfallUnityItem item,
            out IngredientDefinition def)
        {
            return byTemplateIndex.TryGetValue(item.TemplateIndex, out def);
        }
    }
}