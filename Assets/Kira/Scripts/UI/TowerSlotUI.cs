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