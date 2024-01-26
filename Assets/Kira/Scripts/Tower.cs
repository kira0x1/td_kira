using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Kira
{
    public class Tower : MonoBehaviour
    {
        public TowerData towerData;
        public Projectile projectilePrefab;

        [SerializeField] private Transform aimTransform;
        [SerializeField] private Transform projectileSpawnTransform;
        [SerializeField] private SphereCollider triggerCollider;

        private Enemy enemyTarget;
        private Transform enemyPivot;
        private bool hasTarget;
        private readonly Dictionary<Guid, Enemy> enemiesInRange = new Dictionary<Guid, Enemy>();
        private float nextAttackTime;
        private Vector3 defaultAimPos;

        private void Start()
        {
            defaultAimPos = aimTransform.position;
        }

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

            HandleAiming();
            HandleShooting();
        }

        private void HandleAiming()
        {
            Vector3 aimPos = aimTransform.position;
            Vector3 enemyPos = enemyPivot.position;
            enemyPos.y = aimPos.y;

            Vector3 lookDir = enemyPos - aimPos;
            Quaternion lookRot = Quaternion.LookRotation(lookDir);
            aimTransform.rotation = lookRot;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                Enemy enemy = other.GetComponent<Enemy>();
                enemiesInRange.Add(enemy.UUID, enemy);

                if (!hasTarget)
                {
                    enemyTarget = enemy;
                    enemyPivot = enemyTarget.transform;
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
            enemiesInRange.Remove(enemy.UUID);

            if (enemiesInRange.Count > 0)
            {
                enemyTarget = enemiesInRange.First().Value;
                enemyPivot = enemyTarget.Pivot;
                hasTarget = true;
            }
            else
            {
                aimTransform.position = defaultAimPos;
                aimTransform.localRotation = Quaternion.Euler(Vector3.zero);
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