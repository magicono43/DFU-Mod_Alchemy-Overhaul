using System.Collections.Generic;

namespace AlchemyOverhaul.Ingredients.Knowledge
{
	public static class IngredientKnowledgeSystem
	{
		private static Dictionary<string, IngredientEffectKnowledge> knowledge;

		public static void LoadFromSave(Dictionary<string, IngredientEffectKnowledge> savedKnowledge)
		{
			knowledge = savedKnowledge ??
				new Dictionary<string, IngredientEffectKnowledge>();
		}

		public static Dictionary<string, IngredientEffectKnowledge> GetSaveData()
		{
			return knowledge;
		}

		public static bool IsKnown(string ingredientId, IngredientEffectKnowledge effect)
		{
			if (knowledge == null)
				return false;

			if (!knowledge.TryGetValue(ingredientId, out var known))
				return false;

			return (known & effect) != 0;
		}

		public static void Learn(string ingredientId, IngredientEffectKnowledge effect)
		{
			if (knowledge == null)
				return;

			if (!knowledge.TryGetValue(ingredientId, out var known))
				known = IngredientEffectKnowledge.None;

			known |= effect;
			knowledge[ingredientId] = known;
		}
	}
}