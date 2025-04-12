using System.Collections;
using UnityEngine;

public class DelayedAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public float delayInSeconds = 3f;

    void Start()
    {
        StartCoroutine(PlaySoundWithDelay());
    }

    IEnumerator PlaySoundWithDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        audioSource.Play();
    }
}