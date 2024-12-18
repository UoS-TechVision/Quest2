using UnityEngine;

public class Monster : MonoBehaviour
{
    private Transform player; // Reference to the player's transform

    [SerializeField]
    private float followDistance = 2.0f; // Distance to follow behind or in front of the player

    [SerializeField]
    private float followSpeed = 3.0f; // Speed at which the monster follows

    [SerializeField]
    private bool followInFront = true; // Whether the monster follows in front or behind the player

    void Start()
    {
        // Find the player automatically by tag (make sure your player GameObject is tagged as "Player")
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player not found! Ensure the player GameObject is tagged as 'Player'.");
        }
    }

    void Update()
    {
        if (player != null)
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        // Calculate target position
        Vector3 targetPosition = followInFront
            ? player.position + player.forward * followDistance // In front of the player
            : player.position - player.forward * followDistance; // Behind the player

        // Smoothly move toward the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);

        // Face the same direction as the player
        transform.LookAt(player.position);
    }
}
