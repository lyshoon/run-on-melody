using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class star : MonoBehaviour
{
  private void OnTriggerEnter(Collider other)
 {
    Debug.Log("Object collided: " + other.name);
    PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();

    if (playerInventory != null)
    {
        Debug.Log("Adding star...");
        playerInventory.StarCollected();
        Destroy(gameObject);
    }
    else
    {
        Debug.LogWarning("PlayerInventory component not found on " + other.name);
    }
  }

}
