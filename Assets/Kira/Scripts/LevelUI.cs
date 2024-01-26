using TMPro;
using UnityEngine;

namespace Kira
{
    public class LevelUI : MonoBehaviour
    {
        public TextMeshProUGUI healthUI;
        private LevelManager levelManager;

        private void Awake()
        {
            levelManager = FindAnyObjectByType<LevelManager>();
            levelManager.OnHealthChanged += OnHealthChanged;
            healthUI.text = levelManager.levelSettings.health.ToString();
        }

        private void OnHealthChanged(int damage, int curHealth)
        {
            healthUI.text = curHealth.ToString();
        }
    }
}