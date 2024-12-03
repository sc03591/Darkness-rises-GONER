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

    void Start()
    {
        inventory = FindObjectOfType<Inventory>(); // Assuming you have an Inventory script as described earlier
        CreateSlots();
    }

    void CreateSlots()
    {
        GridLayoutGroup gridLayoutGroup = inventoryPanel.GetComponent<GridLayoutGroup>();

        if (gridLayoutGroup == null)
        {
            Debug.LogError("Grid Layout Group component is missing on the inventory panel.");
            return;
        }

        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        gridLayoutGroup.constraintCount = 4; // Example: 4 rows
        gridLayoutGroup.cellSize = new Vector2(180, 180); // Adjust according to your slot size
        gridLayoutGroup.spacing = new Vector2(10, 10); // Adjust to spread out the slots

        for (int i = 0; i < numberOfSlots; i++)
        {
            GameObject slot = Instantiate(slotPrefab, inventoryPanel.transform);
            slots.Add(slot);
            Debug.Log("Slot created: " + slot.name); // Debug log for slot creation
        }

        UpdateUI();
    }

    public void UpdateUI()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].GetComponentInChildren<Text>().text = inventory.items[i].itemName;
                // Optionally set item icons if you have them
                slots[i].GetComponent<Image>().sprite = inventory.items[i].icon;
            }
            else
            {
                slots[i].GetComponentInChildren<Text>().text = "";
                slots[i].GetComponent<Image>().sprite = null;
            }
        }
    }
}
