using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // List of items in the inventory
    public List<InventorySlot> items = new List<InventorySlot>();

    public void AddItem(Item item)
    {
        if (item != null)
        {
            if (item.isStackable)
            {
                InventorySlot existingSlot = items.Find(slot => slot.item == item && slot.quantity < item.maxStackSize);
                if (existingSlot != null)
                {
                    existingSlot.quantity++;
                    Debug.Log("Added item to existing stack: " + item.itemName);
                }
                else
                {
                    items.Add(new InventorySlot(item, 1));
                    Debug.Log("Added new stack: " + item.itemName);
                }
            }
            else
            {
                items.Add(new InventorySlot(item, 1));
                Debug.Log("Added item: " + item.itemName);
            }

            PrintInventory();
        }
        else
        {
            Debug.LogError("Tried to add a null item to inventory.");
        }
    }

    public void RemoveItem(Item item)
    {
        if (item != null)
        {
            InventorySlot existingSlot = items.Find(slot => slot.item == item);
            if (existingSlot != null)
            {
                if (existingSlot.quantity > 1)
                {
                    existingSlot.quantity--;
                }
                else
                {
                    items.Remove(existingSlot);
                }

                Debug.Log("Removed item: " + item.itemName);
                PrintInventory();
            }
            else
            {
                Debug.Log("Item not found in inventory: " + item.itemName);
            }
        }
        else
        {
            Debug.LogError("Tried to remove a null item from inventory.");
        }
    }

    public void UseItem(Item item)
    {
        if (item != null)
        {
            InventorySlot existingSlot = items.Find(slot => slot.item == item);
            if (existingSlot != null)
            {
                Debug.Log("Using item: " + item.itemName);
                // Implement any logic for using the item here

                RemoveItem(item); // Remove the item from inventory after use
            }
            else
            {
                Debug.Log("Item not found in inventory: " + item.itemName);
            }
        }
        else
        {
            Debug.LogError("Tried to use a null item.");
        }
    }

    private void PrintInventory( )
    {
        if (items.Count > 0)
        {
            string inventoryContents = "Current inventory: ";
            foreach (var slot in items)
            {
                inventoryContents += $"{slot.item.itemName} (x{slot.quantity}), ";
            }
            Debug.Log(inventoryContents);
        }
        else
        {
            Debug.Log("Inventory is empty.");
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public Item item;
    public int quantity;

    public InventorySlot(Item item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }
}
