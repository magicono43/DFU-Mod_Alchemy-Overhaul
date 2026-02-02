using System.Collections.Generic;
using AlchemyOverhaul.Data.Enums;

namespace AlchemyOverhaul.Data.Runtime
{
    public sealed class PotionData
    {
        // ===== Identity / Provenance =====
        public string PotionGuid;              // System.Guid.ToString()
        public ulong CreatedGameTime;
        public int BrewerEntityId;              // Or -1 if unknown
        public int AlchemySkillAtBrew;

        public PotionBrewFlags BrewFlags;

        // ===== Ingredient Provenance =====
        public List<IngredientContribution> Ingredients;

        // ===== Effect Payload =====
        public List<PotionEffectInstance> Effects;

        // ===== Secondary Attributes =====
        public float Toxicity;
        public float Palatability;
        public float SpoilageRate;

        // ===== Spoilage State =====
        public bool IsSpoiled;
        public ulong LastSpoilageCheckTime;
    }
}
