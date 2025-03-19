using UnityEngine;
using System.Collections.Generic; // Required for List<>

public class BossSpawner : MonoBehaviour
{
    public int gridWidth = 8;
    public int gridHeight = 5;
    public float cellSize = 1.5f; // Adjust based on room size
    [SerializeField] private GameObject bossPrefab; // Assign boss prefab in the Inspector
    [SerializeField] private Transform spawnPoint; // Assign a specific spawn point in the Inspector

    void Start(){
        SpawnBoss();
    }

    void SpawnBoss() {
        Vector3 roomPosition = transform.position; // Room center
        float totalWidth = (gridWidth - 1) * cellSize; // Total grid width
        float totalHeight = (gridHeight - 1) * cellSize; // Total grid height

        // Calculate middle-left position
        Vector3 spawnPosition = new Vector3(
            roomPosition.x + (totalWidth / 2), // Leftmost X position
            roomPosition.y,                    // Keep the same Y position
            roomPosition.z                      // Middle Z position
        );

        if (bossPrefab != null)
        {
            Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Boss prefab is not assigned in SpawnBoss!");
        }
    }
}
