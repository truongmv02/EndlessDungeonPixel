using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    [SerializeField] Slider slider;
    AsyncOperation asyncOperation;

    public void LoadScene(string scene)
    {
        gameObject.SetActive(true);
        slider.value= 0f;
        StartCoroutine(LoadSceneCoroutine(scene));
    }
    IEnumerator LoadSceneCoroutine(string sceneName)
    {
        asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;
        float progress = 0f;
        while (!asyncOperation.isDone)
        {
            progress = Mathf.MoveTowards(progress, asyncOperation.progress, Time.deltaTime);
            slider.value= progress;

            if (progress >= 0.9f)
            {
                slider.value= 1;
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }

}
