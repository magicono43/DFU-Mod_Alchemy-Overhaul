using System.Collections.Generic;
using AlchemyOverhaul.Ingredients.Definitions;

namespace AlchemyOverhaul.Ingredients.Database
{
    public static class IngredientDatabase
    {
        private static readonly Dictionary<int, IngredientDefinition> ingredients
            = new Dictionary<int, IngredientDefinition>();

        public static void Register(IngredientDefinition definition)
        {
            ingredients[definition.TemplateIndex] = definition;
        }

        public static IngredientDefinition Get(int templateIndex)
        {
            ingredients.TryGetValue(templateIndex, out var def);
            return def;
        }

        public static bool Contains(int templateIndex)
        {
            return ingredients.ContainsKey(templateIndex);
        }

        public static IEnumerable<IngredientDefinition> All()
        {
            return ingredients.Values;
        }
    }
}