using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Kira
{
    public class Tower : MonoBehaviour
    {
        public TowerData towerData;
        public Projectile projectilePrefab;

        [SerializeField] private Transform aimTransform;
        [FormerlySerializedAs("projectileSpawn"), SerializeField] private Transform projectileSpawnTransform;
        [SerializeField] private SphereCollider triggerCollider;

        private Enemy enemyTarget;
        private bool hasTarget;
        private readonly List<Enemy> enemiesInRange = new List<Enemy>();

        private float nextAttackTime;

        private void Update()
        {
            if (!hasTarget) return;

            if (enemyTarget.isDead)
            {
                RemoveEnemy(enemyTarget);
                return;
            }

            // TODO: remove later, only using this because for some reason will look at when enemies are spawned
            float distance = Vector3.Distance(transform.position, enemyTarget.transform.position);
            if (distance > triggerCollider.radius) return;

            Vector3 lookAtPos = enemyTarget.transform.position;
            lookAtPos.y = aimTransform.position.y;
            aimTransform.LookAt(lookAtPos);

            HandleShooting();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                Enemy enemy = other.GetComponent<Enemy>();
                enemiesInRange.Add(enemy);

                if (!hasTarget)
                {
                    enemyTarget = enemy;
                    hasTarget = true;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                Enemy enemy = other.GetComponent<Enemy>();
                RemoveEnemy(enemy);
            }
        }

        private void RemoveEnemy(Enemy enemy)
        {
            enemiesInRange.Remove(enemy);

            if (enemiesInRange.Count > 0)
            {
                enemyTarget = enemiesInRange[0];
                hasTarget = true;
            }
            else
            {
                hasTarget = false;
            }
        }

        private void HandleShooting()
        {
            if (Time.time < nextAttackTime) return;
            nextAttackTime = Time.time + towerData.attackSpeed;
            Projectile projectile = Instantiate(projectilePrefab, projectileSpawnTransform.position, Quaternion.LookRotation(aimTransform.forward));
            projectile.damage = towerData.attackDamage;
            projectile.SetTarget(enemyTarget);
        }
    }
}