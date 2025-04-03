using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    // Sanity System
    public int sanity = 100;
    public int karma = 0;

    // Inventory System
    private Dictionary<string, int> inventory = new Dictionary<string, int>();
    
    public int getSanity(){
        return sanity;
    }

    [SerializeField] private AudioClip hitSound;

    public void playHitSound()
    {
        AudioManager.Instance.PlaySFX(hitSound);
    }

    void Start()
    {
        sanity = 100;
        StartCoroutine(RunEveryTwoSeconds());
    }
    void Update()
    {
        
    }
    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    // Modify Sanity
    public void ModifySanity(int amount)
    {
        sanity += amount;
        sanity = Mathf.Clamp(sanity, 1, 100);
        Debug.Log("Sanity: " + sanity);
    }

    // Inventory Management
    public void AddItem(string itemName, int amount)
    {
        if (string.IsNullOrEmpty(itemName) || amount <= 0) return;

        if (inventory.ContainsKey(itemName))
        {
            inventory[itemName] += amount;
        }
        else
        {
            inventory[itemName] = amount;
        }

        Debug.Log($"Added {amount}x {itemName} to inventory.");
    }

    public bool HasItem(string itemName, int amount = 1)
    {
        return inventory.ContainsKey(itemName) && inventory[itemName] >= amount;
    }

    public void RemoveItem(string itemName, int amount)
    {
        if (HasItem(itemName, amount))
        {
            inventory[itemName] -= amount;
            if (inventory[itemName] <= 0)
                inventory.Remove(itemName);

            Debug.Log($"Removed {amount}x {itemName} from inventory.");
        }
    }

    public void PrintInventory()
    {
        Debug.Log("Current Inventory:");
        foreach (var item in inventory)
        {
            Debug.Log($"{item.Key}: {item.Value}");
        }
    }
    IEnumerator RunEveryTwoSeconds()
    {
        while (true) // Infinite loop
        {
            Debug.Log("Running every 1.5 seconds...");
            if (GlobalVariable.Instance.onWater){
                sanity --;
            }
            yield return new WaitForSeconds(1.5f); // Wait for 2 seconds

        }
    }
    public void addKarma(int karmaAmt){
        karma += karmaAmt;
        karma = Mathf.Clamp(karmaAmt,-100, 100);
    }
}
