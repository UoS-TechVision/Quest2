/**
* Overworld Game Controller
* Controls entity interactions in the Overworld state
*/
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class BattleInfo {
    public static List<GameObject> enemies = new List<GameObject>(); //What enemies are we fighting in this battle?
    public static uint difficultyScaling; //How difficult is this battle?
};

public class GC_Overworld : MonoBehaviour
{
    [SerializeField] private string battleScene;    //Name of the Battle Scene to transition into
    private Player playerRef;   //Cache a reference to the player.

    private static uint deathCount = 0; 
    private static bool bIsGameOver = false; 

    [SerializeField] private bool bDbgShouldGameEnd = false; //Debugging; For Game Over Testing!

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Cache a reference to the player
        playerRef = GameObject.FindWithTag("Player").GetComponent<Player>(); 
        bIsGameOver = false; 

        //Monster Spawner call here? 
        // currently find all enemies in the scene but enemies not spawned yet
        GameObject[] allObjects = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == LayerMask.NameToLayer("Enemy"))
            {
                BattleInfo.enemies.Add(obj);
            }
        }

        //Restoring player and monster positions
        Transfer transfer = GameObject.FindAnyObjectByType<Transfer>();
        if(transfer != null){
            //playerRef.transform.position = transfer.playerPosition; 
            //removing defeated enemies
            foreach(GameObject enemy in BattleInfo.enemies){
                if(transfer.defeatedEnemies.Contains(enemy.name)){
                    Destroy(enemy); 
                    Debug.Log("Enemy destroyed");
                }
            }
            foreach(GameObject enemy in BattleInfo.enemies){
                enemy.transform.position = transfer.enemyPositions[enemy.name]; 
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Store monster positions
        Transfer transfer = GameObject.FindAnyObjectByType<Transfer>();
        if(transfer != null){
            transfer.playerPosition = playerRef.transform.position; 
            foreach(GameObject enemy in BattleInfo.enemies){
                transfer.enemyPositions[enemy.name] = enemy.transform.position; 
            }
        }

        //check for loss condition
        if(!CheckLoseCondition() && !bIsGameOver){
            //If the player has "lost", Trigger a Game Over
            GameOver(); 
        }
        //TODO: Hacking for testing; Remove!
        {
            if(Input.GetButtonDown("Fire1")){
                //TransitionToBattle(); 
            }
            else if(Input.GetButtonDown("Fire2")){
                GameObject obj = new GameObject(); //Intentionally instantiate empty gameobjects, to test Game Over state reset!
            }
        }
    }

    //Returns True if the player can still continue, False otherwise
    //Note: Bad function name; Consider renaming?
    public bool CheckLoseCondition(){
        return !bDbgShouldGameEnd;  

    }

    public void GameOver(){
        bIsGameOver = true; 
    
        //Increment the Death Counter! (It's a Roguelike after all!)
        deathCount++; 

        Debug.Log("Game Over! Death Count = " + deathCount);

        //TODO: Draw some Game Over UI or something!

        //For now, we'll just reload the active scene (assuming it's the Overworld!)
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name); 
    }
}
