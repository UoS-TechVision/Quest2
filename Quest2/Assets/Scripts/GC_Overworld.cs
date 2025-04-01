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
