using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class SidePanelScript : MonoBehaviour
{
    public GameObject popupWindow; // The pop-up window (assign in the Inspector)
    public GameObject toggleButtonText; 
    private bool isPopupActive = false;

    void Start()
    {
        // Ensure the pop-up window is initially hidden
        popupWindow.SetActive(false);
    }

    public void ToggleMenu()
    {
        if (isPopupActive)
        {
            HidePopup(); 
        }
        else
        {
            ShowPopup(); 
        }
    }

    void ShowPopup()
    {
        toggleButtonText.GetComponent<TextMeshProUGUI>().text = "-"; 
        // Display the pop-up window
        popupWindow.SetActive(true);
        isPopupActive = true;
    }

    void HidePopup()
    {
        toggleButtonText.GetComponent<TextMeshProUGUI>().text = "+";
        // Hide the pop-up window
        popupWindow.SetActive(false);
        isPopupActive = false;
    }
}
