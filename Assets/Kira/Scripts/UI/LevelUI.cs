using TMPro;
using UnityEngine;

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
        [SerializeField]
        private TextMeshProUGUI roundText;

        private LevelManager levelManager;

        private void Start()
        {
            levelManager = FindAnyObjectByType<LevelManager>();
            LevelStats levelStats = levelManager.levelStats;
            levelStats.OnHealthChanged += OnHealthChanged;
            levelStats.OnGemsChanged += OnGemsChanged;
            levelStats.OnRoundCompleted += OnRoundCompleted;

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

        private void OnRoundCompleted(int curRound)
        {
            roundText.text = $"Round {curRound + 1}";
        }

        public void OnStartGameClicked()
        {
            levelManager.StartLevel();
            startBtnPanel.HidePanel();
        }
    }
}