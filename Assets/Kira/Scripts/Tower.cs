using System.Collections.Generic;
using UnityEngine;

namespace Kira
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private Transform aimTransform;
        [SerializeField] private SphereCollider triggerCollider;

        private Enemy enemyTargeting;
        private bool hasTarget;


        private List<Enemy> enemiesInRange = new List<Enemy>();

        private void Update()
        {
            if (!hasTarget) return;

            // TODO: remove later, only using this because for some reason will look at when enemies are spawned
            float distance = Vector3.Distance(transform.position, enemyTargeting.transform.position);
            if (distance > triggerCollider.radius) return;

            Vector3 lookAtPos = enemyTargeting.transform.position;
            lookAtPos.y = aimTransform.position.y;
            aimTransform.LookAt(lookAtPos);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                Enemy enemy = other.GetComponent<Enemy>();
                enemiesInRange.Add(enemy);

                if (!hasTarget)
                {
                    enemyTargeting = enemy;
                    hasTarget = true;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                Enemy enemy = other.GetComponent<Enemy>();
                enemiesInRange.Remove(enemy);

                if (enemiesInRange.Count > 0)
                {
                    enemyTargeting = enemiesInRange[0];
                    hasTarget = true;
                }
                else
                {
                    hasTarget = false;
                }
            }
        }

        private void HandleNoTargets()
        {
        }
    }
}