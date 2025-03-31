using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "ScriptableObjects/Dialogue")]
public class TextDialogue : ScriptableObject
{
    public string characterName;  
    public Sprite characterSprite;
    public string sentence;       

    public int sanityChange;  // Modify sanity when dialogue is read

    // Item rewards (Optional)
    public string itemToGive;
    public int itemAmount;

    // Item requirements (Takes items from player)
    public string itemToTake;
    public int itemTakeAmount;
}
