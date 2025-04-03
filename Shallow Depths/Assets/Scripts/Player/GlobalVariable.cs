using System;
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
    public int endingUnlocked = 0;
    public int FinishedGame = 0;

    [Header("EndingsUnlocked")]
    [SerializeField] private bool ending1 = false;
    [SerializeField] private bool ending2 = false;
    [SerializeField] private bool ending3 = false;
    [SerializeField] private bool ending4 = false;
    [SerializeField] private bool ending5 = false;
    int intEnding1, intEnding2, intEnding3, intEnding4, intEnding5;

    private void Start()
    {
        intEnding1 = Convert.ToInt32(ending1);
        intEnding2 = Convert.ToInt32(ending2);
        intEnding3 = Convert.ToInt32(ending3);
        intEnding4 = Convert.ToInt32(ending4);
        intEnding5 = Convert.ToInt32(ending5);
    }
    void Update()
    {
        endingUnlocked = intEnding1+intEnding2+intEnding3+intEnding4+intEnding5;
    }

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
        FinishedGame++;
    }
}
