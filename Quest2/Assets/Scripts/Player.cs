/**
* Player Class
* Represents the Player (Priestess)
*/
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    CharacterController characterController;
    [SerializeField]
    float speed = 5f;
	[SerializeField]
	Stats stats;
	
    void Start()
    {
        if (stats == null)
        {
            stats = gameObject.AddComponent<Stats>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        characterController.Move(move * (Time.deltaTime * speed));
        Debug.unityLogger.Log(stats.ToString());
    }
}
