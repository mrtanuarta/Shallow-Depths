using System.Collections;
using UnityEngine;

public class IntroSceneManager : MonoBehaviour
{
    [SerializeField] private float introDuration = 5f; // Set duration of intro scene

    private void Start()
    {
        StartCoroutine(WaitAndLoadGameScene());
    }

    private IEnumerator WaitAndLoadGameScene()
    {
        yield return new WaitForSeconds(introDuration);
        SceneTransitionManager.Instance.LoadScene("SampleScene");
    }
}
