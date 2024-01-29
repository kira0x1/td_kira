using UnityEngine;

namespace Kira
{
    [RequireComponent(typeof(CanvasGroup))]
    public class PanelUI : MonoBehaviour
    {
        private CanvasGroup m_CanvasGroup;

        private void Awake()
        {
            m_CanvasGroup = GetComponent<CanvasGroup>();
        }

        public void ShowPanel()
        {
            m_CanvasGroup.alpha = 1f;
            m_CanvasGroup.blocksRaycasts = true;
            m_CanvasGroup.interactable = true;
        }

        public void HidePanel()
        {
            m_CanvasGroup.alpha = 0f;
            m_CanvasGroup.blocksRaycasts = false;
            m_CanvasGroup.interactable = false;
        }
    }
}