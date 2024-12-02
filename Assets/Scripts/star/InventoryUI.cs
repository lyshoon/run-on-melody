using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    private TextMeshProUGUI StarText;

    // Start is called before the first frame update
    void Start()
    {
        StarText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateStarText(PlayerInventory playerInventory)
    {
        StarText.text = playerInventory.NumberOfStars.ToString();
    }
}
