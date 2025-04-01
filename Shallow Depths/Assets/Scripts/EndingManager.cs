using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem; // Required for new Input System

public class EndingManager : MonoBehaviour
{
    public static EndingManager Instance;

    [SerializeField] private CanvasGroup fadeScreen;
    [SerializeField] private GameObject grandmaSprite;
    [SerializeField] private DialogueManager dialogueManager;

    [Header("Ending Sequences")]
    [SerializeField] private EndingDialogueSequence ending1Sequence;  // Brought meds
    [SerializeField] private EndingDialogueSequence ending2Sequence;  // Low sanity
    [SerializeField] private EndingDialogueSequence ending3Sequence;  // High sanity
    [SerializeField] private EndingDialogueSequence defaultEndingSequence;  // Failsafe

    private bool hasTriggered = false;
    private bool isPlayerInRange = false; // Tracks if the player is in range

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed && isPlayerInRange && !hasTriggered)
        {
            hasTriggered = true;
            Debug.Log("Player interacted to trigger the ending!");
            StartCoroutine(FadeToBlack());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    private IEnumerator FadeToBlack()
    {
        // Disable Player Movement
        PlayerMovement.Instance?.EnableMovement(false);

        float duration = 2f;
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            fadeScreen.alpha = Mathf.Lerp(0, 1, time / duration);
            yield return null;
        }

        yield return new WaitForSeconds(1f); // Small delay before revealing grandma

        ShowEnding();
    }

    private void ShowEnding()
    {
        grandmaSprite.SetActive(true);

        // Determine which ending to play
        if (PlayerStats.Instance != null)
        {
            if (PlayerStats.Instance.HasItem("Grandma's Meds"))
            {
                dialogueManager.StartDialogue(ending1Sequence.dialogues);
            }
            else if (PlayerStats.Instance.sanity < 20)
            {
                dialogueManager.StartDialogue(ending2Sequence.dialogues);
            }
            else if (PlayerStats.Instance.sanity > 80)
            {
                dialogueManager.StartDialogue(ending3Sequence.dialogues);
            }
            else
            {
                dialogueManager.StartDialogue(defaultEndingSequence.dialogues);
            }
        }
        else
        {
            Debug.LogError("PlayerStats instance not found! Using default ending.");
            dialogueManager.StartDialogue(defaultEndingSequence.dialogues);
        }
    }

}
