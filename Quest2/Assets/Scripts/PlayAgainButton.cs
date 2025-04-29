using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgainScreen : MonoBehaviour
{
    public void OnPlayAgainButton()
    {
        SceneManager.LoadScene("Main Menu"); // Replace with your actual scene name
    }
}