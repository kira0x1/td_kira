using System;
using System.Collections;
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

        private int m_CurRound;
        private int m_RoundsLength;
        private int m_EnemiesToSpawn;

        private void Awake()
        {
            // TODO: change when loading manager and stage selector added
            InitalizeLevel();
        }

        private void Start()
        {
            m_RoundsLength = levelSettings.rounds.Count;

            if (m_RoundsLength > 0)
            {
                StartCoroutine(StartRound(0));
            }
        }

        // Loads the level and all dependencies, should be called when loading from main menu
        public void InitalizeLevel()
        {
            levelStats = new LevelStats(levelSettings.startHealth, levelSettings.startGems);
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

        private void HandleRoundEnd()
        {
            if (m_CurRound + 1 >= m_RoundsLength)
            {
                return;
            }

            m_CurRound++;
            levelStats.AddGems(levelSettings.baseRoundIncome);
            StartCoroutine(StartRound(m_CurRound, true));
        }
    }
}