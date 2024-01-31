using UnityEngine;

namespace Kira
{
    public class SpeedManager : MonoBehaviour
    {
        public enum SpeedState
        {
            SLOW = -1,
            NORMAL = 0,
            FAST = 1,
            FASTEST = 2
        }

        public SpeedState m_SpeedState = SpeedState.NORMAL;

        [SerializeField] private float m_SlowSpeed = 0.5f;
        [SerializeField] private float m_NormalSpeed = 1.0f;
        [SerializeField] private float m_FastSpeed = 2.0f;
        [SerializeField] private float m_FastestSpeed = 3.0f;

        private bool m_IsPaused = false;

        public bool IsPaused => m_IsPaused;

        public void TogglePause()
        {
            m_IsPaused = !m_IsPaused;
            if (m_IsPaused) Time.timeScale = 0f;
            else Time.timeScale = GetSpeed();
        }

        public void IncreaseSpeed()
        {
            m_SpeedState++;
            if (m_SpeedState > SpeedState.FASTEST) m_SpeedState = SpeedState.SLOW;
            Time.timeScale = GetSpeed();
        }

        public void DecreaseSpeed()
        {
            if (m_SpeedState == SpeedState.SLOW)
            {
                m_SpeedState = SpeedState.FASTEST;
            }
            else
            {
                m_SpeedState--;
            }

            Time.timeScale = GetSpeed();
        }

        public float GetSpeed()
        {
            return m_SpeedState switch
            {
                SpeedState.SLOW => m_SlowSpeed,
                SpeedState.NORMAL => m_NormalSpeed,
                SpeedState.FAST => m_FastSpeed,
                SpeedState.FASTEST => m_FastestSpeed,
                _ => m_NormalSpeed
            };
        }
    }
}