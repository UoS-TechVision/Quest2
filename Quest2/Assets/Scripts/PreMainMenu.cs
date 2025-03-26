using UnityEngine;
using UnityEngine.SceneManagement;


public class PreMainMenu : MonoBehaviour
{
    // OnEnable is called 
    void OnEnable() {
        // Only specifying the scenName or sceneBuildIndex will load the Scene with the Single mode
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }
}
