using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float zoomSpeed = 0.5f;  // Speed of zooming
    public float minZoom = 5f;      // Minimum zoom level
    public float maxZoom = 15f;     // Maximum zoom level
    public float dragSpeed = 1f;    // Speed of dragging
    private Vector3 dragOrigin;

    void Update()
    {
        // Handle zooming
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            // Get the world position of the mouse before zooming
            Vector3 mousePositionBeforeZoom = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Adjust the orthographic size based on scroll input
            float newOrthographicSize = Camera.main.orthographicSize - scroll * zoomSpeed;
            newOrthographicSize = Mathf.Clamp(newOrthographicSize, minZoom, maxZoom);
            Camera.main.orthographicSize = newOrthographicSize;

            // Get the world position of the mouse after zooming
            Vector3 mousePositionAfterZoom = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Adjust the camera position to keep the zoom centered on the mouse
            Camera.main.transform.position += mousePositionBeforeZoom - mousePositionAfterZoom;
        }

        // Handle dragging
        if (Input.GetMouseButtonDown(1))
        {
            dragOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return;
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 difference = dragOrigin - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += difference * dragSpeed;
            dragOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
