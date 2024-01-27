using System;
using UnityEngine;

namespace Kira
{
    public class TowerTrigger : MonoBehaviour
    {
        public Action<Enemy> OnEnemyEnterTrigger;
        public Action<Enemy> OnEnemyExitTrigger;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                Enemy enemy = other.GetComponent<Enemy>();
                OnEnemyEnterTrigger?.Invoke(enemy);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                Enemy enemy = other.GetComponent<Enemy>();
                OnEnemyExitTrigger?.Invoke(enemy);
            }
        }
    }
}