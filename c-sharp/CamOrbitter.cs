using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CamOrbitter : MonoBehaviour
{
    [Header("Make the target a planet to orbit around a specefied planet")]
    public Transform target; // The object to orbit around
    public float sensitivity = 3.0f; // How sensitive the mouse drag is
    public float distance = 10.0f; // The distance from the camera to the target

    private float currentX = 0.0f;
    private float currentY = 0.0f;
    public float minYAngle = 10.0f; // Minimum vertical angle
    public float maxYAngle = 80.0f; // Maximum vertical angle
    public float maxZoomDist;
    public float scrollSens; 

    [Header("UI Stuff I think")]
    public Transform cursor; 

    void Update()
    {
        Cursor.visible = false; 

        CursorToMouse(); 

        // If right-click is held down
        if (Input.GetMouseButton(1))
        {
            cursor.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;

            currentX += Input.GetAxis("Mouse X") * sensitivity;
            currentY -= Input.GetAxis("Mouse Y") * sensitivity;

            // Clamp the vertical rotation to prevent flipping
            currentY = Mathf.Clamp(currentY, minYAngle, maxYAngle);
        }
        else
        {
            cursor.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }

        if(!IsPointerOverUIObject())
        {
            // Call the zoom function using the mouse scroll wheel (Mouse ScrollWheel returns a small value)
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            Zoom(scrollInput * scrollSens, 5f, maxZoomDist); // Adjust zoom speed and min/max distance as needed
        }      
    }

    void LateUpdate()
    {
        // Calculate the new rotation
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);

        // Set the camera's position based on the rotation and distance
        Vector3 direction = new Vector3(0, 0, -distance);
        transform.position = target.position + rotation * direction;

        // Always look at the target
        transform.LookAt(target);
    }

    public void Zoom(float zoomAmount, float minDistance, float maxDistance)
    {
        // Adjust the distance based on the zoomAmount (e.g. from scroll input)
        distance -= zoomAmount;

        // Clamp the distance to keep it within the min and max values
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
    }

    private void CursorToMouse()
    {
        cursor.position = Input.mousePosition;
    }

    private bool IsPointerOverUIObject()
    {
        // Create a pointer event
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        // Raycast and check if the pointer is over any UI object
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        return results.Count > 0;
    }
}
