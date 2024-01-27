using UnityEngine;

namespace Kira
{
    [CreateAssetMenu(fileName = "New TowerData", menuName = "Kira/Tower")]
    public class TowerData : ScriptableObject
    {
        public Sprite icon;
        public int cost = 50;
        public Tower towerPrefab;

        [Header("Combat Stats")]
        public float attackSpeed = 0.25f;
        public int attackDamage = 1;
    }
}