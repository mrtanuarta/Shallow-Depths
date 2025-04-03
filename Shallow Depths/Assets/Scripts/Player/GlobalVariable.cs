using UnityEngine;

public class GlobalVariable : MonoBehaviour
{
    public static GlobalVariable Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    [Header("PlayerStatus")]
    public bool onWater = true;
    public int karma = 0;
    public void addKarma(int karmaAmt){
        karma += karmaAmt;
        karma = Mathf.Clamp(karmaAmt,-100, 100);
    }
}
