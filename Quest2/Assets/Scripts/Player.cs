/**
* Player Class
* Represents the Player (Priestess)
*/
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private GameObject specifiedMonster;
    private string battleScene;

    private void OnTriggerEnter(Collider other) {
        Debug.Log("OnTriggerEnter called with: " + other.gameObject.name);

        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
            Debug.Log("Enemy entered trigger!");
            
            battleScene = "devBattle";
            specifiedMonster = other.gameObject;

            SetInvisible(specifiedMonster);

            DontDestroyOnLoad(specifiedMonster);
            Debug.Log($"Collided with monster: {specifiedMonster.name}");

            //Saving overworld state to return to
            OverworldManager overworldManager = GameObject.FindFirstObjectByType<OverworldManager>();
            if (overworldManager != null) {
                overworldManager.SaveOverworldState();
                Debug.Log("Overworld state saved!");
            } else{
                Debug.LogWarning("Warning: Could not find Overworld Manager in Overworld Scene!");
            }

            //sets up callback once scene is loaded -> call OnBattleSceneLoaded
            SceneManager.sceneLoaded += OnBattleSceneLoaded;
            
            //Load the Battle Scene
            Debug.Log("Transitioning to Battle Scene!");
            SceneManager.LoadScene(battleScene, LoadSceneMode.Single);
        }
    }

    private void SetInvisible(GameObject monster) {
        // Disable the renderer on the specified monster and its children
        Renderer[] renderers = monster.GetComponentsInChildren<Renderer>(true); // Get all renderers, including inactive ones
        foreach (Renderer renderer in renderers) {
            renderer.enabled = false; // Disable each renderer
        }
    }
    private void OnBattleSceneLoaded(Scene scene, LoadSceneMode mode) {
        Debug.Log("Battle Scene Loaded!");

        if (scene.name == battleScene)
        {
            //Unregister the callback
            SceneManager.sceneLoaded -= OnBattleSceneLoaded;
            
            //Get Transfer Object to pass references to the Battle Controller
            Transfer transferObj = GameObject.FindFirstObjectByType<Transfer>();
            if (transferObj != null) {
                //getting player and enemy references
                transferObj.setPlayerObj(GameObject.FindWithTag("Player"));
                transferObj.setEnemyObj(specifiedMonster);
                Debug.Log($"Transfer monster: {specifiedMonster.name}");
            } else{
                Debug.LogWarning("Warning: Could not find Battle Controller in Battle Scene!");
            }
        }
    }
}
