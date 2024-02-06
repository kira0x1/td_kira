using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Kira
{
    public class LoadingManager : MonoBehaviour
    {
        private static LoadingManager m_Instance;
        public static LoadingManager Instance => m_Instance;

        private bool m_IsLoading;

        [SerializeField]
        private CanvasGroup m_CanvasGroup;

        [SerializeField]
        private TextMeshProUGUI m_LoadingText;

        [SerializeField]
        private Image m_LoadingFill;

        private void Awake()
        {
            if (m_Instance != null && m_Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            m_Instance = this;
        }

        public void LoadScene(int sceneIndex)
        {
            if (m_IsLoading) return;
            StartCoroutine(StartLoadScene(sceneIndex));
        }

        private IEnumerator StartLoadScene(int sceneIndex)
        {
            m_IsLoading = true;

            EnableLoadUI();

            var ao = SceneManager.LoadSceneAsync(sceneIndex);
            ao.allowSceneActivation = false;

            while (m_IsLoading)
            {
                m_LoadingFill.fillAmount = ao.progress;
                m_LoadingText.text = $"{ao.progress * 100}%";

                if (ao.progress >= 0.9f)
                {
                    Debug.Log($"Loading: {ao.progress:F2}");
                    m_IsLoading = false;
                    ao.allowSceneActivation = true;
                }

                yield return null;
            }

            m_IsLoading = false;
            DisableLoadUI();
        }

        private void EnableLoadUI()
        {
            m_LoadingText.text = "0%";
            m_LoadingFill.fillAmount = 0f;
            m_CanvasGroup.alpha = 1f;
            m_CanvasGroup.interactable = true;
            m_CanvasGroup.blocksRaycasts = true;
        }

        private void DisableLoadUI()
        {
            m_LoadingText.text = "0%";
            m_LoadingFill.fillAmount = 0;
            m_CanvasGroup.alpha = 0f;
            m_CanvasGroup.interactable = false;
            m_CanvasGroup.blocksRaycasts = false;
        }
    }
}