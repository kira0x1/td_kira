using System;
using UnityEngine;
using UnityEngine.UI;

namespace Kira
{
    public class TowerSlotUI : MonoBehaviour
    {
        [SerializeField] private TowerData towerData;
        [SerializeField] private Image iconImage;

        public static Action<TowerData> towerSlotClicked;

        private void Start()
        {
            //TODO: set from level ui
            SetTower(towerData);
        }

        public void SetTower(TowerData towerData)
        {
            iconImage.sprite = towerData.icon;
        }

        public void OnClick()
        {
            towerSlotClicked?.Invoke(towerData);
        }
    }
}