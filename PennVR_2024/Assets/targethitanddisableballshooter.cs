using UnityEngine;
using System.Collections.Generic;

public class TargetActivator : MonoBehaviour
{
    // The next target to activate
    public GameObject nextTarget;

    // The AudioSource to play the hit sound
    public AudioSource audioSource;

    // The AudioClip for the hit sound
    public AudioClip hitSound;

    // Additional audio clips to play after a delay
    public AudioClip[] additionalHitSounds;

    // Delay between consecutive audio clips
    public float delayBetweenClips = 0.5f;

    // A set to track balls that have already interacted
    private static HashSet<GameObject> interactedBalls = new HashSet<GameObject>();

    // Group of objects to disable on impact
    public GameObject[] objectsToDisable;

    void Start()
    {
        // Ensure the next target is initially inactive
        if (nextTarget != null)
        {
            nextTarget.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the object that collided has the tag "ball"
        if (collision.gameObject.CompareTag("ball"))
        {
            // If the ball has already interacted with another target, ignore it
            if (interactedBalls.Contains(collision.gameObject))
            {
                return;
            }

            // Add the ball to the interacted set
            interactedBalls.Add(collision.gameObject);

            // Trigger the target hit behavior
            OnTargetHit();
        }
    }

    void OnTargetHit()
    {
        // Play the hit sound
        if (audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }

        // Schedule additional audio clips
        for (int i = 0; i < additionalHitSounds.Length; i++)
        {
            if (additionalHitSounds[i] != null)
            {
                float delay = (i + 1) * delayBetweenClips;
                Invoke(nameof(PlayAdditionalSound), delay);
            }
        }

        // Disable all objects in the group
        foreach (var obj in objectsToDisable)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }

        // Activate the next target
        if (nextTarget != null)
        {
            nextTarget.SetActive(true);
        }

        // Disable the current target
        gameObject.SetActive(false);

        // Disable this script to prevent multiple activations
        this.enabled = false;
    }

    void PlayAdditionalSound()
    {
        // Play additional sounds sequentially
        foreach (var clip in additionalHitSounds)
        {
            if (clip != null && audioSource != null)
            {
                audioSource.PlayOneShot(clip);
                return;
            }
        }
    }
}
