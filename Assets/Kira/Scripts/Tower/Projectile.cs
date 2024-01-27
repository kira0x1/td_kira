using UnityEngine;

namespace Kira
{
    public class Projectile : MonoBehaviour
    {
        public int damage = 1;
        public float speed = 10f;
        public bool hasTarget;
        public Enemy target;

        private Transform projectileTransform;
        private bool hasHit;

        [SerializeField]
        private GameObject projectileGraphic;
        [SerializeField]
        private GameObject hitEffect;

        private void Awake()
        {
            projectileTransform = transform;
        }

        private void Update()
        {
            if (!hasTarget || hasHit) return;

            Vector3 projPos = projectileTransform.position;
            Vector3 targetPos = target.Pivot.position;

            projectileTransform.position = Vector3.MoveTowards(projPos, targetPos, speed * Time.deltaTime);

            float distance = Vector3.Distance(projPos, targetPos);
            if (distance <= 1f)
            {
                OnHit();
            }
        }

        private void OnHit()
        {
            hasHit = true;
            projectileGraphic.SetActive(false);
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            target.Hit(damage);

            //TODO: setup pooling system
            Destroy(gameObject);
        }

        public void SetTarget(Enemy target)
        {
            this.target = target;
            projectileGraphic.SetActive(true);
            hasTarget = true;
        }
    }
}