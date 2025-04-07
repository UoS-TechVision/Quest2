using UnityEngine;
using System.Collections.Generic;

public class Transfer : MonoBehaviour
{

    public GameObject playerObj;
    public GameObject enemyObj;

    public Vector3 playerPosition;
    public Dictionary<string, Vector3> enemyPositions = new Dictionary<string, Vector3>();
    public HashSet<string> defeatedEnemies = new HashSet<string>();

    public void setPlayerObj(GameObject player)
    {
        playerObj = player;
    }

    public void setEnemyObj(GameObject enemy)
    {
        enemyObj = enemy;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    
}
