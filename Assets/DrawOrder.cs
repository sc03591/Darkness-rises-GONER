using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DrawOrder : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        // Calculate the bottom Y position of the sprite
        float spriteBottomY = spriteRenderer.bounds.min.y;

        // The lower the bottom Y position, the higher the sorting order
        spriteRenderer.sortingOrder = Mathf.RoundToInt(spriteBottomY * -100);
    }
}
