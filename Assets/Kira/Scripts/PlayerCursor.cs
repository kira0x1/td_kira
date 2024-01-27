using UnityEngine;

namespace Kira
{
    public class PlayerCursor : MonoBehaviour
    {
        [SerializeField]
        private LayerMask towerLayer;

        private LevelManager levelManager;
        private TowerPlacer towerPlacer;
        private Camera m_Cam;

        private PlayerState playerState;

        private enum PlayerState
        {
            NORMAL,
            PLACING_TOWER,
            GAME_OVER
        }


        private void Awake()
        {
            m_Cam = Camera.main;
            levelManager = FindFirstObjectByType<LevelManager>();
            towerPlacer = FindFirstObjectByType<TowerPlacer>();
        }

        private void Start()
        {
            TowerSlotUI.towerSlotClicked += OnTowerSlotClicked;
        }

        private void Update()
        {
            switch (playerState)
            {
                case PlayerState.NORMAL:
                    HandleRay();
                    break;
                case PlayerState.PLACING_TOWER:
                    HandleTowerPlacing();
                    break;
            }
        }

        private void OnTowerSlotClicked(TowerData tower)
        {
            if (levelManager.levelStats.Gems < tower.cost) return;

            if (playerState == PlayerState.NORMAL)
            {
                playerState = PlayerState.PLACING_TOWER;
                towerPlacer.EnablePlacer(tower);
            }
        }

        private void HandleTowerPlacing()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                towerPlacer.PlaceTower();
                playerState = PlayerState.NORMAL;
            }
        }

        private void HandleRay()
        {
            Ray ray = m_Cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 1000f, towerLayer))
            {
                Tower tower = hit.transform.GetComponent<Tower>();
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    HandleClickTower(tower);
                }
            }
        }

        private void HandleClickTower(Tower tower)
        {
            Debug.Log($"<color=lightblue>Clicked {tower.name}</color>");
        }
    }
}