using UnityEngine;

namespace Kira
{
    public class ProjectileHitEffect : MonoBehaviour
    {
        public float destroyTime = 1f;

        private void Start()
        {
            Destroy(gameObject, destroyTime);
        }
    }
}