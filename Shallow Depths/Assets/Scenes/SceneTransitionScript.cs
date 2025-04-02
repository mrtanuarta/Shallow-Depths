using UnityEngine;
using UnityEngine.SceneManagement;

public class ToGame : MonoBehaviour
{
    public void GoToGame()
    {
        Debug.Log("Button Pressed");
        SceneManager.LoadScene("SampleScene"); // Ensure "Main-menu" is in Build Settings
    }
}

