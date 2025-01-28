using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventorySlot> items = new List<InventorySlot>();
    public GameObject itemSlotPrefab; // Reference to the slot prefab
    public Transform itemGrid;       // Reference to the UI Grid Layout
    public GameObject inventoryPanel; // Reference to the Inventory UI Panel

    private bool isInventoryVisible = false; // Tracks if inventory is visible

    void Start( )
    {
        inventoryPanel.SetActive(isInventoryVisible);
    }

    void Update( )
    {
        // Check if "I" key is pressed to toggle the inventory
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory( )
    {
        isInventoryVisible = !isInventoryVisible; // Toggle the visibility state
        inventoryPanel.SetActive(isInventoryVisible); // Show or hide the panel
    }

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
                }
                else
                {
                    items.Add(new InventorySlot(item, 1));
                }
            }
            else
            {
                items.Add(new InventorySlot(item, 1));
            }

            RefreshUI();
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

                RefreshUI();
            }
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
                RemoveItem(item); // Remove the item after use
            }
        }
    }

    public void RefreshUI( )
    {
        // Clear existing slots
        foreach (Transform child in itemGrid)
        {
            Destroy(child.gameObject);
        }

        // Populate the grid with inventory slots
        foreach (var slot in items)
        {
            GameObject slotObject = Instantiate(itemSlotPrefab, itemGrid);
            InventorySlotUI slotUI = slotObject.GetComponent<InventorySlotUI>();
            slotUI.UpdateSlot(slot.item.icon, slot.quantity);
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
