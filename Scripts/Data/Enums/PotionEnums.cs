using System;

namespace AlchemyOverhaul.Data.Enums
{
    public enum PotionEffectKey
    {
        RestoreHealth,
        RegenerateHealth,
        FortifyStrength,
        DamageFatigue,
        CurePoison,
        // etc
    }
    
    [Flags]
    public enum PotionBrewFlags
    {
        None                    = 0,
        HeatedLow               = 1 << 0,
        HeatedMedium            = 1 << 1,
        HeatedHigh              = 1 << 2,

        SpecialEffectTriggered  = 1 << 3,
        ExperimentalBrew        = 1 << 4,

        UsedAlembic             = 1 << 5,
        UsedRetort              = 1 << 6,
        UsedCalcinator          = 1 << 7,

        MasterToolBonus         = 1 << 8
    }
    
    [Flags]
    public enum IngredientEffectMask
    {
        None        = 0,
        Effect1     = 1 << 0,
        Effect2     = 1 << 1,
        Effect3     = 1 << 2,
        Effect4     = 1 << 3,
        Special     = 1 << 4
    }
    
    public enum EffectScalingModel
    {
        Additive,
        Multiplicative,
        Custom
    }
    
    public enum EffectIdentificationLevel
    {
        Unknown,
        Partial,    // Effect known, magnitude/duration unknown
        Full
    }
}
