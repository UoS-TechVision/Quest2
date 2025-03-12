using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource; // Assign in Inspector

    private void Start()
    {
        if (audioSource.clip == null)
        {
            Debug.LogWarning("No AudioClip assigned to AudioSource! Assign one in the Inspector.");
        }
    }

    public void PlaySound()
    {
        if (audioSource != null)
        {
            audioSource.Play(); // Play the assigned AudioClip
        }
    }
}