using UnityEngine;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    // Sanity System
    public int sanity = 100;

    // Inventory System
    private Dictionary<string, int> inventory = new Dictionary<string, int>();
    public int getSanity(){
        return sanity;
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
}
