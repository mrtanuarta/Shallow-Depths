using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public GameObject dialogueUI;
    public Image characterImage;
    public Text characterName;
    public Text dialogueText;
    public float textSpeed = 0.05f;

    private TextDialogue[] dialogueLines;
    private int currentLine;
    private bool isDialogueActive = false;

    private PlayerMovement playerMovement;
    private PlayerInput playerInput;
    private PlayerStats playerStats;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        dialogueUI.SetActive(false);
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerInput = FindObjectOfType<PlayerInput>();
        playerStats = FindObjectOfType<PlayerStats>();
    }

    public void StartDialogue(TextDialogue[] dialogues)
    {
        dialogueLines = dialogues;  // Store all dialogue lines
        currentLine = 0;
        isDialogueActive = true;
        dialogueUI.SetActive(true);
        playerMovement.EnableMovement(false);
        playerInput.DeactivateInput();

        ShowDialogue(dialogueLines[currentLine]);
    }


    void ShowDialogue(TextDialogue dialogue)
    {
        characterImage.sprite = dialogue.picture;
        characterName.text = dialogue.char_name;
        StartCoroutine(TypeText(dialogue.dialogue));

        // Apply sanity change
        if (dialogue.sanityChange != 0)
        {
            playerStats.ModifySanity(dialogue.sanityChange);
        }

        // Take items (if required)
        if (!string.IsNullOrEmpty(dialogue.itemToTake))
        {
            if (playerStats.HasItem(dialogue.itemToTake, dialogue.itemTakeAmount))
            {
                playerStats.RemoveItem(dialogue.itemToTake, dialogue.itemTakeAmount);
                Debug.Log($"Gave away {dialogue.itemTakeAmount}x {dialogue.itemToTake}");
            }
            else
            {
                Debug.Log($"You don't have {dialogue.itemTakeAmount}x {dialogue.itemToTake}!");
                // Maybe trigger a different dialogue line if the player lacks items
            }
        }

        // Give items
        if (!string.IsNullOrEmpty(dialogue.itemToGive))
        {
            playerStats.AddItem(dialogue.itemToGive, dialogue.itemAmount);
        }
    }

    IEnumerator TypeText(string line)
    {
        dialogueText.text = "";
        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void Update()
    {
        if (isDialogueActive && Keyboard.current.eKey.wasPressedThisFrame)
        {
            NextDialogue();
        }
    }

    void NextDialogue()
    {
        currentLine++;
        if (currentLine < dialogueLines.Length)
        {
            ShowDialogue(dialogueLines[currentLine]);
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        dialogueUI.SetActive(false);
        isDialogueActive = false;
        playerMovement.EnableMovement(true);
        playerInput.ActivateInput();
    }
}
