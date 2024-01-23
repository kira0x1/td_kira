using System.Collections.Generic;
using UnityEngine;

namespace Kira
{
    public class LevelStats : MonoBehaviour
    {
        public int Health = 100;
    }

    [System.Serializable]
    public class LevelSettings
    {
        // Time until next round starts
        public float roundDelay = 2.5f;

        public List<RoundSetting> rounds = new List<RoundSetting>();
        public int RoundsCount => rounds.Count;
    }

    [System.Serializable]
    public struct RoundStat
    {
        public int healthLost;
    }

    [System.Serializable]
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