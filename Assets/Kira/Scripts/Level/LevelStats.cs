using System;

namespace Kira
{
    public class LevelStats
    {
        private int m_CurHealth;
        private int m_CurGems;
        private int m_CurRound;

        public Action<int, int> OnGemsChanged;
        public Action<int, int> OnHealthChanged;
        public Action<int> OnRoundCompleted;

        public LevelStats(int startHealth, int startGems)
        {
            m_CurHealth = startHealth;
            m_CurGems = startGems;
            m_CurRound = 0;
        }

        public int Round
        {
            get => m_CurRound;
            private set => m_CurRound = value;
        }

        public int Health
        {
            get => m_CurHealth;
            private set => m_CurHealth = value;
        }

        public int Gems
        {
            get => m_CurGems;
            private set => m_CurGems = value;
        }

        public void AddGems(int amount)
        {
            Gems += amount;
            OnGemsChanged?.Invoke(amount, m_CurGems);
        }

        public void SpendGems(int amount)
        {
            Gems -= amount;
            OnGemsChanged?.Invoke(-amount, m_CurGems);
        }

        public void RemoveHealth(int removeAmount)
        {
            Health -= removeAmount;
            OnHealthChanged?.Invoke(removeAmount, m_CurHealth);
        }

        public void NextRound()
        {
            Round++;
            OnRoundCompleted?.Invoke(Round);
        }
    }
}