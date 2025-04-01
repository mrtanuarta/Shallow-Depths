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
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;
    [SerializeField] private TMP_Text yesButtonText;
    [SerializeField] private TMP_Text noButtonText;

    private Queue<TextDialogue> _dialogues = new Queue<TextDialogue>();
    private bool dialogueIsActive = false;
    private bool canProceed = true;
    private bool decisionMade = false; // New: Prevent multiple inputs
    private TextDialogue currentDialogue;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        yesButton.gameObject.SetActive(true); // Yes button is always visible
        noButton.gameObject.SetActive(false);

        yesButton.onClick.AddListener(() => HandleDecision(true));
        noButton.onClick.AddListener(() => HandleDecision(false));
    }

    public void StartDialogue(TextDialogue[] dialogues)
    {
        if (!dialogueIsActive)
        {
            dialogueIsActive = true;

            // Add a delay before clearing dialogues and activating the UI
            StartCoroutine(DelayedDialogueStart(dialogues));
        }
    }

    private IEnumerator DelayedDialogueStart(TextDialogue[] dialogues)
    {
        // Wait for a short delay before activating the UI
        yield return new WaitForSeconds(0.2f); // Adjust the delay as needed

        // Clear any old dialogues after the delay
        _dialogues.Clear();

        // Add each dialogue window to the queue
        foreach (TextDialogue dialogue in dialogues)
        {
            _dialogues.Enqueue(dialogue);
        }

        dialogueUI.SetActive(true); // Show UI
        PlayerMovement.Instance.EnableMovement(false); // Stop player movement

        // Start the dialogue after the delay
        DisplayNextDialogue();
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

        decisionMade = false; // Reset decision flag for the new dialogue

        currentDialogue = _dialogues.Dequeue();
        nameText.text = currentDialogue.characterName;
        dialogueText.text = currentDialogue.sentence;

        if (characterImage != null)
        {
            characterImage.sprite = currentDialogue.characterSprite;
            characterImage.gameObject.SetActive(currentDialogue.characterSprite != null);
        }

        yesButtonText.text = currentDialogue.yesSentence;
        yesButton.gameObject.SetActive(true); // Yes button is always shown

        if (currentDialogue.hasDecision)
        {
            noButtonText.text = currentDialogue.noSentence;
            noButton.gameObject.SetActive(true);
        }
        else
        {
            noButton.gameObject.SetActive(false);
        }
    }

    private IEnumerator DialogueCooldown()
    {
        canProceed = false;
        yield return new WaitForSeconds(0.2f); // Adjust as needed
        canProceed = true;
    }

    public void HandleDecision(bool isYes)
    {
        if (decisionMade) return; // Prevent duplicate inputs
        decisionMade = true; // Lock input until next dialogue

        if (isYes)
        {
            PlayerStats.Instance.ModifySanity(currentDialogue.YesSanityChange);
            PlayerStats.Instance.AddItem(currentDialogue.YesItemToGive, currentDialogue.YesItemAmount);
            PlayerStats.Instance.RemoveItem(currentDialogue.YesItemToTake, currentDialogue.YesItemTakeAmount);
        }
        else
        {
            PlayerStats.Instance.ModifySanity(currentDialogue.NoSanityChange);
            PlayerStats.Instance.AddItem(currentDialogue.NoItemToGive, currentDialogue.NoItemAmount);
            PlayerStats.Instance.RemoveItem(currentDialogue.NoItemToTake, currentDialogue.NoItemTakeAmount);
        }

        // Ensure buttons behave correctly after selection
        yesButton.interactable = false;
        noButton.interactable = false;

        StartCoroutine(ProceedToNextDialogue());
    }

    private IEnumerator ProceedToNextDialogue()
    {
        yield return new WaitForSeconds(0.2f); // Short delay before switching dialogues
        yesButton.interactable = true;
        noButton.interactable = true;
        decisionMade = false; // Reset input lock only after transition
        DisplayNextDialogue();
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

    // **NEW: Functions for Player Input Component**
    public void OnYesPressed()
    {
        if (dialogueIsActive)
        {
            HandleDecision(true); // Equivalent to pressing Yes button
        }
    }

    public void OnNoPressed()
    {
        if (dialogueIsActive && currentDialogue.hasDecision)
        {
            HandleDecision(false); // Equivalent to pressing No button
        }
    }
}
