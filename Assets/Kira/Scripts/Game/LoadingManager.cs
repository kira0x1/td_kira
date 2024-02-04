using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kira
{
    public class LoadingManager : MonoBehaviour
    {
        private static LoadingManager m_Instance;
        public static LoadingManager Instance => m_Instance;

        private bool m_IsLoading;

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
            var ao = SceneManager.LoadSceneAsync(sceneIndex);
            ao.allowSceneActivation = false;

            while (m_IsLoading)
            {
                if (ao.progress >= 0.9f)
                {
                    Debug.Log($"Loading: {ao.progress:F2}");
                    m_IsLoading = false;
                    ao.allowSceneActivation = true;
                }

                yield return null;
            }

            m_IsLoading = false;
        }
    }
}