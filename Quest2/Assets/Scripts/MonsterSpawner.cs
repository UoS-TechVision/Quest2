using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab; // Assign in Unity Inspector
    public int gridWidth = 8;
    public int gridHeight = 5;
    public float cellSize = 1.5f; // Adjust based on room size

    void Start()
    {
        SpawnMonsters();
    }

    void SpawnMonsters()
    {
        Vector3 roomPosition = transform.position; // Base position of the room

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                // Random chance to spawn a monster (adjust probability as needed)
                if (Random.value > 0.7f)
                {
                    Vector3 spawnPos = roomPosition + new Vector3(x * cellSize, 0, y * cellSize);
                    Instantiate(monsterPrefab, spawnPos, Quaternion.identity);
                }
            }
        }
    }
}
