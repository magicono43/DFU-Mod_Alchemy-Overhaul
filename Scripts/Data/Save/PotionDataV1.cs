using System;
using System.Collections.Generic;
using AlchemyOverhaul.Data.Enums;
using AlchemyOverhaul.Data.Runtime;

namespace AlchemyOverhaul.Data.Save
{
    [Serializable]
    public sealed class PotionDataV1
    {
        public string PotionGuid;
        public ulong CreatedGameTime;
        public int BrewerEntityId;
        public int AlchemySkillAtBrew;

        public PotionBrewFlags BrewFlags;

        public List<IngredientContribution> Ingredients;
        public List<PotionEffectInstance> Effects;

        public float Toxicity;
        public float Palatability;
        public float SpoilageRate;

        public bool IsSpoiled;
        public ulong LastSpoilageCheckTime;
    }
}
