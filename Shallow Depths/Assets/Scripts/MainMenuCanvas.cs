using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI FinishedGame;
    [SerializeField] private TextMeshProUGUI EndingsUnlocked;
    private int EU = 0;
    private int FG = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        updateText(); 
        FG = GlobalVariable.Instance.FinishedGame;
        EU = GlobalVariable.Instance.endingUnlocked;
    }
    void updateText(){
        FinishedGame.text = "Times Game Finished > "+FG.ToString();
        EndingsUnlocked.text = "Endings Unlocked > "+EU.ToString()+"/5";
    }
}
