using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawner : MonoBehaviour
{
    public GameObject[] environmentPrefabs; 
    public int numberOfElements = 18;        
    public float spawnDistance = 50f;       
    public Transform player;                // Reference to the player for position tracking
    private List<GameObject> activeElements = new List<GameObject>();

    private float zSpawn = 0;

    void Start()
    {
        for (int i = 0; i < numberOfElements; i++)
        {
            SpawnEnvironment(Random.Range(0, environmentPrefabs.Length));
        }
    }

    void Update()
    {
        if (player.position.z - 30 > zSpawn - (numberOfElements * spawnDistance))
        {
            SpawnEnvironment(Random.Range(0, environmentPrefabs.Length));
            DeleteEnvironment();
        }
    }

    void SpawnEnvironment(int index)
{
    // Randomly position planets to the sides of the running path
    float xPosition = Random.Range(-18f, -8f); // Left side of the lane
    if (Random.value > 0.5f)                   // Randomly place on the right side
    {
        xPosition = Random.Range(8f, 18f);     // Right side of the lane
    }

    float yPosition = Random.Range(3f, 5f);    // Keep planets near the ground level
    float zPosition = zSpawn;

    Vector3 spawnPosition = new Vector3(xPosition, yPosition, zPosition);

    GameObject element = Instantiate(environmentPrefabs[index], spawnPosition, Quaternion.identity);
    activeElements.Add(element);

    zSpawn += spawnDistance; // Move the Z-axis spawn position forward
}


    void DeleteEnvironment()
    {
        Destroy(activeElements[0]);
        activeElements.RemoveAt(0);
    }
}
