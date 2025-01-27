using UnityEngine;
using System.Collections.Generic;

public class Resource : MonoBehaviour
{
    private NavMeshController surface;
    public string resourceType;
    public int hitsRequired = 5;
    public float dropChanceMultiplier = 1.0f; // Multiplier for drop chances
    public List<Item> Drops = new List<Item>();

    private int currentHits;
    private Inventory inventory; // Reference to Inventory script

    void Start( )
    {
        // Use GameObject.Find to locate the InventoryManager
        GameObject inventoryManager = GameObject.Find("InventoryManager"); // Make sure the name matches exactly

        if (inventoryManager != null)
        {
            inventory = inventoryManager.GetComponent<Inventory>();
            if (inventory == null)
            {
                Debug.LogError("Inventory script not found on the InventoryManager GameObject.");
            }
        }
        else
        {
            Debug.LogError("InventoryManager GameObject not found.");
        }

        surface = FindObjectOfType<NavMeshController>();
        if (surface == null)
        {
            Debug.LogError("NavMeshController not found in the scene.");
        }
    }

    public void Hit( )
    {
        currentHits++;
        if (currentHits >= hitsRequired)
        {
            GatherResource();
        }
    }

    public void GatherResource( )
    {
        Debug.Log("Gathered: " + resourceType);

        // Find which items should be dropped based on resource multiplier
        List<Item> droppedItems = GetRandomDrops();

        if (droppedItems.Count > 0)
        {
            Debug.Log("Dropped items:");
            foreach (var item in droppedItems)
            {
                Debug.Log(item.itemName);

                if (inventory != null)
                {
                    inventory.AddItem(item);
                    Debug.Log(item.itemName + " added to inventory");
                }
                else
                {
                    Debug.LogError("Inventory script not assigned.");
                }
            }
        }
        else
        {
            Debug.Log("No items dropped.");
        }

        Destroy(gameObject);

        if (surface != null)
        {
            surface.BuildNavMesh();
        }
    }

    private List<Item> GetRandomDrops( )
    {
        List<Item> result = new List<Item>();

        foreach (var drop in Drops)
        {
            // Calculate the drop chance for this item based on the resource multiplier
            float adjustedDropChance = drop.baseDropChance * dropChanceMultiplier;

            float randomValue = Random.Range(0f, 100f);
            if (randomValue <= adjustedDropChance)
            {
                result.Add(drop);
            }
        }

        return result;
    }
}
