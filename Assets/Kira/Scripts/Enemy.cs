using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

namespace Kira
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private SplineAnimate splineAnimate;
        [SerializeField] private GameObject enemyModel;

        private bool hasStarted;
        public event Action<Enemy, bool> OnEnemyDone;

        public void Init(SplineContainer splineContainer)
        {
            splineAnimate.Container = splineContainer;
            splineAnimate.Updated += OnUpdated;
            StartCoroutine(PlayWhenReady());
        }

        private void OnUpdated(Vector3 position, Quaternion rotation)
        {
            if (splineAnimate.IsPlaying || !hasStarted) return;
            enemyModel.SetActive(false);
            OnEnemyDone?.Invoke(this, false);
        }

        private IEnumerator PlayWhenReady()
        {
            while (!splineAnimate.didAwake || !splineAnimate.didStart)
            {
                yield return null;
            }

            splineAnimate.Play();
            enemyModel.SetActive(true);
            hasStarted = true;
        }
    }
}