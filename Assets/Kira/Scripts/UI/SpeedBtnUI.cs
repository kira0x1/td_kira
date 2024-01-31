using UnityEngine;
using UnityEngine.EventSystems;

namespace Kira
{
    public class SpeedBtnUI : MonoBehaviour, IPointerClickHandler
    {
        private SpeedUI m_SpeedUI;

        private void Awake()
        {
            m_SpeedUI = FindFirstObjectByType<SpeedUI>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                m_SpeedUI.OnSpeedIncreaseClicked();
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                m_SpeedUI.OnSpeedDecreaseClicked();
            }
        }
    }
}