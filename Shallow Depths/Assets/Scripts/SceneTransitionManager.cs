using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance { get; private set; }
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private float fadeDuration = 1f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject); // Keeps this object across scenes
    }

    private void Start()
    {
        StartCoroutine(FadeIn()); // Fade in when scene starts
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(FadeToScene(sceneName));
    }

    private IEnumerator FadeIn()
    {
        fadeCanvasGroup.alpha = 1;
        while (fadeCanvasGroup.alpha > 0)
        {
            fadeCanvasGroup.alpha -= Time.deltaTime / fadeDuration;
            yield return null;
        }
        fadeCanvasGroup.blocksRaycasts = false;
    }

    private IEnumerator FadeToScene(string sceneName)
    {
        fadeCanvasGroup.blocksRaycasts = true;
        while (fadeCanvasGroup.alpha < 1)
        {
            fadeCanvasGroup.alpha += Time.deltaTime / fadeDuration;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
        yield return new WaitForSeconds(0.2f); // Small delay for scene loading

        StartCoroutine(FadeIn());
    }
}
