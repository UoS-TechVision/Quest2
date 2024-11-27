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
    [SerializeField] public BattleInfo battleInfo; 



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: Hacking for testing; Remove!
        {
            if(Input.GetButtonDown("Fire1")){
                TransitionToBattle(); 
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

        //Load the Battle Scene
        Debug.Log("Transitioning to Battle Scene!");
        SceneManager.LoadScene(battleScene, LoadSceneMode.Single);
    }
}
