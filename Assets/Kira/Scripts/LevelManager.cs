using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

namespace Kira
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private LevelSettings levelSettings;
        [SerializeField] private SplineContainer splineContainer;
        [SerializeField] private Enemy enemyPrefab;
        private Spline spline;
        private Vector3 startPosition;

        private void Start()
        {
            spline = splineContainer.Spline;
            startPosition = spline[0].Position;

            if (levelSettings.rounds.Count > 0)
            {
                StartCoroutine(StartRound(levelSettings.rounds[0]));
            }
        }

        private IEnumerator StartRound(RoundSetting round)
        {
            int enemiesSpawned = 0;

            while (enemiesSpawned < round.spawnAmount)
            {
                enemiesSpawned++;
                yield return new WaitForSeconds(round.spawnRate);
            }
        }

        public void SpawnEnemy()
        {
            Enemy enemy = Instantiate(enemyPrefab, startPosition, Quaternion.identity);
            enemy.Init(splineContainer);
        }
    }
}