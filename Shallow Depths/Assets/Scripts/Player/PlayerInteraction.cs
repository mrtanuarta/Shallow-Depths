using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    private NPCDialogue _nearbyNPC;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            _nearbyNPC = other.GetComponent<NPCDialogue>();
            Debug.Log(other.name+" Entered NPC range: " + _nearbyNPC.name);
        }
        if (other.CompareTag("Ground")){
            Debug.Log("Player is standing on ground");
            GlobalVariable.Instance.onWater = false;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            _nearbyNPC = null;
        }
        if (other.CompareTag("Ground")){
            Debug.Log("Player is no longer standing on ground");
            GlobalVariable.Instance.onWater = true;
        }
    }

    public void OnInteract()
    {
        if (_nearbyNPC == null)
        {
            Debug.LogError("OnInteract: No NPC nearby!");
            return;
        }

        _nearbyNPC.TriggerDialogue();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || DialogueManager.Instance.getDialogueIsActive())
        {
            OnInteract();
        }
    }
}
