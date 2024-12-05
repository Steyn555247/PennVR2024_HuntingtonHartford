using UnityEngine;

public class TargetHitHandler : MonoBehaviour
{
    public GameObject nextTarget; // The next target to activate
    public AudioSource audioSource; // Audio source for playing sounds
    public AudioClip immediateHitSound; // Sound to play immediately on hit
    public AudioClip delayedHitSound; // Sound to play after a delay
    public float delayedSoundTime = 2f; // Time in seconds after which the delayed sound plays

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object is tagged as "ball"
        if (collision.gameObject.CompareTag("ball"))
        {
            // Deactivate the ball's ability to trigger other targets
            DisableBallInteraction(collision.gameObject);

            // Activate the next target if specified
            if (nextTarget != null)
            {
                nextTarget.SetActive(true);
            }

            // Play the immediate hit sound
            PlayImmediateSound();

            // Schedule the delayed hit sound
            PlayDelayedSound();

            // Handle additional actions for this target
            OnHit();
        }
    }

    private void DisableBallInteraction(GameObject ball)
    {
        // Disable the ball's collider to prevent further collisions with targets
        Collider ballCollider = ball.GetComponent<Collider>();
        if (ballCollider != null)
        {
            ballCollider.enabled = false; // Disable the collider
        }

        // Change the ball's tag to avoid further target interactions
        ball.tag = "inactiveBall";

        // Optional: Indicate that the ball has hit a target (e.g., change color)
        Renderer ballRenderer = ball.GetComponent<Renderer>();
        if (ballRenderer != null)
        {
            ballRenderer.material.color = Color.gray; // Example: Change the ball's color to gray
        }
    }

    private void PlayImmediateSound()
    {
        if (audioSource != null && immediateHitSound != null)
        {
            audioSource.PlayOneShot(immediateHitSound);
        }
        else
        {
            Debug.LogWarning("Immediate hit sound or AudioSource is not assigned!");
        }
    }

    private void PlayDelayedSound()
    {
        if (audioSource != null && delayedHitSound != null)
        {
            audioSource.PlayDelayed(delayedSoundTime);
        }
        else
        {
            Debug.LogWarning("Delayed hit sound or AudioSource is not assigned!");
        }
    }

    private void OnHit()
    {
        // Optional: Perform additional actions when the target is hit
        Debug.Log($"{gameObject.name} was hit!");

        // Deactivate the current target
        gameObject.SetActive(false);
    }
}
