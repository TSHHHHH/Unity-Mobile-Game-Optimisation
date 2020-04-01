using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreenCanvasController : MonoBehaviour
{
    public GameObject options;
    public GameObject loadingCanvas;
    public Slider loadingSlider;
    public TextMeshProUGUI readyText;

    private AsyncOperation operation;

    public void QuitGame()
    {
        Application.Quit();
    }

    public void startLoading()
    {
        options.SetActive(false);

        loadingCanvas.SetActive(true);

        StartCoroutine(loadScene("Optimised Scene"));
    }

    private IEnumerator loadScene(string sceneName)
    {
        yield return null;

        operation = SceneManager.LoadSceneAsync(sceneName);

        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            loadingSlider.value = operation.progress;
            if (operation.progress >= 0.9f)
            {
                loadingSlider.gameObject.SetActive(false);

                readyText.gameObject.SetActive(true);

                if (Input.GetMouseButtonDown(0))
                {
                    operation.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }
}