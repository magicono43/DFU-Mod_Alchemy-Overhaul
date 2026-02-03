using System.Collections.Generic;
using AlchemyOverhaul.Data.Enums;

namespace AlchemyOverhaul.Data.Runtime
{
    /// <summary>
    /// Runtime-frozen potion instance.
    /// All fields in this class are serialized via DFU mod save data.
    /// No values are recomputed after creation.
    /// </summary>
    public sealed class PotionData
    {
        // ===== Identity / Provenance =====
        public string PotionInstanceId;              // System.Guid.ToString()
        public string PotionDefinitionId;
        public ulong CreatedGameTime;
        public int BrewerEntityId;              // Or -1 if unknown
        public int AlchemySkillAtBrew;

        public PotionBrewFlags BrewFlags;

        // ===== Ingredient Provenance =====
        public List<IngredientContribution> Ingredients;

        // ===== Effect Payload =====
        public List<PotionEffectInstance> Effects;

        // ===== Spoilage State =====
        public bool IsSpoiled;
        public ulong LastSpoilageCheckTime;
    }
}
