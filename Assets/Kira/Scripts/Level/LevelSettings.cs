using System;
using System.Collections.Generic;

namespace Kira
{
    [Serializable]
    public class LevelSettings
    {
        public int startHealth = 50;
        public int startGems = 650;
        public int baseRoundIncome = 50;

        // Time until next round starts
        public float roundDelay = 2.5f;

        public List<RoundSetting> rounds = new List<RoundSetting>();
        public int RoundsCount => rounds.Count;
    }

    [Serializable]
    public struct RoundStat
    {
        public int healthLost;
    }

    [Serializable]
    public class RoundSetting
    {
        public float spawnRate = 2f;
        public int spawnAmount = 10;
    }

    public enum EnemyType
    {
        BASIC,
        FAST,
        LEAD
    }
}