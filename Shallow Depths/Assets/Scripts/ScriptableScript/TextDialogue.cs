using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "ScriptableObjects/Dialogue")]
public class TextDialogue : ScriptableObject
{
    public Sprite picture;
    public string char_name;
    public string dialogue;

    public int sanityChange;  // Modify sanity when dialogue is read

    // Item rewards (Optional)
    public string itemToGive;
    public int itemAmount;

    // Item requirements (Takes items from player)
    public string itemToTake;
    public int itemTakeAmount;
}
