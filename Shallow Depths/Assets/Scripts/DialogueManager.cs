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
    [SerializeField] private AudioClip clickSound;

    private Queue<TextDialogue> _dialogues = new Queue<TextDialogue>();
    private bool dialogueIsActive = false;
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
            foreach (TextDialogue dialogue in dialogues)
            {
                _dialogues.Enqueue(dialogue);
            }

            dialogueIsActive = true;
            dialogueUI.SetActive(true);
            PlayerMovement.Instance.EnableMovement(false);
            DisplayNextDialogue();
        }
    }

    public void DisplayNextDialogue()
    {
        AudioManager.Instance.PlaySFX(clickSound);
        
        if (_dialogues.Count == 0)
        {
            EndDialogue();
            return;
        }

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

    public void HandleDecision(bool isYes)
    {
        if (isYes)
        {
            PlayerStats.Instance.ModifySanity(currentDialogue.YesSanityChange);
            PlayerStats.Instance.InteractAddKarma(currentDialogue.YesKarmaChange);
            PlayerStats.Instance.AddItem(currentDialogue.YesItemToGive, currentDialogue.YesItemAmount);
            PlayerStats.Instance.RemoveItem(currentDialogue.YesItemToTake, currentDialogue.YesItemTakeAmount);
        }
        else
        {
            PlayerStats.Instance.ModifySanity(currentDialogue.NoSanityChange);
            PlayerStats.Instance.InteractAddKarma(currentDialogue.NoKarmaChange);
            PlayerStats.Instance.AddItem(currentDialogue.NoItemToGive, currentDialogue.NoItemAmount);
            PlayerStats.Instance.RemoveItem(currentDialogue.NoItemToTake, currentDialogue.NoItemTakeAmount);
        }

        DisplayNextDialogue();
    }

    public virtual void EndDialogue()
    {
        dialogueUI.SetActive(false);
        dialogueIsActive = false;
        PlayerMovement.Instance.EnableMovement(true); // Re-enable movement
    }

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

    public bool getDialogueIsActive()
    {
        return dialogueIsActive;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && dialogueIsActive == true)
        {
            OnYesPressed();
        }
        else if (Input.GetKeyDown(KeyCode.G) && dialogueIsActive == true)
        {
            OnNoPressed();
        }
    }
}