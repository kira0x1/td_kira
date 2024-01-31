using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Kira
{
    public class SpeedUI : MonoBehaviour
    {
        [SerializeField] private Image playIconUI;
        [SerializeField] private Sprite playSprite;
        [SerializeField] private Sprite pauseSprite;
        [SerializeField] private TextMeshProUGUI speedText;

        private SpeedManager m_SpeedManager;

        private void Awake()
        {
            m_SpeedManager = FindFirstObjectByType<SpeedManager>();
        }

        public void OnPlayBtnClicked()
        {
            m_SpeedManager.TogglePause();
            UpdatePlayBtnSprite();
        }

        public void UpdatePlayBtnSprite()
        {
            playIconUI.sprite = m_SpeedManager.IsPaused ? playSprite : pauseSprite;
        }

        public void OnSpeedIncreaseClicked()
        {
            m_SpeedManager.IncreaseSpeed();
            UpdateSpeedText();
        }

        public void OnSpeedDecreaseClicked()
        {
            m_SpeedManager.DecreaseSpeed();
            UpdateSpeedText();
        }

        private void UpdateSpeedText()
        {
            speedText.text = m_SpeedManager.m_SpeedState switch
            {
                SpeedManager.SpeedState.SLOW => "x1/2",
                SpeedManager.SpeedState.NORMAL => "x1",
                SpeedManager.SpeedState.FAST => "x2",
                SpeedManager.SpeedState.FASTEST => "x3",
                _ => speedText.text
            };
        }
    }
}