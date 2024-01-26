using UnityEngine;

namespace Kira
{
    [CreateAssetMenu(fileName = "New TowerData", menuName = "Kira/Tower")]
    public class TowerData : ScriptableObject
    {
        public float attackSpeed = 0.25f;
        public int attackDamage = 1;
    }
}