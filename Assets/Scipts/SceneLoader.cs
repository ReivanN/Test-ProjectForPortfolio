using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string loadingSceneName = "LoadingScreen";
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject loading;
    [SerializeField] private Image progressBar;

    public void LoadScene(string sceneName)
    {
        loading.SetActive(true);
        menu.SetActive(false);
        StartCoroutine(LoadSceneAsync(sceneName));

    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation loadTargetScene = SceneManager.LoadSceneAsync(sceneName);
        loadTargetScene.allowSceneActivation = false;

        // Отображаем прогресс
        while (!loadTargetScene.isDone)
        {
            float progress = Mathf.Clamp01(loadTargetScene.progress / 0.9f);
            if (progressBar != null)
            {
                progressBar.fillAmount = progress;
            }

            // Проверка завершения загрузки
            if (loadTargetScene.progress >= 0.9f)
            {
                // Включаем сцену
                loadTargetScene.allowSceneActivation = true;
            }
            
            yield return null;
        }
    }
}