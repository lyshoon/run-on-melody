using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class star : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory playerInventroy = other.GetComponent<PlayerInventory>();

        if(playerInventroy != null)
        {
            playerInventroy.StarCollected();
            gameObject.SetActive(false);
        }

    }
}
