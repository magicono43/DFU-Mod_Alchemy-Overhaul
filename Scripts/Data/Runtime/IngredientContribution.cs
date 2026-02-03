using System.Collections.Generic;
using AlchemyOverhaul.Data.Enums;

namespace AlchemyOverhaul.Data.Runtime
{
    /// <summary>
    /// Frozen contribution of a single ingredient to a potion at brew time.
    /// Serialized as part of PotionData.
    /// </summary>
    public sealed class IngredientContribution
    {
        // ===== Ingredient Identity =====

        /// <summary>
        /// Stable identifier for the ingredient definition.
        /// Example: "ao.ing.nirnroot"
        /// </summary>
        public string IngredientId;

        /// <summary>
        /// Optional: UID of the consumed inventory item.
        /// Used for analytics/debugging only.
        /// </summary>
        public ulong SourceItemUid;

        // ===== Quantity & Quality =====

        /// <summary>
        /// Units of this ingredient consumed.
        /// </summary>
        public int QuantityUsed;

        /// <summary>
        /// Ingredient quality at brew time (0–100 or mod-defined scale).
        /// </summary>
        public int Quality;

        // ===== Effect Contribution =====

        /// <summary>
        /// Effects this ingredient contributed to the final potion.
        /// These are frozen copies, not references.
        /// </summary>
        public List<PotionEffectInstance> ContributedEffects;

        public IngredientEffectMask ExpressedEffects;
        public bool SpecialEffectTriggered;
    }
}
