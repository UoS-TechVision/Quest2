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

    private void OnTriggerEnter(Collider other) {
        Debug.Log("OnTriggerEnter called with: " + other.gameObject.name);

        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
            Debug.Log("Enemy entered trigger!");
            SceneChangeTrigger scriptComponent = other.gameObject.GetComponent<SceneChangeTrigger>();
            if (scriptComponent != null) {
                scriptComponent.battleScene = "devBattle";
                scriptComponent.specifiedMonster = other.gameObject;
            }

            // Got simple scene change working, The above is for more complex scene change
            SceneManager.LoadScene("devBattle", LoadSceneMode.Single);
        }
    }
}
