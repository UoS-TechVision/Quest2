using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab; // Assign in Unity Inspector
    public int gridWidth = 8;
    public int gridHeight = 5;
    public float cellSize = 1.5f; // Adjust based on room size
    public GameObject sceneChangePrefab; // Assign in Unity Inspector
    void Start()
    {
        SpawnMonsters();
    }

    void SpawnMonsters()
    {
        Vector3 roomPosition = transform.position; // Room center
        float totalWidth = (gridWidth - 1) * cellSize; // Total grid width
        float totalHeight = (gridHeight - 1) * cellSize; // Total grid height
        Vector3 bottomLeft = roomPosition - new Vector3(totalWidth / 2, 0, totalHeight / 2);
        int monstersSpawned = 0; // Track how many monsters have been spawned

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (monstersSpawned >= 3) return; // Stop spawning if limit reached

                if (Random.value > 0.9f) // Adjust spawn chance as needed
                {

                   
                    Vector3 spawnPos = bottomLeft + new Vector3(x * cellSize, 0, y * cellSize);
                    Collider[] colliders = Physics.OverlapSphere(spawnPos, 0.5f); // Check if any colliders are near the spawn position
                    if (colliders.Length > 0) continue; // Skip this position if something is already there
                    // Instantiate the monster prefab with the desired rotation
                    GameObject monster = Instantiate(monsterPrefab, spawnPos, Quaternion.identity);
                    // Make the monster face the wall
                    monster.transform.Rotate(0, -90, 0); 

                    monstersSpawned++; // Increase the counter
                }
            }
        }
    }
}
