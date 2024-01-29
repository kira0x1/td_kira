using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Kira
{
    public class LevelUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI healthText;
        [SerializeField]
        private TextMeshProUGUI gemsText;
        [SerializeField]
        private PanelUI startBtnPanel;

        private LevelManager levelManager;

        private void Start()
        {
            levelManager = FindAnyObjectByType<LevelManager>();
            levelManager.levelStats.OnHealthChanged += OnHealthChanged;
            levelManager.levelStats.OnGemsChanged += OnGemsChanged;

            healthText.text = levelManager.levelSettings.startHealth.ToString();
            gemsText.text = levelManager.levelSettings.startGems.ToString();
        }

        private void OnHealthChanged(int damage, int curHealth)
        {
            healthText.text = curHealth.ToString();
        }

        private void OnGemsChanged(int amount, int curGems)
        {
            gemsText.text = curGems.ToString();
        }

        public void OnStartGameClicked()
        {
            levelManager.StartLevel();
            startBtnPanel.HidePanel();
        }
    }
}