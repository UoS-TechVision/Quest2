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
            GC_Battle battleController = GameObject.FindFirstObjectByType<GC_Battle>();
            if (battleController != null)
            {
                //Pass the player reference to the Battle Controller
                battleController.SetPlayerReference(GameObject.FindWithTag("Player"));
                battleController.setMobReference(specifiedMonster);
            }
            else
            {
                Debug.LogWarning("Warning: Could not find Battle Controller in Battle Scene!");
            }
        }
    }
}
