using UnityEngine;
using UnityEngine.SceneManagement;

public class ToGame : MonoBehaviour
{
    public void GoToGame()
    {
        Debug.Log("Button Pressed");
        SceneTransitionManager.Instance.LoadScene("Intro");
    }
}

