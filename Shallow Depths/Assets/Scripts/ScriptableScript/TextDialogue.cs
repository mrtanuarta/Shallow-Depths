using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "ScriptableObjects/Dialogue")]
public class TextDialogue : ScriptableObject
{
    public string characterName;
    public Sprite characterSprite;
    public string sentence;

    [Header("Choice Options")]
    public bool hasDecision; // If true, shows YES/NO buttons

    [Header("Yes Decision")]
    public string yesSentence;

    public int YesSanityChange; // Change to sanity
    public int YesKarmaChange; // Change to Karma


    // Item rewards (Optional)
    public string YesItemToGive;
    public int YesItemAmount;

    // Item requirements (Takes items from player)
    public string YesItemToTake;
    public int YesItemTakeAmount;

    public bool YesFinalDialogue;

    [Header("No Decision")]
    public string noSentence;

    public int NoSanityChange; // Change to sanity
    public int NoKarmaChange; // Change to Karma

    // Item rewards (Optional)
    public string NoItemToGive;
    public int NoItemAmount;

    // Item requirements (Takes items from player)
    public string NoItemToTake;
    public int NoItemTakeAmount;

    public bool NoFinalDialogue;
}
