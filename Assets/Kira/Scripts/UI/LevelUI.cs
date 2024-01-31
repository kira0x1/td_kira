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

        private LevelManager m_LevelManager;
        private SpeedUI m_SpeedUI;

        private void Start()
        {
            m_LevelManager = FindAnyObjectByType<LevelManager>();
            m_SpeedUI = FindFirstObjectByType<SpeedUI>();
            LevelStats levelStats = m_LevelManager.levelStats;
            levelStats.OnHealthChanged += OnHealthChanged;
            levelStats.OnGemsChanged += OnGemsChanged;
            levelStats.OnRoundCompleted += OnRoundCompleted;

            healthText.text = m_LevelManager.levelSettings.startHealth.ToString();
            gemsText.text = m_LevelManager.levelSettings.startGems.ToString();
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
            m_LevelManager.StartLevel();
            startBtnPanel.HidePanel();
            m_SpeedUI.UpdatePlayBtnSprite();
        }
    }
}