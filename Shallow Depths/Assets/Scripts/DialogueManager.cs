using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance; // Singleton instance

    [SerializeField] private GameObject dialogueUI;
    [SerializeField] private Image characterImage;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text yesText;
    [SerializeField] private TMP_Text noText;

    private Queue<TextDialogue> _dialogues = new Queue<TextDialogue>();

    private bool dialogueIsActive = false;
    private bool canProceed = true;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void StartDialogue(TextDialogue[] dialogues)
    {
        if (!dialogueIsActive)
        {
            dialogueIsActive = true;
            _dialogues.Clear(); // Clear any old dialogues

            foreach (TextDialogue dialogue in dialogues)
            {
                _dialogues.Enqueue(dialogue); // Add each dialogue window
            }

            dialogueUI.SetActive(true); // Show UI
            PlayerMovement.Instance.EnableMovement(false); // Stop player movement

            DisplayNextDialogue(); // Start showing dialogues
        }
    }

    public void DisplayNextDialogue()
    {
        if (!canProceed) return; // Prevents multiple triggers in one frame

        StartCoroutine(DialogueCooldown()); // Start cooldown

        if (_dialogues.Count == 0)
        {
            EndDialogue();
            return;
        }

        TextDialogue currentDialogue = _dialogues.Dequeue();
        nameText.text = currentDialogue.characterName;
        dialogueText.text = currentDialogue.sentence;
        yesText.text = currentDialogue.yesSentence;
        if (currentDialogue.hasDecision)
        {
            noText.text = currentDialogue.noSentence;
        }
        else
        {
            noText.text = null;
        }

        if (characterImage != null)
        {
            characterImage.sprite = currentDialogue.characterSprite;
            characterImage.gameObject.SetActive(currentDialogue.characterSprite != null);
        }
    }

    private IEnumerator DialogueCooldown()
    {
        canProceed = false;
        yield return new WaitForSeconds(0.2f); // Adjust as needed
        canProceed = true;
    }

    public void EndDialogue()
    {
        dialogueUI.SetActive(false);
        dialogueIsActive = false;
        PlayerMovement.Instance.EnableMovement(true); // Re-enable movement
    }

    public void OnContinue()
    {
        DisplayNextDialogue();
    }
}
