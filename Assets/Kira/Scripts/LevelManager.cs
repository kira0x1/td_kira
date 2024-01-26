using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

namespace Kira
{
    public class LevelManager : MonoBehaviour
    {
        public int enemiesAlive;

        [SerializeField] private LevelSettings levelSettings;
        [SerializeField] private SplineContainer splineContainer;
        [SerializeField] private Enemy enemyPrefab;

        [SerializeField]
        private int curRound;
        [SerializeField]
        private int endRound;

        private int enemiesToSpawn;

        private void Start()
        {
            endRound = levelSettings.rounds.Count;

            if (endRound > 0)
            {
                StartCoroutine(StartRound(0));
            }
        }

        private IEnumerator StartRound(int roundIndex, bool startWithDelay = false)
        {
            Debug.Log($"Starting round: {roundIndex}");
            RoundSetting round = levelSettings.rounds[roundIndex];
            enemiesToSpawn = round.spawnAmount;

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


            while (enemiesToSpawn > 0 && enemiesAlive > 0)
            {
                yield return null;
            }

            HandleRoundEnd();
        }

        private void SpawnEnemy()
        {
            Enemy enemy = Instantiate(enemyPrefab);
            enemiesAlive++;
            enemy.OnEnemyDone += OnEnemyDone;
            enemy.Init(splineContainer);
        }

        private void OnEnemyDone(Enemy enemy, bool playerDestroyed)
        {
            if (!playerDestroyed) levelSettings.health--;
            enemiesAlive--;
            enemy.OnEnemyDone -= OnEnemyDone;
        }

        private void HandleRoundEnd()
        {
            if (curRound + 1 >= endRound)
            {
                return;
            }

            curRound++;
            StartCoroutine(StartRound(curRound, true));
        }
    }
}