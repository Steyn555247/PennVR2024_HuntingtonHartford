using UnityEngine;

public class TargetZone : MonoBehaviour
{
    public AudioClip hitSound; // Sound to play when the target is hit
    public GameObject nextTarget; // Reference to the next target
    private AudioSource audioSource; // AudioSource component

    void Start()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource component is missing on " + gameObject.name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the ball
        if (other.gameObject.CompareTag("Ball"))
        {
            // Play sound
            if (hitSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(hitSound);
            }

            // Activate the next target
            if (nextTarget != null)
            {
                nextTarget.SetActive(true);
            }

            // Deactivate the current target
            gameObject.SetActive(false);

            // Disable the ball's collider to prevent further triggers
            Collider ballCollider = other.GetComponent<Collider>();
            if (ballCollider != null)
            {
                ballCollider.enabled = false; // Disable the collider
                Debug.Log("Ball collider disabled to prevent further triggers.");
            }
        }
    }
}
