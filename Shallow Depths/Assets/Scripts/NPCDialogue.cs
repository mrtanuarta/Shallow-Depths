using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public TextDialogue dialogue;  // Assigned in Inspector

    public void TriggerDialogue()
    {
        if (dialogue == null)
        {
            Debug.LogError("NPCDialogue: dialogue is null on " + gameObject.name);
            return;
        }

        DialogueManager.Instance.StartDialogue(new TextDialogue[] { dialogue });
    }
}
