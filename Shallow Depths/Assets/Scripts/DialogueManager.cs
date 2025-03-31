using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance; // Singleton reference

    [SerializeField] private GameObject dialogueUI;
    [SerializeField] private Image characterImage;
    [SerializeField] private Text nameText;
    [SerializeField] private Text dialogueText;

    private Queue<TextDialogue> dialogueQueue = new Queue<TextDialogue>();
    private bool isDialogueActive = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void StartDialogue(TextDialogue[] dialogues)
    {
        if (dialogues == null || dialogues.Length == 0)
        {
            Debug.LogError("StartDialogue: No dialogues provided!");
            return;
        }

        dialogueQueue.Clear();
        foreach (var dialogue in dialogues)
        {
            dialogueQueue.Enqueue(dialogue);
        }

        dialogueUI.SetActive(true);
        isDialogueActive = true;
        DisplayNextDialogue();
    }

    public void DisplayNextDialogue()
    {
        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        TextDialogue currentDialogue = dialogueQueue.Dequeue();

        if (currentDialogue == null)
        {
            Debug.LogError("DisplayNextDialogue: Encountered a null dialogue!");
            return;
        }

        Debug.Log("Displaying dialogue for: " + currentDialogue.char_name);
        Debug.Log("Dialogue text: " + currentDialogue.dialogue);

        if (characterImage == null || nameText == null || dialogueText == null)
        {
            Debug.LogError("UI elements not assigned in DialogueManager!");
            return;
        }

        characterImage.sprite = currentDialogue.picture;
        nameText.text = currentDialogue.char_name;
        dialogueText.text = currentDialogue.dialogue;
    }


    private void EndDialogue()
    {
        dialogueUI.SetActive(false);
        isDialogueActive = false;
    }
}
