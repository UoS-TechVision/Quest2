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
    private Stats stats = new Stats();

    void Start()
    {
        Debug.Log(stats.GetStats());
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        characterController.Move(move*Time.deltaTime*speed);
    }
}
