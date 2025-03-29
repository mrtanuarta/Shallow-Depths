using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "ScriptableObjects/Dialogue")]
public class TextDialogue : ScriptableObject
{
    public Sprite picture;
    public string char_name;
    public string dialogue;
    public int sanity;
}
