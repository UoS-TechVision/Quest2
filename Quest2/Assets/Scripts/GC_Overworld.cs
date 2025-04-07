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
        // currently find all enemies in the scene but multiple enemies not spawned yet
        GameObject[] allObjects = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == LayerMask.NameToLayer("Enemy"))
            {
                BattleInfo.enemies.Add(obj);
                //Debug.Log("Found enemy: " + obj.name);
            }
        }

        // initally setting then on transition back -> restoring player and monster positions
        OverworldManager overworldManager = GameObject.FindFirstObjectByType<OverworldManager>();
        if (overworldManager != null)
        {
            overworldManager.enemies = BattleInfo.enemies.ToArray();
            // Debug.Log("Found enemies: " + overworldManager.enemies.Length);
            // foreach (GameObject enemy in overworldManager.enemies)
            // {
            //     Debug.Log("Found enemy: " + enemy.name);
            // }
            overworldManager.player = playerRef.gameObject;
            DontDestroyOnLoad(playerRef.gameObject);
            overworldManager.LoadOverworldState();
        }
        else
        {
            Debug.LogWarning("OverworldManager not found!");
        }

    }

    // Update is called once per frame
    void Update()
    {

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
