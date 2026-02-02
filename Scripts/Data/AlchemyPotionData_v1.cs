using System;
using System.Collections.Generic;

namespace AlchemyOverhaul.Data
{
    // =========================
    // ROOT DATA CONTRACT
    // =========================
    [Serializable]
    public class AlchemyPotionData_v1
    {
        public const int CURRENT_SCHEMA = 1;

        public string potionId;
        public int schemaVersion = CURRENT_SCHEMA;

        public PotionRecipeData recipe;
        public PotionResultData result;
        public PotionStateData state;
    }

    // =========================
    // RECIPE / IDENTITY
    // =========================
    [Serializable]
    public class PotionRecipeData
    {
        public List<IngredientEntry> ingredients = new List<IngredientEntry>();
        public PreparationMethod preparationMethod;
        public AlchemyToolFlags toolsUsed;

        public int alchemySkillAtBrew;
        public int labQuality;
        public int toolQuality;

        public int randomSeed;
    }

    [Serializable]
    public class IngredientEntry
    {
        public string ingredientId;
        public int quantity;
    }

    // =========================
    // RESULT / EFFECTS
    // =========================
    [Serializable]
    public class PotionResultData
    {
        public List<PotionEffectData> effects = new List<PotionEffectData>();

        public float basePotency;
        public float baseToxicity;
        public float basePalatability;
        public float baseInstability;
        public float baseSpoilageRate;

        public float instability;
        public float potencyMultiplier;
        public bool isCorrupted;
    }

    [Serializable]
    public class PotionEffectData
    {
        public string effectKey;
        public int magnitude;
        public int duration;
        public EffectTarget target;
        public EffectApplicationFlags flags;
    }

    // =========================
    // RUNTIME STATE
    // =========================
    [Serializable]
    public class PotionStateData
    {
        public int remainingUses;
        public int maxUses;

        public int ageInDays;
        public bool isSpoiled;
        public bool identified;
    }

    // =========================
    // ENUMS
    // =========================
    public enum PreparationMethod
    {
        FieldKit,
        AlchemyTable,
        AlchemyLab,
        AdvancedLab
    }

    [Flags]
    public enum AlchemyToolFlags
    {
        None = 0,
        Mortar = 1 << 0,
        Alembic = 1 << 1,
        Calcinator = 1 << 2,
        Retort = 1 << 3,
        Brazier = 1 << 4
    }

    public enum EffectTarget
    {
        Self,
        Touch,
        Area
    }

    [Flags]
    public enum EffectApplicationFlags
    {
        None = 0,
        Instant = 1 << 0,
        OverTime = 1 << 1,
        Harmful = 1 << 2,
        Beneficial = 1 << 3
    }
}
