using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Common
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private GameObject loadingPanel;
        [SerializeField] private Slider slider;

        public void LoadLevel(int sceneIndex)
        {
            StartCoroutine(LoadAsunchronously(sceneIndex)); 
        }

        private IEnumerator LoadAsunchronously(int sceneIndex)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);
            loadingPanel.SetActive(true);
            while (asyncOperation.isDone == false)
            {
                float progress = asyncOperation.progress;
                slider.value = progress;
                yield return null;
            }
        }
    }
}
