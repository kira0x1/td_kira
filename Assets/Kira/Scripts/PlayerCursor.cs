using UnityEngine;

namespace Kira
{
    public class PlayerCursor : MonoBehaviour
    {
        private Camera m_Cam;
        [SerializeField] private LayerMask towerLayer;
        private TowerPlacer towerPlacer;

        private enum PlayerState
        {
            NORMAL,
            PLACING_TOWER,
            GAME_OVER
        }

        private PlayerState playerState;
        private TowerData towerPlacing;

        private void Awake()
        {
            m_Cam = Camera.main;
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
            if (playerState == PlayerState.NORMAL)
            {
                towerPlacing = tower;
                playerState = PlayerState.PLACING_TOWER;
                towerPlacer.EnablePlacer();
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