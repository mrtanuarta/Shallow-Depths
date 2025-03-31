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
            Debug.Log("Entered NPC range: " + _nearbyNPC.name);
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            _nearbyNPC = null;
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (_nearbyNPC == null)
            {
                Debug.LogError("OnInteract: No NPC nearby!");
                return;
            }

            Debug.Log("Interacting with NPC: " + _nearbyNPC.name);
            _nearbyNPC.TriggerDialogue();
        }
    }
}
