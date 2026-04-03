using AlchemyOverhaul.Data.Definitions;
using System.Collections.Generic;

namespace AlchemyOverhaul.Crafting.UI
{
    public static class ResultInfoTextConstructor
    {
        public static string[] GetInfoTextBasedOnPotionEffect(List<PotionEffectBlueprint> effects)
        {
            List<string> descs = new List<string>();

            // Tomorrow, maybe see about working on what happens when you have more than 2 ingredients with a matching effect on them, so mostly combination math stuff.
            // Also just more testing in general for all the different spell effects and such, for their text especially when combined as well, etc.

            foreach (PotionEffectBlueprint effect in effects)
            {
                string key = effect.EffectKey;
                int minMag = effect.MinMagnitude;
                int maxMag = effect.MaxMagnitude;
                int minDur = effect.MinDuration;
                int maxDur = effect.MaxDuration;

                switch (key)
                {
                    case "Climbing": descs.Add("Climbing for " +minDur+ " to " +maxDur+ " Rounds."); break;
                    case "ElementalResistance-Fire": descs.Add(minMag+ "% to " + maxMag+ "% chance to Resist Fire effects, for " +minDur+ " to " +maxDur+ " Rounds."); break;
                    case "ElementalResistance-Frost": descs.Add(minMag+ "% to " + maxMag+ "% chance to Resist Frost effects, for " +minDur+ " to " +maxDur+ " Rounds."); break;
                    case "ElementalResistance-Poison": descs.Add(minMag+ "% to " + maxMag+ "% chance to Resist Poison effects, for " +minDur+ " to " +maxDur+ " Rounds."); break;
                    case "ElementalResistance-Shock": descs.Add(minMag+ "% to " + maxMag+ "% chance to Resist Shock effects, for " +minDur+ " to " +maxDur+ " Rounds."); break;
                    case "ElementalResistance-Magicka": descs.Add(minMag+ "% to " + maxMag+ "% chance to Resist Magic effects, for " +minDur+ " to " +maxDur+ " Rounds."); break;
                    case "Jumping": descs.Add("Jumping for " +minDur+ " to " +maxDur+ " Rounds."); break;
                    case "Paralyze": descs.Add("Paralysis for " +minDur+ " to " +maxDur+ " Rounds."); break;
                    case "Slowfall": descs.Add("Slowfall for " +minDur+ " to " +maxDur+ " Rounds."); break;
                    case "WaterBreathing": descs.Add("Water Breathing for " +minDur+ " to " +maxDur+ " Rounds."); break;
                    case "ContinuousDamage-Fatigue": descs.Add("Damage Fatigue " +minMag+ " to " +maxMag+ " each round, for " +minDur+ " to " +maxDur+ " Rounds."); break;
                    case "ContinuousDamage-Health": descs.Add("Damage Health " +minMag + " to " +maxMag+ " each round, for " +minDur+ " to " +maxDur+ " Rounds."); break;
                    case "ContinuousDamage-SpellPoints": descs.Add("Damage Spell Points " +minMag+ " to " +maxMag+ " each round, for " +minDur+ " to " +maxDur+ " Rounds."); break;
                    case "Damage-Fatigue": descs.Add("Damage Fatigue " +minMag+ " to " +maxMag+ " points."); break;
                    case "Damage-Health": descs.Add("Damage Health " +minMag+ " to " +maxMag+ " points."); break;
                    case "Damage-SpellPoints": descs.Add("Damage Spell Points " +minMag+ " to " +maxMag+ "."); break;
                    case "Drain-Agility": descs.Add("Damage Agility " +minMag+ " to " +maxMag+ " points."); break;
                    case "Drain-Endurance": descs.Add("Damage Endurance " +minMag + " to " +maxMag+ " points."); break;
                    case "Drain-Intelligence": descs.Add("Damage Intelligence " +minMag+ " to " +maxMag+ " points."); break;
                    case "Drain-Luck": descs.Add("Damage Luck " +minMag+ " to " +maxMag+ " points."); break;
                    case "Drain-Personality": descs.Add("Damage Personality " +minMag+ " to " +maxMag+ " points."); break;
                    case "Drain-Speed": descs.Add("Damage Speed " +minMag+ " to " +maxMag+ " points."); break;
                    case "Drain-Strength": descs.Add("Damage Strength " +minMag+ " to " +maxMag+ " points."); break;
                    case "Drain-Willpower": descs.Add("Damage Willpower " +minMag+ " to " +maxMag+ " points."); break;
                    case "Chameleon-Normal": descs.Add("Normal Chameleon for " +minDur+ " to " +maxDur+ " Rounds."); break;
                    case "Chameleon-True": descs.Add("True Chameleon for " +minDur+ " to " +maxDur+ " Rounds."); break;
                    case "Invisibility-Normal": descs.Add("Normal Invisibility for " +minDur+ " to " +maxDur+ " Rounds."); break;
                    case "Invisibility-True": descs.Add("True Invisibility for " +minDur+ " to " +maxDur+ " Rounds."); break;
                    case "Shadow-Normal": descs.Add("Normal Shadow for " +minDur+ " to " +maxDur+ " Rounds."); break;
                    case "Shadow-True": descs.Add("True Shadow for " +minDur+ " to " +maxDur+ " Rounds."); break;
                    case "ComprehendLanguages": descs.Add("Comprehend Languages with a " +minMag+ "% to " +maxMag+ "% success chance, for " +minDur+ " to " +maxDur+ " Rounds."); break;
                    case "Dispel-Magic": descs.Add("Dispel Magic effects with a " +minMag+ "% to " +maxMag+ "% success chance."); break;
                    case "Silence": descs.Add("Silence for " +minDur+ " to " +maxDur+ " Rounds."); break;
                    case "Cure-Disease": descs.Add("Cure All Diseases."); break;
                    case "Cure-Paralyzation": descs.Add("Cure Paralysis."); break;
                    case "Cure-Poison": descs.Add("Cure All Poisoning."); break;
                    case "Fortify-Agility": descs.Add("Fortify Agility " +minMag+ " to " +maxMag+ " points, for " +minDur+ " to " +maxDur+ " Rounds."); break;
                    case "Fortify-Endurance": descs.Add("Fortify Endurance " + minMag+ " to " +maxMag+ " points, for " +minDur+ " to " +maxDur+ " Rounds."); break;
                    case "Fortify-Intelligence": descs.Add("Fortify Intelligence " + minMag+ " to " +maxMag+ " points, for " +minDur+ " to " +maxDur+ " Rounds."); break;
                    case "Fortify-Luck": descs.Add("Fortify Luck " + minMag+ " to " +maxMag+ " points, for " +minDur+ " to " +maxDur+ " Rounds."); break;
                    case "Fortify-Personality": descs.Add("Fortify Personality " + minMag+ " to " +maxMag+ " points, for " +minDur+ " to " +maxDur+ " Rounds."); break;
                    case "Fortify-Speed": descs.Add("Fortify Speed " + minMag+ " to " +maxMag+ " points, for " +minDur+ " to " +maxDur+ " Rounds."); break;
                    case "Fortify-Strength": descs.Add("Fortify Strength " + minMag+ " to " +maxMag+ " points, for " +minDur+ " to " +maxDur+ " Rounds."); break;
                    case "Fortify-Willpower": descs.Add("Fortify Willpower " + minMag+ " to " +maxMag+ " points, for " +minDur+ " to " +maxDur+ " Rounds."); break;
                    case "FreeAction": descs.Add("Free Action for " +minDur+ " to " +maxDur+ " Rounds."); break;
                    case "Heal-Agility": descs.Add("Heal Agility " +minMag+ " to " +maxMag+ " points."); break;
                    case "Heal-Endurance": descs.Add("Heal Endurance " + minMag+ " to " +maxMag+ " points."); break;
                    case "Heal-Fatigue": descs.Add("Restore Fatigue by " +minMag+ " to " +maxMag+ " points."); break;
                    case "Heal-Health": descs.Add("Restore Health by " +minMag+ " to " +maxMag+ " points."); break;
                    case "Heal-Intelligence": descs.Add("Heal Intelligence " + minMag+ " to " +maxMag+ " points."); break;
                    case "Heal-Luck": descs.Add("Heal Luck " + minMag+ " to " +maxMag+ " points."); break;
                    case "Heal-Personality": descs.Add("Heal Personality " + minMag+ " to " +maxMag+ " points."); break;
                    case "Heal-Speed": descs.Add("Heal Speed " + minMag+ " to " +maxMag+ " points."); break;
                    case "Heal-SpellPoints": descs.Add("Restore Spell Points by " +minMag+ " to " +maxMag+ " points."); break;
                    case "Heal-Strength": descs.Add("Heal Strength " + minMag+ " to " +maxMag+ " points."); break;
                    case "Heal-Willpower": descs.Add("Heal Willpower " + minMag+ " to " +maxMag+ " points."); break;
                    case "Regenerate": descs.Add("Regenerate Health " +minMag+ " to " +maxMag+ " points each round, for " +minDur+ " to " +maxDur+ " Rounds."); break;
                    case "SpellAbsorption": descs.Add("Spell Absorption with a " +minMag+ "% to " +maxMag+ "% success chance, for " +minDur+ " to " +maxDur+ " Rounds."); break;
                    case "Detect-Enemy": descs.Add("Detect Enemy for " +minDur+ " to " +maxDur+ " Rounds."); break;
                    case "Detect-Magic": descs.Add("Detect Magic for " +minDur+ " to " +maxDur+ " Rounds."); break;
                    case "Detect-Treasure": descs.Add("Detect Treasure for " +minDur+ " to " +maxDur+ " Rounds."); break;
                    case "Levitate": descs.Add("Levitate for " +minDur+ " to " +maxDur+ " Rounds."); break;
                    case "Pacify-Animal": descs.Add(minMag+ "% to " +maxMag+ "% chance to Pacify Animals."); break;
                    case "Pacify-Daedra": descs.Add(minMag+ "% to " +maxMag+ "% chance to Pacify Daedra."); break;
                    case "Pacify-Humanoid": descs.Add(minMag+ "% to " +maxMag+ "% chance to Pacify Humanoids."); break;
                    case "Pacify-Undead": descs.Add(minMag+ "% to " +maxMag+ "% chance to Pacify Undead."); break;
                    case "SpellReflection": descs.Add("Spell Reflection with a " +minMag+ "% to " +maxMag+ "% success chance, for " +minDur+ " to " +maxDur+ " Rounds."); break;
                    case "SpellResistance": descs.Add("Spell Resistance with a " + minMag+ "% to " +maxMag+ "% success chance, for " +minDur+ " to " +maxDur+ " Rounds."); break;
                    case "WaterWalking": descs.Add("Water Walking for " +minDur+ " to " +maxDur+ " Rounds."); break;
                }
            }

            return descs.ToArray();
        }
    }
}
