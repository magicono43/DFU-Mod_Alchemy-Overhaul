using System.Collections.Generic;

namespace AlchemyOverhaul.Ingredients.Definitions
{
    public sealed class IngredientDefinition
    {
        public int TemplateIndex { get; }

        public string IngredientId { get; }
        public string DisplayName { get; }

        public IReadOnlyList<IngredientEffectEntry> PrimaryEffects { get; }
        public IngredientEffectEntry SecretEffect { get; }

        public IngredientDefinition(
            int templateIndex,
            string ingredientId,
            string displayName,
            List<IngredientEffectEntry> primaryEffects,
            IngredientEffectEntry secretEffect)
        {
            TemplateIndex = templateIndex;
            IngredientId = ingredientId;
            DisplayName = displayName;
            PrimaryEffects = primaryEffects;
            SecretEffect = secretEffect;
        }
    }
	
	public sealed class IngredientEffectEntry
	{
		public string EffectId { get; }
		public float BaseMagnitude { get; }

		public IngredientEffectEntry(string effectId, float baseMagnitude)
		{
			EffectId = effectId;
			BaseMagnitude = baseMagnitude;
		}
	}
}