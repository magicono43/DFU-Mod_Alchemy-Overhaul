using AlchemyOverhaul.Data;
using UnityEngine;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace AlchemyOverhaul.Runtime
{
    public static class PotionFactory
    {
        public static AlchemyPotionData_v1 CreatePotion(
            PotionRecipeData recipe,
            PotionResultData result,
            int uses)
        {
            recipe.randomSeed = recipe.randomSeed != 0
                ? recipe.randomSeed
                : Random.Range(int.MinValue, int.MaxValue);

            var potion = new AlchemyPotionData_v1
            {
                recipe = recipe,
                result = result,
                state = new PotionStateData
                {
                    maxUses = uses,
                    remainingUses = uses,
                    identified = false,
                    ageInDays = 0
                }
            };

            potion.potionId = PotionIdUtility.GeneratePotionId(recipe);
            return potion;
        }
    }
    
    public static class PotionIdUtility
    {
        public static string GeneratePotionId(PotionRecipeData recipe)
        {
            var sb = new StringBuilder();
            sb.Append("Alchemy_v1|");
            sb.Append(recipe.preparationMethod).Append("|");

            var sorted = recipe.ingredients
                .OrderBy(i => i.ingredientId)
                .ThenBy(i => i.quantity);

            foreach (var ing in sorted)
                sb.Append(ing.ingredientId).Append(":").Append(ing.quantity).Append("|");

            sb.Append(recipe.randomSeed);

            return Hash(sb.ToString());
        }

        private static string Hash(string input)
        {
            using (SHA1 sha1 = SHA1.Create())
            {
                byte[] data = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                return System.Convert.ToBase64String(data);
            }
        }
    }
    
    public static class PotionValidator
    {
        public static bool IsValid(AlchemyPotionData_v1 potion)
        {
            if (potion == null) return false;
            if (potion.schemaVersion != AlchemyPotionData_v1.CURRENT_SCHEMA) return false;
            if (string.IsNullOrEmpty(potion.potionId)) return false;
            if (potion.recipe == null || potion.result == null || potion.state == null) return false;

            return true;
        }
    }
}
