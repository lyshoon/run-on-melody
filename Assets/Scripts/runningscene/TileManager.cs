using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;        // Path tiles
    public GameObject[] itemPrefabs;       // Objects to spawn (e.g., coins)
    public float zSpawn = 0;               // Z-axis for spawning tiles
    public float tileLength = 10;          // Length of each tile
    public int numberOfTiles = 5;          // Number of tiles active at any time
    private List<GameObject> activeTiles = new List<GameObject>();

    public Transform playerTransform;      // Player's position
    public GameObject obstaclePrefab;      // Obstacle prefab

    void Start()
    {
        zSpawn = playerTransform.position.z; 
        for (int i = 0; i < numberOfTiles; i++)
        {
            if (i == 0)
                SpawnTile(0);               // First tile is static
            else
                SpawnTile(Random.Range(0, tilePrefabs.Length)); // Random tiles
        }
    }

    void Update()
    {
        // Spawn new tiles as the player progresses
        if (playerTransform.position.z - 35 > zSpawn - (numberOfTiles * tileLength))
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
            DeleteTile();
        }
    }

    public void SpawnTile(int tileIndex)
{
    // Spawn the tile
    GameObject go = Instantiate(tilePrefabs[tileIndex], transform.forward * zSpawn, transform.rotation);
    activeTiles.Add(go);

    // Define possible spawn points
    Transform leftSpawnPoint = go.transform.Find("SpawnPointLeft");
    Transform middleSpawnPoint = go.transform.Find("SpawnPointMiddle");
    Transform rightSpawnPoint = go.transform.Find("SpawnPointRight");

    // Randomly select a spawn point
    Transform selectedSpawnPoint = null;
    int spawnChoice = Random.Range(0, 3); // 0: Left, 1: Middle, 2: Right
    if (spawnChoice == 0 && leftSpawnPoint != null) 
        selectedSpawnPoint = leftSpawnPoint;
    else if (spawnChoice == 1 && middleSpawnPoint != null) 
        selectedSpawnPoint = middleSpawnPoint;
    else if (spawnChoice == 2 && rightSpawnPoint != null) 
        selectedSpawnPoint = rightSpawnPoint;

    // Adjust spawn frequency to make items appear more often
    if (selectedSpawnPoint != null && Random.Range(0f, 1f) < 0.8f) // 80% chance to spawn
    {
        GameObject item = Instantiate(
            itemPrefabs[Random.Range(0, itemPrefabs.Length)],
            selectedSpawnPoint.position,
            Quaternion.Euler(-90, 0, 0), // Ensure the item is upright
            go.transform // Parent the item to the tile for organization
        );

        // Ensure the item's scale is correct
        item.transform.localScale = Vector3.one;
    }

    // Spawn obstacles on the newly spawned tile
    SpawnObstacle(go);

    // Move the zSpawn position forward for the next tile
    zSpawn += tileLength;
}



    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    private void SpawnItems(GameObject tile)
    {
        // Get all child spawn points in the tile
        Transform[] spawnPoints = tile.GetComponentsInChildren<Transform>();

        foreach (Transform spawnPoint in spawnPoints)
        {
            if (spawnPoint.CompareTag("SpawnPoint"))
            {
                // Randomly decide whether to spawn an object
                if (Random.value > 0.5f) // 50% chance to spawn
                {
                    GameObject item = Instantiate(
                        itemPrefabs[Random.Range(0, itemPrefabs.Length)], 
                        spawnPoint.position, 
                        Quaternion.identity
                    );
                    item.transform.parent = tile.transform; // Parent to the tile for cleanup

                    // Reset scale for the item
                    item.transform.localScale = Vector3.one;
                }
            }
        }
    }

    void SpawnObstacle(GameObject tile)
{
    // Ensure the tile is not null
    if (tile == null)
    {
        Debug.LogError("Tile is null in SpawnObstacle");
        return;
    }

    // Define possible spawn points
    Transform spawnLeft = tile.transform.Find("ObstacleSpawnLeft");
    Transform spawnMiddle = tile.transform.Find("ObstacleSpawnMiddle");
    Transform spawnRight = tile.transform.Find("ObstacleSpawnRight");

    // Check if spawn points exist
    if (spawnLeft == null || spawnMiddle == null || spawnRight == null)
    {
        Debug.LogError("One or more spawn points are missing in the tile prefab.");
        return;
    }

    // Randomly select a spawn point
    Transform selectedSpawnPoint = null;
    int spawnChoice = Random.Range(0, 3); // 0: Left, 1: Middle, 2: Right
    if (spawnChoice == 0)
        selectedSpawnPoint = spawnLeft;
    else if (spawnChoice == 1)
        selectedSpawnPoint = spawnMiddle;
    else if (spawnChoice == 2)
        selectedSpawnPoint = spawnRight;

    // Adjust spawn frequency for obstacles
    if (selectedSpawnPoint != null && Random.Range(0f, 1f) < 0.9f) // 80% chance to spawn
    {
        if (obstaclePrefab == null)
        {
            Debug.LogError("Obstacle prefab is not assigned.");
            return;
        }

        GameObject obstacle = Instantiate(
            obstaclePrefab,
            selectedSpawnPoint.position,
            Quaternion.identity, // Use default rotation
            tile.transform // Parent to the tile for cleanup
        );

        // Ensure the obstacle's scale and rotation are correct
        obstacle.transform.localScale = new Vector3(4.5f, 1f, 1f);
        obstacle.transform.rotation = Quaternion.identity; // Adjust rotation if needed
    }
}

}
