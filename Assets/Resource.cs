using UnityEngine;
using System.Collections;

public class Resource : MonoBehaviour
{
    public string resourceType;
    public int hitsRequired = 5;

    private int currentHits;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
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
    }

    private IEnumerator FlashWhite()
    {
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = originalColor;
    }
}
