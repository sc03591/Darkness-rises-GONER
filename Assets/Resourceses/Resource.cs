using UnityEngine;
using System.Collections.Generic;

public class Resource : MonoBehaviour
{
    private NavMeshController surface;
    public string resourceType;
    public int hitsRequired = 5;
    public float dropChanceMultiplier = 1.0f; // Multiplikator for drop sandsynligheder
    public List<Item> Drops = new List<Item>();

    private int currentHits;
    private GameObject targetObject;

    void Start( )
    {
        targetObject = GameObject.Find("Inventory");
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

        // Find hvilke items der skal droppes baseret på ressource multiplier
        List<Item> droppedItems = GetRandomDrops();

        if (droppedItems.Count > 0)
        {
            Debug.Log("Dropped items:");
            foreach (var item in droppedItems)
            {
                Debug.Log(item.itemName);

                if (targetObject != null)
                {
                    Inventory inventory = targetObject.GetComponent<Inventory>();
                    if (inventory != null)
                    {
                        inventory.AddItem(item);
                    }
                    else
                    {
                        Debug.LogError("Inventory script not found on the target object.");
                    }
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
            // Beregn sandsynligheden for dette item baseret på ressource multiplier
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
