using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RaycastCursor : MonoBehaviour
{
    public LayerMask layerMask;
    public TextMeshProUGUI textMeshPro;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        // Create a ray from the camera through the cursor position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        // Variable to store the hit information
        RaycastHit hit;

        // Perform the raycast
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            // Log the name of the hit object
            // Debug.Log("Hit object: " + hit.collider.gameObject.name);
            // You can return the hit object or perform other actions here
            GameObject hitObject = hit.collider.gameObject;
            // Do something with hitObject

            textMeshPro.text = hitObject.name;
        } else {
            textMeshPro.text = "";
        }
    }
}
