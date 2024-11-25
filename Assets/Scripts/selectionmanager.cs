using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance { get; private set; }
    public bool onTarget;

    public GameObject interaction_Info_UI;
    public Text interaction_text;
    public Text rabbitCounterText;

    private int rabbitCount = 0;

    public float pickUpRange = 3f;

    private void Start()
    {
        onTarget = false;
        interaction_text = interaction_Info_UI.GetComponentInChildren<Text>();
        if (rabbitCounterText != null)
        {
            rabbitCounterText.text = "Rabbits: 0/5";
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Update()
    {
        GameObject nearestRabbit = FindNearestRabbit();
        if (nearestRabbit != null)
        {
            interaction_text.text = "Pick";
            interaction_Info_UI.SetActive(true);

            if (Input.GetMouseButtonDown(0))
            {
                interactableObject interactable = nearestRabbit.GetComponent<interactableObject>();
                if (interactable != null)
                {
                    CollectRabbit(interactable);
                }
            }
        }
        else
        {
            interaction_Info_UI.SetActive(false);
        }
    }

    private GameObject FindNearestRabbit()
    {
        GameObject[] rabbits = GameObject.FindGameObjectsWithTag("Rabbit");
        GameObject closestRabbit = null;
        float closestDistance = pickUpRange;

        foreach (GameObject rabbit in rabbits)
        {
            float distance = Vector3.Distance(rabbit.transform.position, transform.position);
            if (distance < closestDistance)
            {
                closestRabbit = rabbit;
                closestDistance = distance;
            }
        }
        return closestRabbit;
    }

    private void CollectRabbit(interactableObject interactable)
    {
        if (interactable.CompareTag("Rabbit"))
        {
            rabbitCount++;
            if (rabbitCounterText != null)
            {
                rabbitCounterText.text = $"Rabbits: {rabbitCount}/5";
            }

            Destroy(interactable.gameObject);

            if (rabbitCount >= 5)
            {
                GoToRunningGame();
            }
        }
    }

    private void GoToRunningGame()
    {
        SceneManager.LoadScene("RunningGameScene");
    }
}
