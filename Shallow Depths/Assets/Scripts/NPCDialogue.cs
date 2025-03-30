using UnityEngine;
using UnityEngine.InputSystem;

public class NPCDialogue : MonoBehaviour
{
    public TextDialogue dialogue;  // Assign in Inspector
    private bool isPlayerNearby = false;

    private PlayerInput playerInput;
    private InputAction interactAction;

    private void Awake()
    {
        playerInput = FindObjectOfType<PlayerInput>();  // Find PlayerInput in the scene
        interactAction = playerInput.actions["Interact"]; // Make sure "Interact" action exists
    }

    private void OnEnable()
    {
        interactAction.performed += OnInteract;
    }

    private void OnDisable()
    {
        interactAction.performed -= OnInteract;
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (isPlayerNearby)
        {
            DialogueManager.Instance.StartDialogue(new TextDialogue[] { dialogue });
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}
