/**
* Overworld Game Controller
* Controls entity interactions in the Overworld state
*/
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class BattleInfo {
    public static List<GameObject> enemies; //What enemies are we fighting in this battle?
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
    }

    // Update is called once per frame
    void Update()
    {
        if(!CheckLoseCondition() && !bIsGameOver){
            //If the player has "lost", Trigger a Game Over
            GameOver(); 
        }
        //TODO: Hacking for testing; Remove!
        {
            if(Input.GetButtonDown("Fire1")){
                TransitionToBattle(); 
            }
            else if(Input.GetButtonDown("Fire2")){
                GameObject obj = new GameObject(); //Intentionally instantiate empty gameobjects, to test Game Over state reset!
            }
        }
    }

    public void TransitionToBattle(){

        //Ensure we're not trying to transition to a scene that doesn't exist!
        if(string.IsNullOrEmpty(battleScene)){
            Debug.LogWarning("Warning: Invalid Battle Scene Name!");
            return;
        }

        //Using static data members to pass data through to the Battle scene!
        //BattleInfo.enemies.Add(new GameObject());   //For now, we'll pretend that enemies are just a generic GameObject...
        BattleInfo.difficultyScaling = 100; 

        //sets up callback once scene is loaded -> call OnBattleSceneLoaded
        SceneManager.sceneLoaded += OnBattleSceneLoaded;

        //Load the Battle Scene
        Debug.Log("Transitioning to Battle Scene!");
        SceneManager.LoadScene(battleScene, LoadSceneMode.Single);

    }

    private void OnBattleSceneLoaded(Scene scene, LoadSceneMode mode){
        Debug.Log("Battle Scene Loaded!");

        if (scene.name == battleScene){

            //Unregister the callback
            SceneManager.sceneLoaded -= OnBattleSceneLoaded;            
            //Get the Battle Controller
            GC_Battle battleController = GameObject.FindFirstObjectByType<GC_Battle>();
            if (battleController != null){   
                //Pass the player reference to the Battle Controller
                battleController.SetPlayerReference(GameObject.FindWithTag("Player"));
                battleController.setMobReference(BattleInfo.enemies[0].GetComponent<GameObject>());
            }
            else {
                Debug.LogWarning("Warning: Could not find Battle Controller in Battle Scene!");
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
