using UnityEngine;
using System.Collections;

public class Resource : MonoBehaviour
    
{
    private NavMeshController surface;
    public string resourceType;
    public int hitsRequired = 5;

    private int currentHits;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        surface = FindObjectOfType<NavMeshController>();
        if (surface == null)
        { Debug.LogError("NavMeshController not found in the scene."); }
    }

    public void Hit()
    {
        currentHits++;
        StartCoroutine(FlashWhite());
        if (currentHits >= hitsRequired)
        {
            GatherResource();
        }
    }

    private void GatherResource()
    {
        // Add code to add the resource to the player's inventory
        Debug.Log("Gathered: " + resourceType);
        Destroy(gameObject);
        if (surface != null)
        {
            surface.BuildNavMesh();
        }
    }

    private IEnumerator FlashWhite()
    {
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = originalColor;
    }
}
