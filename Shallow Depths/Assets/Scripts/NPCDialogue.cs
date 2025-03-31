using UnityEngine;
using UnityEngine.UIElements;

public class NPCDialogue : MonoBehaviour
{
    public TextDialogue[] dialogues; // Array of multiple dialogues
    [SerializeField] private GameObject _InteractUI;
    private Collider2D Collider2D;
    private GameObject _currentInteractUI;
    void Start()
    {
        Collider2D = GetComponent<Collider2D>();
    }
    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogues);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        _currentInteractUI = Instantiate(_InteractUI);
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Destroy(_currentInteractUI);
    }
}
