using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public int NumberOfStars { get; private set;}
    
    public UnityEvent<PlayerInventory>OnStarCollected;

    public void StarCollected()
    {
        NumberOfStars++;
        Debug.Log("Stars collected: " + NumberOfStars);
        
        if(OnStarCollected != null)
        {
            Debug.Log("Invoking OnStarCollected event.");
            OnStarCollected.Invoke(this);
        }
        else{
            Debug.LogWarning("OnStarCollected event has no listener");
        }
    }
}
