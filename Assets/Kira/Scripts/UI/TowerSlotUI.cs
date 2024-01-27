using System;
using UnityEngine;

namespace Kira
{
    public class TowerSlotUI : MonoBehaviour
    {
        private TowerData towerData;
        public static Action<TowerData> towerSlotClicked;

        public void OnClick()
        {
            towerSlotClicked?.Invoke(towerData);
        }
    }
}