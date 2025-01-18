using UnityEngine;

public class Node : MonoBehaviour
{
    public Vector2Int gridPosition; // Position i grid'et
    public bool isWalkable = true;  // Om denne node kan bruges (f.eks. til pathfinding)
    public GameObject resource;     // Reference til ressource p� noden

    public bool HasResource()
    {
        return resource != null;
    }

    public void PlaceResource(GameObject resourcePrefab)
    {
        if (!HasResource())
        {
            // Instanti�r ressource p� nodens position
            resource = Instantiate(resourcePrefab, transform.position, Quaternion.identity, transform);
        }
    }
}
