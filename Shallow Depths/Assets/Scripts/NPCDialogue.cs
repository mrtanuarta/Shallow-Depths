using UnityEngine;
using UnityEngine.UIElements;

public class NPCDialogue : MonoBehaviour
{
    [SerializeField] private TextDialogue[] dialogues; // Array of multiple dialogues
    [SerializeField] private TextDialogue[] finalDialogues;
    [SerializeField] private GameObject _InteractUI;

    [SerializeField] private bool finalDialogue = false;

    private Collider2D Collider2D;
    private GameObject _currentInteractUI;

    void Start()
    {
        Collider2D = GetComponent<Collider2D>();
    }
    public void TriggerDialogue()
    {
        if (!finalDialogue) 
        { 
            DialogueManager.Instance.StartDialogue(dialogues, this);
        }
        else
        {
            DialogueManager.Instance.StartDialogue(finalDialogues, this);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_currentInteractUI == null){
            _currentInteractUI = Instantiate(_InteractUI, transform.position + new Vector3(0,1,0), Quaternion.identity);
        }
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (_currentInteractUI!=null){
            Destroy(_currentInteractUI);
        }
    }

    public void setFinalDialogue()
    {
        finalDialogue = true;
    }
}
