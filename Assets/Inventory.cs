using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    public void Update() 
    {
        Debug.Log(items);
    }

    public void AddItem(Item item)
    {
        items.Add(item);
        Debug.Log("Added item: " + item.itemName);
    }

    public void RemoveItem(Item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            Debug.Log("Removed item: " + item.itemName);
        }
        else
        {
            Debug.Log("Item not found in inventory: " + item.itemName);
        }
    }

    public void UseItem(Item item)
    {
        // Implement item usage logic here
        Debug.Log("Using item: " + item.itemName);
    }
}
