using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float wiggleAmount = 10f;
    public float wiggleSpeed = 10f;
    public float interactionDistance = 0.5f; // Distance from player to resource
    private Vector2 targetPosition;
    private bool isMoving;
    private Resource targetResource;
    public bool IsHitting = true;
    public float hittingCoolDown = 1f;
    

    private void Start()
    {
        
    }
    void Update()
    {
        if (targetResource == null)
        {
            IsHitting = true;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition = new Vector2(mousePosition.x, mousePosition.y);
            isMoving = true;
            

            RaycastHit2D hit = Physics2D.Raycast(targetPosition, Vector2.zero);
            if (hit.collider != null)
            {
                
                if (hit.collider.gameObject.CompareTag("Resource"))
                {
                    targetResource = hit.collider.gameObject.GetComponent<Resource>();
                    targetPosition = hit.collider.transform.position;
                    
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
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            WiggleEffect();
            

            if (targetResource != null)
            {
                Collider2D resourceCollider = targetResource.GetComponent<Collider2D>();
                Vector2 closestPoint = resourceCollider.ClosestPoint(transform.position);
                float distanceToResource = Vector2.Distance(transform.position, closestPoint);
                

                if (distanceToResource <= interactionDistance)
                {
                    isMoving = false;
                    transform.rotation = Quaternion.identity; // Reset rotation before hitting
                    StartCoroutine(HitResource());
                }
            }
            else if (Vector2.Distance(transform.position, targetPosition) <= 0f)
            {
                isMoving = false;
                transform.rotation = Quaternion.identity; // Reset rotation
            }
        }
    }

    private void WiggleEffect()
    {
        float angle = Mathf.Sin(Time.time * wiggleSpeed) * wiggleAmount;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private IEnumerator HitResource()
    {
        
        while (targetResource != null && IsHitting == true)
        {
            IsHitting = false;
            targetResource.Hit();
            yield return SwingHead();
            Debug.Log("Hitting resource: " + targetResource.resourceType);
            yield return new WaitForSeconds(hittingCoolDown); // Adjust hit speed here
            IsHitting = true;
        }
       
        transform.rotation = Quaternion.identity; // Reset rotation after hitting
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
