using System.Collections.Generic;

namespace AlchemyOverhaul.Player.Skills
{
    public static class AlchemySkill
    {
        private const int MinLevel = 1;
        private const int MaxLevel = 100;

        public static int Level { get; private set; } = 1;
        public static int Experience { get; private set; } = 0;
        public static int TotalExperience { get; private set; } = 0;

        public static void LoadFromSave(int level, int xp, int totalXp)
        {
            Level = level;
            Experience = xp;
            TotalExperience = totalXp;
        }

        public static int SetLevel(int value)
        {
            Level = Clamp(value);
            NotifyLevelChange();
            return Level;
        }

        public static void Increase(int amount, bool notify = false)
        {
            Level = Clamp(Level + amount);
            if (notify) { NotifyLevelChange(); }
        }

        public static void Decrease(int amount, bool notify = false)
        {
            Level = Clamp(Level - amount);
            if (notify) { NotifyLevelChange(); }
        }

        private static int Clamp(int value)
        {
            if (value < MinLevel) return MinLevel;
            if (value > MaxLevel) return MaxLevel;
            return value;
        }

        private static void NotifyLevelChange()
        {
            DaggerfallWorkshop.Game.DaggerfallUI.AddHUDText($"Your Alchemy Skill Is Now {Level}", 2f);
        }

        public static void AddExperience(int amount)
        {
            Experience += amount;
            TotalExperience += amount;

            while (Experience >= GetXpToNextLevel())
            {
                Experience -= GetXpToNextLevel();
                Increase(1, true);
            }
        }

        public static void ZeroCurrentXP()
        {
            Experience = 0;
        }

        private static int GetXpToNextLevel()
        {
            return 2 + (Level * 1);
        }

        public static float GetPowerMultiplier()
        {
            return 1.0f + (Level * 0.01f); // +1% per level
        }
    }
}