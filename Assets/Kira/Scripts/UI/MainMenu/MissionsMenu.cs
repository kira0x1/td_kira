using UnityEngine;

namespace Kira.Menu
{
    public class MissionsMenu : PanelUI
    {
        [SerializeField]
        private MissionSlot m_SlotPrefab;
        [SerializeField]
        private MissionData[] m_Missions;
        [SerializeField]
        private Transform m_GridParent;
        private bool m_HasSPawnedMissions;


        private void Start()
        {
            if (!m_HasSPawnedMissions)
            {
                SpawnMissionSlots();
            }
        }

        public void SpawnMissionSlots()
        {
            foreach (MissionData mission in m_Missions)
            {
                MissionSlot slot = Instantiate(m_SlotPrefab, m_GridParent);
                slot.SetMission(mission);
            }

            m_HasSPawnedMissions = true;
        }

        public void OnBackBtn()
        {
            HidePanel();
        }
    }
}