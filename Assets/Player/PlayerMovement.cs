using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public float interactionDistance = 1f; // Distance from player to resource
    private Vector2 targetPosition;
    private bool isMoving;
    private Resource targetResource;
    public bool IsHitting = false;
    public float hittingCoolDown = 1f;
    private NavMeshAgent agent;
    public float speed = 10f;

    private Inventory inventory; // Reference to the Inventory script
    private bool isHittingResource = false; // Tracks if hitting is in progress

    private void Start( )
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = speed;

        // Get reference to the Inventory script (ensure it's assigned or in the same GameObject)
        inventory = FindObjectOfType<Inventory>();
    }

    void Update( )
    {
        // Block movement logic if the inventory is visible
        if (inventory != null && inventory.isInventoryVisible)
        {
            agent.isStopped = true; // Stops NavMeshAgent movement
            return;
        }

        agent.isStopped = false; // Allow movement when inventory is closed

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = transform.position.z; // Ensure z-position is set
            targetPosition = new Vector2(mousePosition.x, mousePosition.y);
            isMoving = true;

            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Resource"))
                {
                    targetResource = hit.collider.gameObject.GetComponent<Resource>();
                    // Target the closest point on the collider
                    Collider2D resourceCollider = targetResource.GetComponent<Collider2D>();
                    Vector2 closestPoint = resourceCollider.ClosestPoint(transform.position);
                    targetPosition = closestPoint;
                }
                else
                {
                    targetResource = null;
                }
            }
            else
            {
                targetResource = null;
            }
        }

        if (isMoving)
        {
            if (targetResource != null)
            {
                Collider2D resourceCollider = targetResource.GetComponent<Collider2D>();
                Vector2 closestPoint = resourceCollider.ClosestPoint(transform.position);
                float distanceToResource = Vector2.Distance(transform.position, closestPoint);

                if (distanceToResource <= interactionDistance)
                {
                    isMoving = false;

                    // Start the coroutine only if it's not already running
                    if (!isHittingResource)
                    {
                        StartCoroutine(HitResource());
                    }
                }
            }
            else if (Vector2.Distance(transform.position, targetPosition) <= 0.1f)
            {
                isMoving = false;
            }
        }

        SetAgentDestination();

        // Reset rotation when idle and not farming
        if (!isMoving && targetResource == null)
        {
            transform.rotation = Quaternion.identity;
        }
    }

    private void SetAgentDestination( )
    {
        if (isMoving)
        {
            // Set the destination of the NavMeshAgent
            agent.SetDestination(new Vector3(targetPosition.x, targetPosition.y, transform.position.z));
        }
    }

    private IEnumerator HitResource( )
    {
        if (isHittingResource)
            yield break; // Prevent multiple coroutines
        isHittingResource = true;

        while (targetResource != null)
        {
            IsHitting = false;
            targetResource.Hit();
            yield return SwingHead();
            yield return new WaitForSeconds(hittingCoolDown); // Adjust hit speed here
            IsHitting = true;
        }

        // Ensure the rotation resets after interaction ends
        transform.rotation = Quaternion.identity;
        isHittingResource = false;
    }

    private IEnumerator SwingHead( )
    {
        float swingDuration = 0.2f;
        float timer = 0f;
        float swingAngle = 30f; // Angle to swing the head
        Quaternion originalRotation = Quaternion.identity; // Default rotation (facing forward)
        Quaternion swingRotation = Quaternion.Euler(0, 0, swingAngle);

        // Swing head to the target angle
        while (timer < swingDuration)
        {
            transform.rotation = Quaternion.Lerp(originalRotation, swingRotation, timer / swingDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        // Reset timer for the return swing
        timer = 0f;

        // Swing head back to the original angle
        while (timer < swingDuration)
        {
            transform.rotation = Quaternion.Lerp(swingRotation, originalRotation, timer / swingDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        transform.rotation = originalRotation; // Force reset to original rotation
    }
}
