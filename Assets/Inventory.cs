using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Liste over items i inventory
    public List<Item> items = new List<Item>();

    /// <summary>
    /// Tilføjer et item til inventory
    /// </summary>
    /// <param name="item">Item der skal tilføjes</param>
    public void AddItem(Item item)
    {
        if (item != null)
        {
            items.Add(item);
            Debug.Log("Added item: " + item.itemName);
            PrintInventory();
        }
        else
        {
            Debug.LogError("Tried to add a null item to inventory.");
        }
    }

    /// <summary>
    /// Fjerner et item fra inventory
    /// </summary>
    /// <param name="item">Item der skal fjernes</param>
    public void RemoveItem(Item item)
    {
        if (item != null && items.Contains(item))
        {
            items.Remove(item);
            Debug.Log("Removed item: " + item.itemName);
            PrintInventory();
        }
        else if (item == null)
        {
            Debug.LogError("Tried to remove a null item from inventory.");
        }
        else
        {
            Debug.Log("Item not found in inventory: " + item?.itemName);
        }
    }

    /// <summary>
    /// Brug et item fra inventory
    /// </summary>
    /// <param name="item">Item der skal bruges</param>
    public void UseItem(Item item)
    {
        if (item != null && items.Contains(item))
        {
            Debug.Log("Using item: " + item.itemName);
            // Implementer eventuel logik for brug af item her
        }
        else if (item == null)
        {
            Debug.LogError("Tried to use a null item.");
        }
        else
        {
            Debug.Log("Item not found in inventory: " + item?.itemName);
        }
    }

    /// <summary>
    /// Printer det aktuelle inventory til konsollen
    /// </summary>
    private void PrintInventory( )
    {
        if (items.Count > 0)
        {
            Debug.Log("Current inventory: " + string.Join(", ", items.ConvertAll(i => i.itemName)));
        }
        else
        {
            Debug.Log("Inventory is empty.");
        }
    }
}
