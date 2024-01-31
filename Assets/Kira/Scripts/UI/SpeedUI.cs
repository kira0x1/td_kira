using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Kira
{
    public class SpeedUI : MonoBehaviour
    {
        [SerializeField] private Image playIconUI;
        [SerializeField] private Sprite playSprite;
        [SerializeField] private Sprite pauseSprite;
        [SerializeField] private TextMeshProUGUI speedText;
    }
}