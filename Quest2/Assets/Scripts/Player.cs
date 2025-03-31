/**
* Player Class
* Represents the Player (Priestess)
*/
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    // Update is called once per frame
    // void Update()
    // {
    //     // Debug.unityLogger.Log(stats.ToString());
        
    //     // Check for collision with monsters
    //     Collider[] hitArr = Physics.OverlapSphere(this.transform.position, 5f, 6);
    //     Debug.Log($"Detected {hitArr.Length} colliders in OverlapSphere.");

    //     if (hitArr.Length == 0)
    //     {
    //         return;
    //     }

    //     foreach (Collider hit in hitArr)
    //     {
    //         if (hit.gameObject.layer == LayerMask.NameToLayer("Enemy"))
    //         {
    //             Debug.unityLogger.Log("Monster hit!");

    //             SceneChangeTrigger scriptComponent = hit.gameObject.GetComponent<SceneChangeTrigger>();
    //             scriptComponent.battleScene = "devBattle";
    //             scriptComponent.specifiedMonster = hit.gameObject;

    //             // GC_Overworld gcOverworld = Object.FindAnyObjectByType<GC_Overworld>();

    //             // if (gcOverworld == null)
    //             // {
    //             //     Debug.unityLogger.Log("GC_Overworld not found!");
    //             //     return;
    //             // }
    //             // else {
    //             //     Debug.unityLogger.Log("GC_Overworld found!");
    //             //     gcOverworld.TransitionToBattle();
    //             //     Destroy(hit.gameObject);
    //             // } 
    //         }
    //     }
    // }

    private GameObject specifiedMonster;
    private string battleScene;

    private void OnTriggerEnter(Collider other) {
        Debug.Log("OnTriggerEnter called with: " + other.gameObject.name);

        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
            Debug.Log("Enemy entered trigger!");
            battleScene = "devBattle";
            specifiedMonster = other.gameObject;
            DontDestroyOnLoad(specifiedMonster);
            Debug.Log($"Collided with monster: {specifiedMonster.name}");

            // Transfer transferObj = GameObject.FindFirstObjectByType<Transfer>();
            // if (transferObj != null)
            // {
            //     //Pass transferObj player reference to the Battle Controller
            //     transferObj.setPlayerObj(GameObject.FindWithTag("Player"));
            //     transferObj.setEnemyObj(specifiedMonster);
            //     Debug.Log($"Transfer monster: {specifiedMonster.name}");
            // }
            // else
            // {
            //     Debug.LogWarning("Warning: Could not find Battle Controller in Battle Scene!");
            // }

            SceneManager.sceneLoaded += OnBattleSceneLoaded;
            //Load the Battle Scene
            Debug.Log("Transitioning to Battle Scene!");
            // // Got simple scene change working, The above is for more complex scene change
            SceneManager.LoadScene("devBattle", LoadSceneMode.Single);
        }
    }

    private void OnBattleSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Battle Scene Loaded!");

        if (scene.name == battleScene)
        {

            //Unregister the callback
            SceneManager.sceneLoaded -= OnBattleSceneLoaded;
            //Get the Battle Controller
            Transfer transferObj = GameObject.FindFirstObjectByType<Transfer>();
            if (transferObj != null)
            {
                //Pass transferObj player reference to the Battle Controller
                transferObj.setPlayerObj(GameObject.FindWithTag("Player"));
                transferObj.setEnemyObj(specifiedMonster);
                Debug.Log($"Transfer monster: {specifiedMonster.name}");
            }
            else
            {
                Debug.LogWarning("Warning: Could not find Battle Controller in Battle Scene!");
            }
        }
    }
}
