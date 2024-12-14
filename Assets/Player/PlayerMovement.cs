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

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = speed;
    }

    void Update()
    {
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
                    StartCoroutine(HitResource());
                }
            }
            else if (Vector2.Distance(transform.position, targetPosition) <= 0.1f)
            {
                isMoving = false;
            }
        }

        SetAgentDestination();
    }

    private void SetAgentDestination()
    {
        if (isMoving)
        {
            // Set the destination of the NavMeshAgent
            agent.SetDestination(new Vector3(targetPosition.x, targetPosition.y, transform.position.z));
        }
    }

    private IEnumerator HitResource()
    {
        while (targetResource != null)
        {
            IsHitting = false;
            targetResource.Hit();
            yield return SwingHead();
            yield return new WaitForSeconds(hittingCoolDown); // Adjust hit speed here
            IsHitting = true;
        }
    }

    private IEnumerator SwingHead()
    {
        float swingDuration = 0.2f;
        float timer = 0f;
        float swingAngle = 30f; // Angle to swing the head
        Quaternion originalRotation = transform.rotation;
        Quaternion swingRotation = Quaternion.Euler(0, 0, swingAngle);

        while (timer < swingDuration)
        {
            transform.rotation = Quaternion.Lerp(originalRotation, swingRotation, timer / swingDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        timer = 0f;
        while (timer < swingDuration)
        {
            transform.rotation = Quaternion.Lerp(swingRotation, originalRotation, timer / swingDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        transform.rotation = originalRotation; // Ensure it resets to the original rotation
    }
}
