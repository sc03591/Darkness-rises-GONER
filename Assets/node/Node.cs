using UnityEngine;

public class Node : MonoBehaviour
{
    public Vector2Int gridPosition; // Position i grid'et
    public bool isWalkable = true;  // Om denne node kan bruges (f.eks. til pathfinding)
    public GameObject resource;     // Reference til ressource på noden

    public bool HasResource()
    {
        return resource != null;
    }

    public void PlaceResource(GameObject resourcePrefab)
    {
        if (!HasResource())
        {
            // Instantiér ressource på nodens position
            resource = Instantiate(resourcePrefab, transform.position, Quaternion.identity, transform);
        }
    }
}
