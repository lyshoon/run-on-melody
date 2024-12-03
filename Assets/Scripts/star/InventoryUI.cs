using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    private TextMeshProUGUI StarText; 
    public PlayerInventory playerInventory; 

    void Start()
    {
        StarText = GetComponent<TextMeshProUGUI>();

        if (StarText == null)
        {
            Debug.LogError("TextMeshProUGUI component is missing! Please attach it to this GameObject.");
        }
        
        if (playerInventory != null)
        {
            playerInventory.OnStarCollected.AddListener(UpdateStarText);
            Debug.Log("Listener added successfully");
        }
        else{
            Debug.LogWarning("playerInventroy is null");
        }
    }

    public void UpdateStarText(PlayerInventory playerInventory)
    {
        if (StarText != null)
        {
            StarText.text = playerInventory.NumberOfStars.ToString();
        }
        else
        {
            Debug.LogError("StarText is null. Ensure the TextMeshProUGUI component is properly assigned.");
        }
    }
}
