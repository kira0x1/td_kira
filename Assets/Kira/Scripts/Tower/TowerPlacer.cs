using UnityEngine;

namespace Kira
{
    public class TowerPlacer : MonoBehaviour
    {
        [SerializeField] private GameObject m_PlacerGraphic;
        [SerializeField] private Texture2D m_DefaultPlaceTexture;
        [SerializeField] private Texture2D m_CantPlaceTexture;
        [SerializeField] private int m_GroundLayer;
        [SerializeField] private LayerMask m_HitLayer;

        private Renderer m_PlacerRenderer;
        private bool m_IsPlacerEnabled;
        private Camera m_Cam;
        private Transform m_CachedTransform;
        private LevelManager m_LevelManager;
        private Vector3 m_HitPos;
        private TowerData m_TowerPlacing;

        private enum PlacerState
        {
            PLACEABLE,
            ERRORED
        }

        private PlacerState placerState;
        private static readonly int BaseMapCachedProp = Shader.PropertyToID("_BaseMap");

        private void Awake()
        {
            m_LevelManager = FindFirstObjectByType<LevelManager>();
            m_CachedTransform = transform;
            m_Cam = Camera.main;
            m_PlacerRenderer = m_PlacerGraphic.GetComponent<Renderer>();
        }

        private void Update()
        {
            if (!m_IsPlacerEnabled) return;
            HandlePlacerPos();
        }

        public void PlaceTower()
        {
            if (!m_IsPlacerEnabled) return;

            if (placerState == PlacerState.PLACEABLE)
            {
                //TODO place tower
                m_LevelManager.SpawnTower(m_TowerPlacing, m_HitPos);
            }

            DisablePlacer();
        }

        public void EnablePlacer(TowerData tower)
        {
            m_TowerPlacing = tower;
            m_IsPlacerEnabled = true;
            if (m_PlacerGraphic != null)
                m_PlacerGraphic.SetActive(true);
            else
            {
                Debug.LogWarning("warning placer graphic null");
            }

            HandlePlacerPos();
        }

        private void DisablePlacer()
        {
            m_IsPlacerEnabled = false;
            m_PlacerGraphic.SetActive(false);
        }

        private void HandlePlacerPos()
        {
            Ray ray = m_Cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 1000f, m_HitLayer))
            {
                bool hitGround = hit.transform.gameObject.layer == m_GroundLayer;
                if (hitGround)
                {
                    SwitchPlacerState(PlacerState.PLACEABLE);
                }
                else
                {
                    SwitchPlacerState(PlacerState.ERRORED);
                }

                m_HitPos = hit.point;
                m_CachedTransform.position = m_HitPos;
            }
            else
            {
                SwitchPlacerState(PlacerState.ERRORED);
            }
        }

        private void SwitchPlacerState(PlacerState nextState)
        {
            if (nextState == PlacerState.ERRORED && placerState == PlacerState.PLACEABLE)
            {
                MaterialPropertyBlock block = new MaterialPropertyBlock();
                block.SetTexture(BaseMapCachedProp, m_CantPlaceTexture);
                m_PlacerRenderer.SetPropertyBlock(block);
            }
            else if (nextState == PlacerState.PLACEABLE && placerState == PlacerState.ERRORED)
            {
                MaterialPropertyBlock block = new MaterialPropertyBlock();
                block.SetTexture(BaseMapCachedProp, m_DefaultPlaceTexture);
                m_PlacerRenderer.SetPropertyBlock(block);
            }

            placerState = nextState;
        }
    }
}