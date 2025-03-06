using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private float gridSize = 1.6f; // The distance between grid points

    private Vector3 targetPosition; // The next position to move to
    private bool isMoving = false; // Prevent overlapping movements

    private Transform cameraTransform;
    private Vector3 cameraOffset;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        targetPosition = rb.position; // Initialize the target position to the starting position
        cameraTransform = transform.GetChild(0);
        cameraOffset = cameraTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving) return; // Ignore input if already moving

        // Handle movement directions and rotations
        if (Input.GetKeyDown(KeyCode.D))
        {
            targetPosition += new Vector3(gridSize, 0f, 0f); // Move right
            transform.rotation = Quaternion.Euler(0f, 0f, 0f); // rotate left
            isMoving = true;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            targetPosition += new Vector3(-gridSize, 0f, 0f); // Move left
            transform.rotation = Quaternion.Euler(0f, 180f, 0f); // rotate right
            isMoving = true;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            targetPosition += new Vector3(0f, 0f, gridSize); // Move forward
            transform.rotation = Quaternion.Euler(0f, -90f, 0f); // rotate up
            isMoving = true;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            targetPosition += new Vector3(0f, 0f, -gridSize); // Move backward
            transform.rotation = Quaternion.Euler(0f, 90f, 0f); // rotate down
            isMoving = true;
        }
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    void MoveCharacter()
    {
        if (!isMoving) return;

        // Smoothly move the character toward the target position
        Vector3 newPosition = Vector3.MoveTowards(rb.position, targetPosition, gridSize / 0.1f * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);

        // Check if the character has reached the target position
        if (Vector3.Distance(rb.position, targetPosition) < 0.01f)
        {
            rb.MovePosition(targetPosition); // Snap to the exact target position
            isMoving = false; // Allow new inputs
        }
    }

    private void LateUpdate()
    {
        if (cameraTransform != null)
        {
            cameraTransform.position = transform.position + cameraOffset;
            cameraTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}
