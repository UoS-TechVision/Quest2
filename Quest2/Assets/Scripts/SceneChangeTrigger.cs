/**
* Script to change to battle scene upon hitting mob object
*/
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeTrigger : MonoBehaviour

{
    public string battleScene;
    public GameObject specifiedMonster;

    void onTriggerEnter(Collider other)
    {
        if (string.IsNullOrEmpty(battleScene))
        {
            Debug.LogWarning("Warning: Invalid Battle Scene Name!");
            return;
        }

        SceneManager.sceneLoaded += OnBattleSceneLoaded;
        //Load the Battle Scene
        Debug.Log("Transitioning to Battle Scene!");
        SceneManager.LoadScene(battleScene, LoadSceneMode.Single);
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
                transferObj.setEnemyObj(BattleInfo.enemies[0].GetComponent<GameObject>());
            }
            else
            {
                Debug.LogWarning("Warning: Could not find Battle Controller in Battle Scene!");
            }
        }
    }
}
