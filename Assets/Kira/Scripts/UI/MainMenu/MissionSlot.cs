using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Kira.Menu
{
    public class MissionSlot : MonoBehaviour
    {
        private MissionData m_Mission;
        [SerializeField] private TextMeshProUGUI m_Text;
        [SerializeField] private Image m_Icon;

        public void SetMission(MissionData missionData)
        {
            m_Mission = missionData;
            m_Text.text = missionData.Title;
            if (missionData.Icon)
            {
                m_Icon.enabled = true;
                m_Icon.sprite = missionData.Icon;
            }
            else
            {
                m_Icon.enabled = false;
            }
        }

        public void OnClick()
        {
            SceneManager.LoadScene(m_Mission.SceneId);
        }
    }
}