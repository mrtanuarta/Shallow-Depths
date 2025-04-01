using UnityEngine;

[CreateAssetMenu(fileName = "EndingDialogueSequence", menuName = "ScriptableObjects/EndingDialogueSequence")]
public class EndingDialogueSequence : ScriptableObject
{
    public TextDialogue[] dialogues;  // Multiple dialogues per ending
}
