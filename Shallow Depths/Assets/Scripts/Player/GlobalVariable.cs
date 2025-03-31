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
}
