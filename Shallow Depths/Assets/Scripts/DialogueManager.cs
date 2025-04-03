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
    private NPCDialogue currentNPC; // Track the NPC initiating dialogue

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

    public void StartDialogue(TextDialogue[] dialogues, NPCDialogue npc = null)
    {
        if (!dialogueIsActive)
        {
            currentNPC = npc; // Store the NPC reference

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
            if (currentDialogue.YesFinalDialogue)
            {
                currentNPC.setFinalDialogue();
            }
        }
        else
        {
            PlayerStats.Instance.ModifySanity(currentDialogue.NoSanityChange);
            PlayerStats.Instance.InteractAddKarma(currentDialogue.NoKarmaChange);
            PlayerStats.Instance.AddItem(currentDialogue.NoItemToGive, currentDialogue.NoItemAmount);
            PlayerStats.Instance.RemoveItem(currentDialogue.NoItemToTake, currentDialogue.NoItemTakeAmount);
            if (currentDialogue.NoFinalDialogue)
            {
                currentNPC.setFinalDialogue();
            }
        }

        DisplayNextDialogue();
    }

    public virtual void EndDialogue()
    {
        dialogueUI.SetActive(false);
        dialogueIsActive = false;
        PlayerMovement.Instance.EnableMovement(true);
    }

    public void OnYesPressed()
    {
        if (dialogueIsActive)
        {
            // Check if an item is required and if the player has enough of it
            if (currentDialogue.YesItemToTake != null && currentDialogue.YesItemTakeAmount > 0)
            {
                if (!PlayerStats.Instance.HasItem(currentDialogue.YesItemToTake, currentDialogue.YesItemTakeAmount))
                {
                    // Prevent decision if the player lacks the item
                    Debug.Log("You don't have the required item!");
                    return;
                }
            }

            HandleDecision(true); // Equivalent to pressing Yes button
        }
    }


    public void OnNoPressed()
    {
        if (dialogueIsActive)
        {
            // Check if an item is required and if the player has enough of it
            if (currentDialogue.NoItemToTake != null && currentDialogue.NoItemTakeAmount > 0)
            {
                if (!PlayerStats.Instance.HasItem(currentDialogue.NoItemToTake, currentDialogue.NoItemTakeAmount))
                {
                    // Prevent decision if the player lacks the item
                    Debug.Log("You don't have the required item!");
                    return;
                }
            }

            HandleDecision(false); // Equivalent to pressing Yes button
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
