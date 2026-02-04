using System;
using System.Collections.Generic;
using AlchemyOverhaul.Data.Enums;
using AlchemyOverhaul.Data.Runtime;

namespace AlchemyOverhaul.Data.Save
{
    [Serializable]
    public sealed class PotionDataV1
    {
        // === Definition Reference ===
        public string PotionDefinitionId;

        // === Instance Identity ===
        public string PotionInstanceId;
        public ulong CreatedGameTime;
        public int BrewerEntityId;
        public int AlchemySkillAtBrew;

        // === Brew Configuration ===
        public PotionBrewFlags BrewFlags;

        // === Composition ===
        public List<IngredientContribution> Ingredients;
        public List<PotionEffectInstance> Effects;

        // === State ===
        public float Toxicity;
        public float Palatability;
        public float SpoilageRate;

        public bool IsSpoiled;
        public ulong LastSpoilageCheckTime;
    }
}