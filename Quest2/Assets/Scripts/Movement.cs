using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private float gridSize = 1.6f; // Distance between grid points
    [SerializeField] private float moveSpeed = 6.0f; // Speed of movement
    [SerializeField] private float raycastDistance = 1.0f; // Distance to check for obstacles
    [SerializeField] private LayerMask obstacleLayer; // Assign in Inspector (e.g., Wall layer)

    private Vector3 targetPosition;
    private bool isMoving = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        targetPosition = rb.position;
    }

    void Update()
    {
        if (isMoving) return;

        if (Input.GetKeyDown(KeyCode.D)) {
            TryMove(Vector3.right);
        } else if (Input.GetKeyDown(KeyCode.A)) {
            TryMove(Vector3.left);
        } else if (Input.GetKeyDown(KeyCode.W)) {
            TryMove(Vector3.forward);
        } else if (Input.GetKeyDown(KeyCode.S)) {
            TryMove(Vector3.back);
        }
    }

    private void TryMove(Vector3 direction)
    {
        if (CanMove(direction))
        {
            targetPosition += direction * gridSize;
            isMoving = true;
        }
    }

    private bool CanMove(Vector3 direction)
    {
        // Cast a ray to detect obstacles
        if (Physics.Raycast(transform.position, direction, raycastDistance, obstacleLayer))
        {
            Debug.Log("Blocked by obstacle in direction: " + direction);
            return false;
        }
        return true;
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    void MoveCharacter()
    {
        if (!isMoving) return;

        Vector3 newPosition = Vector3.MoveTowards(rb.position, targetPosition, moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);

        if (Vector3.Distance(rb.position, targetPosition) < 0.01f)
        {
            rb.MovePosition(targetPosition); // Snap to exact point
            isMoving = false;
        }
    }
}
