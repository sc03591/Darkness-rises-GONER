using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public int numberOfSlots = 20;

    private List<GameObject> slots = new List<GameObject>();
    private Inventory inventory;
    private bool isInventoryOpen = false;

    void Start( )
    {
        inventory = FindObjectOfType<Inventory>(); // Assuming you have an Inventory script as described earlier
        CreateSlots();
        inventoryPanel.SetActive(false); // Start with the inventory panel closed
    }

    void Update( )
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    void ToggleInventory( )
    {
        isInventoryOpen = !isInventoryOpen;
        inventoryPanel.SetActive(isInventoryOpen);
        if (isInventoryOpen)
        {
            UpdateUI();
        }
    }

    void CreateSlots( )
    {
        GridLayoutGroup gridLayoutGroup = inventoryPanel.GetComponent<GridLayoutGroup>();

        if (gridLayoutGroup == null)
        {
            Debug.LogError("Grid Layout Group component is missing on the inventory panel.");
            return;
        }

        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        gridLayoutGroup.constraintCount = 4; // Example: 4 rows
        gridLayoutGroup.cellSize = new Vector2(100, 100); // Adjust according to your slot size
        gridLayoutGroup.spacing = new Vector2(10, 10); // Adjust to spread out the slots
        gridLayoutGroup.padding = new RectOffset(10, 10, 10, 10); // Top, left, bottom, right padding

        for (int i = 0; i < numberOfSlots; i++)
        {
            GameObject slot = Instantiate(slotPrefab, inventoryPanel.transform);
            slots.Add(slot);
        }
       
        UpdateUI();
    }

    public void UpdateUI()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (i < inventory.items.Count)
            {
                InventorySlot inventorySlot = inventory.items[i];
                Item item = inventorySlot.item;
                int quantity = inventorySlot.quantity;

                slots[i].transform.GetChild(0).GetComponent<Text>().text = item.itemName + " x" + quantity; // Display item name and count
                slots[i].transform.GetChild(1).GetComponent<Image>().sprite = item.icon; // Display item icon
            }
            else
            {
                slots[i].transform.GetChild(0).GetComponent<Text>().text = "";
                slots[i].transform.GetChild(1).GetComponent<Image>().sprite = null;
            }
        }
    }
}
