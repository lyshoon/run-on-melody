using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public int NumberOfStars { get; private set; }
    public UnityEvent<PlayerInventory> OnStarCollected;

    public GameObject travelPrompt;
    private bool canTravel = false;

    private void Start()
    {
        if (travelPrompt != null)
        {
            travelPrompt.SetActive(false);
        }
    }

    public void StarCollected()
    {
        NumberOfStars++;
        Debug.Log("Stars collected: " + NumberOfStars);

        if (OnStarCollected != null)
        {
            Debug.Log("Invoking OnStarCollected event.");
            OnStarCollected.Invoke(this);
        }
        else
        {
            Debug.LogWarning("OnStarCollected event has no listener.");
        }

        if (NumberOfStars == 10)
        {
            canTravel = true;
            Debug.Log("10 stars collected! Showing travel prompt.");
            if (travelPrompt != null)
            {
                travelPrompt.SetActive(true);
            }
        }
    }

    public void OnYesClicked()
    {
        if (canTravel)
        {
            Debug.Log("Traveling to running scene.");
            UnityEngine.SceneManagement.SceneManager.LoadScene("RunningScene");
        }
    }

    public void OnNoClicked()
{
    Debug.Log("No button clicked.");
    if (travelPrompt != null)
    {
        Debug.Log("Hiding travel prompt.");
        travelPrompt.SetActive(false);

        // Ensure cursor visibility
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    else
    {
        Debug.LogError("travelPrompt is not assigned in the Inspector.");
    }
}

}
