using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene transitions

public class EndingDialogueManager : DialogueManager
{
    public static EndingDialogueManager Instance; // Singleton instance

    [SerializeField] private string sceneToLoadAfterEnding = "Main Menu"; // Set this in Inspector

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void StartEndingDialogue(TextDialogue[] dialogues)
    {
        Debug.Log("Starting Ending Dialogue...");
        StartDialogue(dialogues); // Calls the base class method to start dialogue
    }

    public override void EndDialogue()
    {
        base.EndDialogue(); // Calls the normal EndDialogue from DialogueManager

        Debug.Log("Ending sequence finished! Loading next scene...");
        LoadEndingScene();
    }

    private void LoadEndingScene()
    {
        SceneManager.LoadScene(sceneToLoadAfterEnding); // Loads the main menu or credits scene
    }
}
