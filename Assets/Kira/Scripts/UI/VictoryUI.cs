namespace Kira
{
    public class VictoryUI : PanelUI
    {
        private LevelManager m_LevelManager;

        private void Start()
        {
            m_LevelManager = FindFirstObjectByType<LevelManager>();
            m_LevelManager.OnVictoryEvent += ShowPanel;
        }

        public void BackToMenuClicked()
        {
            LoadingManager.Instance.LoadScene(0);
        }
    }
}