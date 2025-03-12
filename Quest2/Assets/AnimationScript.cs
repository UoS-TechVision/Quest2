using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    private bool isRotating;
    private Quaternion origRot, targetRot;
    private float timeToRotate = 0.1f;
    void Update()
    {
        // Prevent multiple coroutines from occurring at the same time
        if(isRotating) {
            return;
        }
        // When a key is press down, the player rotates
        if (Input.GetKeyDown(KeyCode.W)) {
            StartCoroutine(RotatePlayer(new Vector3(0, 90f, 0))); // Forward (no rotation)
        }
        else if (Input.GetKeyDown(KeyCode.S)) {
            StartCoroutine(RotatePlayer(new Vector3(0, -90f, 0))); // Rotate 180° to face backward
        }
        else if (Input.GetKeyDown(KeyCode.A)) {
            StartCoroutine(RotatePlayer(new Vector3(0, 0f, 0))); // Rotate 90° to the left
        }
        else if (Input.GetKeyDown(KeyCode.D)) {
            StartCoroutine(RotatePlayer(new Vector3(0, 180f, 0))); // Rotate 90° to the right
        }
    }

    private IEnumerator RotatePlayer(Vector3 rotationAngles) {
        isRotating = true;

        float elapsedTime = 0;
        origRot = transform.rotation;
        targetRot = Quaternion.Euler(rotationAngles); // Convert rotation angles to a Quaternion

        // Allows coroutine to run in the next frame
        while(elapsedTime < timeToRotate) {
            transform.rotation = Quaternion.Lerp(origRot, targetRot, elapsedTime / timeToRotate);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRot;
        isRotating = false;
    }


}
