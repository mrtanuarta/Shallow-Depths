using UnityEngine;

public class GlobalVariable : MonoBehaviour
{
    public static GlobalVariable Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [Header("PlayerStatus")]
    public bool onWater = true;

    [Header("EndingsUnlocked")]
    [SerializeField] private bool ending1 = false;
    [SerializeField] private bool ending2 = false;
    [SerializeField] private bool ending3 = false;
    [SerializeField] private bool ending4 = false;
    [SerializeField] private bool ending5 = false;

    public void UnlockEnding(int ending)
    {
        switch (ending)
        {
            case 1: ending1 = true; break;
            case 2: ending2 = true; break;
            case 3: ending3 = true; break;
            case 4: ending4 = true; break;
            case 5: ending5 = true; break;
        }
    }
}
