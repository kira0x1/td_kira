using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

namespace Kira
{
    public class LevelManager : MonoBehaviour
    {
        public int enemiesAlive;
        public LevelSettings levelSettings;
        public LevelStats levelStats;

        [SerializeField]
        private SplineContainer m_SplineContainer;

        [SerializeField]
        private Enemy m_EnemyPrefab;

        private int m_RoundsLength;
        private int m_EnemiesToSpawn;
        private List<Tower> towersSpawned = new List<Tower>();

        public enum LevelState
        {
            PRE_START,
            PLAYING,
            PAUSED,
            LEVEL_END
        }

        private static LevelState m_LevelState;
        public static LevelState CurrentState => m_LevelState;

        private void Awake()
        {
            // TODO: change when loading manager and stage selector added
            InitalizeLevel();
        }

        // Loads the level and all dependencies, should be called when loading from main menu
        private void InitalizeLevel()
        {
            m_LevelState = LevelState.PRE_START;
            levelStats = new LevelStats(levelSettings.startHealth, levelSettings.startGems);
        }

        public void StartLevel()
        {
            m_RoundsLength = levelSettings.rounds.Count;

            if (m_RoundsLength > 0)
            {
                StartCoroutine(StartRound(0));
                m_LevelState = LevelState.PLAYING;
            }
        }

        private IEnumerator StartRound(int roundIndex, bool startWithDelay = false)
        {
            Debug.Log($"Starting round: {roundIndex}");
            RoundSetting round = levelSettings.rounds[roundIndex];
            m_EnemiesToSpawn = round.spawnAmount;

            if (startWithDelay)
            {
                yield return new WaitForSeconds(levelSettings.roundDelay);
            }

            int enemiesSpawned = 0;

            while (enemiesSpawned < round.spawnAmount)
            {
                enemiesSpawned++;
                SpawnEnemy();
                yield return new WaitForSeconds(round.spawnRate);
            }


            while (m_EnemiesToSpawn > 0 && enemiesAlive > 0)
            {
                yield return null;
            }

            HandleRoundEnd();
        }

        private void SpawnEnemy()
        {
            Enemy enemy = Instantiate(m_EnemyPrefab);
            enemiesAlive++;
            enemy.OnEnemyDone += OnEnemyDone;
            enemy.Init(m_SplineContainer);
        }

        private void OnEnemyDone(Enemy enemy, bool playerDestroyed)
        {
            if (!playerDestroyed)
            {
                RemoveHealth(1);
            }
            else
            {
                levelStats.AddGems(1);
            }

            enemiesAlive--;
            enemy.OnEnemyDone -= OnEnemyDone;
        }

        private void RemoveHealth(int damage)
        {
            levelStats.RemoveHealth(damage);
        }

        public void SpawnTower(TowerData towerData, Vector3 spawnPos)
        {
            Tower tower = Instantiate(towerData.towerPrefab, spawnPos, Quaternion.identity);
            towersSpawned.Add(tower);
            levelStats.SpendGems(towerData.cost);
        }

        private void HandleRoundEnd()
        {
            if (levelStats.Round + 1 >= m_RoundsLength)
            {
                return;
            }


            levelStats.NextRound();
            levelStats.AddGems(levelSettings.baseRoundIncome);
            StartCoroutine(StartRound(levelStats.Round, true));
        }
    }
}