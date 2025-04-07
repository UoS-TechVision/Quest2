using System.IO;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class OverworldState
{
    public Vector3 playerPosition;
    public List<EnemyState> enemies = new List<EnemyState>();
}

[System.Serializable]
public class EnemyState
{
    public string name;
    public Vector3 position;
    public bool isDefeated;
}

public class OverworldManager : MonoBehaviour
{
    public GameObject player;
    public GameObject[] enemies;

    private string saveFilePath;

    private void Start()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "overworldState.json");
        Debug.Log($"Persistent Data Path: {Application.persistentDataPath}");

        // Delete the save file at the start of the game - to ensure monsters are not defeated
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("Save file deleted at the start of the game.");
        }
    }


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        // Prevent duplicate OverworldManager objects
        OverworldManager[] managers = GameObject.FindObjectsByType<OverworldManager>(FindObjectsSortMode.None);
        if (managers.Length > 1)
        {
            Destroy(this.gameObject);
            return;
        }
    }

    public void SaveOverworldState()
    {
        OverworldState state = new OverworldState
        {
            playerPosition = player.transform.position
        };

        //Saving list of enemies in the scene
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                EnemyState enemyState = new EnemyState
                {
                    name = enemy.name,
                    position = enemy.transform.position,
                    isDefeated = false // Default to not defeated
                };

                state.enemies.Add(enemyState);
                //Debug.Log($"Saving enemy: {enemyState.name}, Position: {enemyState.position}, Defeated: {enemyState.isDefeated}");
            }
        }

        string json = JsonUtility.ToJson(state, true);
        File.WriteAllText(saveFilePath, json);
        Debug.Log($"Overworld state saved to {saveFilePath}");
    }

    public void LoadOverworldState()
    {
        if (!File.Exists(saveFilePath))
        {
            Debug.LogWarning("No save file found. Starting with default state.");
            return;
        }

        string json = File.ReadAllText(saveFilePath);
        OverworldState state = JsonUtility.FromJson<OverworldState>(json);

        // Restore player position
        player.transform.position = state.playerPosition;
        
        // Restore enemy positions and remove defeated enemies
        // Testing note - in json can see that debugEnemy is in the list of enemies and is defeated
        // it is destroyed in scene but a new debugEnemy is created in original spot as it is set directly in the scene and not generated by the spawner?
        foreach (EnemyState enemyState in state.enemies)
        {
            Debug.Log($"Restoring enemy: {enemyState.name} at position {enemyState.position}");
            GameObject enemy = GameObject.Find(enemyState.name);
            if (enemy != null)
            {   
                if (enemyState.isDefeated)
                {
                    Debug.Log($"Enemy {enemy.name} is defeated. Destroying it.");
                    Destroy(enemy);
                }
                else
                {
                    Debug.Log($"Restoring enemy {enemy.name} position to {enemy.transform.position}");
                    enemy.transform.position = enemyState.position;
                }
            }
        }

        Debug.Log("Overworld state loaded.");
    }

    public void MarkEnemyAsDefeated(string enemyName)
    {
        if (!File.Exists(saveFilePath))
        {
            Debug.LogWarning("No save file found. Cannot mark enemy as defeated.");
            return;
        }

        string json = File.ReadAllText(saveFilePath);
        OverworldState state = JsonUtility.FromJson<OverworldState>(json);

        foreach (EnemyState enemyState in state.enemies)
        {
            if (enemyState.name == enemyName)
            {
                enemyState.isDefeated = true;
                break;
            }
        }

        json = JsonUtility.ToJson(state, true);
        File.WriteAllText(saveFilePath, json);
        Debug.Log($"Enemy {enemyName} marked as defeated.");
    }
}
