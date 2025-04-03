using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem; // Required for new Input System

public class EndingManager : MonoBehaviour
{
    public static EndingManager Instance;

    [SerializeField] private CanvasGroup fadeScreen;
    [SerializeField] private GameObject grandmaSprite;
    [SerializeField] private DialogueManager EndingDialogueManager;
    [SerializeField] private GameObject _InteractUI;

    [Header("Ending Sequences")]
    [SerializeField] private EndingDialogueSequence ending1Sequence;  // High karma
    [SerializeField] private EndingDialogueSequence ending2Sequence;  // Low karma
    [SerializeField] private EndingDialogueSequence ending3Sequence;  // Brought meds
    [SerializeField] private EndingDialogueSequence ending4Sequence;  // Meds given to kid
    [SerializeField] private EndingDialogueSequence ending5Sequence;  // Flower ending

    private bool EndingTriggered = false;

    private GameObject _currentInteractUI;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        else
            Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_currentInteractUI == null)
        {
            _currentInteractUI = Instantiate(_InteractUI, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (_currentInteractUI != null)
        {
            Destroy(_currentInteractUI);
        }
    }
    public void OnInteract()
    {
        EndingTriggered = true;
        Debug.Log("Player interacted to trigger the ending!");
        StartCoroutine(FadeToBlack());
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
            if (PlayerStats.Instance.HasItem("Flower"))
            {
                EndingDialogueManager.StartDialogue(ending5Sequence.dialogues);
                GlobalVariable.Instance.UnlockEnding(5);
            }
            else if (PlayerStats.Instance.HasItem("Kid's Pendant"))
            {
                EndingDialogueManager.StartDialogue(ending4Sequence.dialogues);
                GlobalVariable.Instance.UnlockEnding(4);
            }
            else if (PlayerStats.Instance.karma <= -40 || !PlayerStats.Instance.HasItem("Grandma's Meds"))
            {
                EndingDialogueManager.StartDialogue(ending2Sequence.dialogues);
                GlobalVariable.Instance.UnlockEnding(2);
            }
            else if (PlayerStats.Instance.HasItem("Grandma's Meds"))
            {
                if (PlayerStats.Instance.karma >= 40)
                {
                    EndingDialogueManager.StartDialogue(ending1Sequence.dialogues);
                    GlobalVariable.Instance.UnlockEnding(1);
                }
                else
                {
                    EndingDialogueManager.StartDialogue(ending3Sequence.dialogues);
                    GlobalVariable.Instance.UnlockEnding(3);
                }
            }
        }
    }

    public bool getEndingTriggered()
    {
        return EndingTriggered;
    }
}
