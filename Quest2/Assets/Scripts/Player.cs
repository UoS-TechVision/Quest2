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
	
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        characterController.Move(move * (Time.deltaTime * speed));
        // Debug.unityLogger.Log(stats.ToString());
        
        // Check for collision with monsters
        Collider[] hitArr = Physics.OverlapSphere(this.transform.position, 5f, 6);
        Debug.Log($"Detected {hitArr.Length} colliders in OverlapSphere.");

        if (hitArr.Length == 0)
        {
            return;
        }

        foreach (Collider hit in hitArr)
        {
            if (hit.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                Debug.unityLogger.Log("Monster hit!");

                SceneChangeTrigger scriptComponent = hit.gameObject.GetComponent<SceneChangeTrigger>();
                scriptComponent.battleScene = "devBattle";
                scriptComponent.specifiedMonster = hit.gameObject;

                // GC_Overworld gcOverworld = Object.FindAnyObjectByType<GC_Overworld>();

                // if (gcOverworld == null)
                // {
                //     Debug.unityLogger.Log("GC_Overworld not found!");
                //     return;
                // }
                // else {
                //     Debug.unityLogger.Log("GC_Overworld found!");
                //     gcOverworld.TransitionToBattle();
                //     Destroy(hit.gameObject);
                // }
                
            }
        }

    }

    private void OnTriggerEnter(Collider other)
        {
            Debug.Log("OnTrigger called!");

            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                Debug.Log("Enemy entered trigger!");
                SceneChangeTrigger scriptComponent = other.gameObject.GetComponent<SceneChangeTrigger>();
                if (scriptComponent != null)
                {
                    scriptComponent.battleScene = "devBattle";
                    scriptComponent.specifiedMonster = other.gameObject;
                }
            }
        }




}
