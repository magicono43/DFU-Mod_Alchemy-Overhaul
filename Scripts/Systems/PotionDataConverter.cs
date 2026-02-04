using AlchemyOverhaul.Data.Runtime;
using AlchemyOverhaul.Data.Save;

namespace AlchemyOverhaul.Systems
{
    public static class PotionDataConverter
    {
        public const int CurrentVersion = 1;

        // ===============================
        // Save → Runtime
        // ===============================
        public static PotionData FromSave(PotionDataSave save)
        {
            if (save == null || save.Data == null)
                return null;

            switch (save.Version)
            {
                case 1:
                    return FromV1(save.Data);

                default:
                    return null;
            }
        }

        private static PotionData FromV1(PotionDataV1 v1)
        {
            return new PotionData
            {
                // Definition reference (resolved later)
                PotionDefinitionId = v1.PotionDefinitionId,

                // Instance identity
                PotionInstanceId = v1.PotionInstanceId,
                CreatedGameTime = v1.CreatedGameTime,
                BrewerEntityId = v1.BrewerEntityId,
                AlchemySkillAtBrew = v1.AlchemySkillAtBrew,

                // Instance configuration
                BrewFlags = v1.BrewFlags,

                // Instance composition/state
                Ingredients = v1.Ingredients,
                Effects = v1.Effects,

                IsSpoiled = v1.IsSpoiled,
                LastSpoilageCheckTime = v1.LastSpoilageCheckTime
            };
        }

        // ===============================
        // Runtime → Save
        // ===============================
        public static PotionDataSave ToSave(PotionData runtime)
        {
            if (runtime == null)
                return null;

            return new PotionDataSave
            {
                Version = CurrentVersion,
                Data = ToV1(runtime)
            };
        }

        private static PotionDataV1 ToV1(PotionData runtime)
        {
            return new PotionDataV1
            {
                PotionDefinitionId = runtime.PotionDefinitionId,

                PotionInstanceId = runtime.PotionInstanceId,
                CreatedGameTime = runtime.CreatedGameTime,
                BrewerEntityId = runtime.BrewerEntityId,
                AlchemySkillAtBrew = runtime.AlchemySkillAtBrew,

                BrewFlags = runtime.BrewFlags,

                Ingredients = runtime.Ingredients,
                Effects = runtime.Effects,

                IsSpoiled = runtime.IsSpoiled,
                LastSpoilageCheckTime = runtime.LastSpoilageCheckTime
            };
        }
    }
}