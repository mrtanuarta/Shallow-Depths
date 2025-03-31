using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public TextDialogue[] dialogues; // Array of multiple dialogues

    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogues);
    }
}
