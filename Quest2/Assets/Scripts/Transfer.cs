using UnityEngine;

public class Transfer : MonoBehaviour
{

    public GameObject playerObj;
    public GameObject enemyObj;
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