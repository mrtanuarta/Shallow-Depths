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
        if (_currentInteractUI == null){
            _currentInteractUI = Instantiate(_InteractUI, transform.position + new Vector3(0,2,0), Quaternion.identity);
        }
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (_currentInteractUI!=null){
            Destroy(_currentInteractUI);
        }
    }
}
