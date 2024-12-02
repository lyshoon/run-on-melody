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
        OnStarCollected.Invoke(this);
    }
}
