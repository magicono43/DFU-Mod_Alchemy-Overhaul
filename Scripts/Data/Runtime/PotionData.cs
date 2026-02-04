using System.Collections.Generic;
using AlchemyOverhaul.Data.Definitions;
using AlchemyOverhaul.Data.Enums;

namespace AlchemyOverhaul.Data.Runtime
{
    /// <summary>
    /// Runtime-frozen potion instance.
    /// Serializable fields are persisted via DFU mod save data.
    /// Definition is resolved at runtime and is not serialized.
    /// </summary>
    public sealed class PotionData
    {
        // ===== Identity / Provenance =====
        public string PotionDefinitionId;
        public string PotionInstanceId;              // System.Guid.ToString()
        public ulong CreatedGameTime;
        public int BrewerEntityId;                   // Or -1 if unknown
        public int AlchemySkillAtBrew;

        public PotionBrewFlags BrewFlags;

        // ===== Ingredient Provenance =====
        public List<IngredientContribution> Ingredients;

        // ===== Effect Payload =====
        public List<PotionEffectInstance> Effects;

        // ===== Spoilage State =====
        public bool IsSpoiled;
        public ulong LastSpoilageCheckTime;

        // ===== Runtime-only =====
        public PotionDefinition Definition;

        public void ApplyDefinition(PotionDefinition definition)
        {
            Definition = definition;

            if (Effects == null || definition.Effects == null)
                return;

            for (int i = Effects.Count - 1; i >= 0; i--)
            {
                if (!definition.TryValidateEffect(Effects[i]))
                {
                    // Rule: removed effects are deleted
                    Effects.RemoveAt(i);
                }
            }
        }
    }
}