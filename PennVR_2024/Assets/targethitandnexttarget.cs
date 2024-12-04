using UnityEngine;
using System.Collections.Generic;

public class TargetToggleActivator : MonoBehaviour
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

    // Group of objects to toggle (activate or deactivate)
    public GameObject[] targetsToToggle;

    // Flag to determine if the objects should be activated or deactivated
    public bool activateTargets = true;

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

        // Toggle the active state of all targets in the group
        foreach (var target in targetsToToggle)
        {
            if (target != null)
            {
                target.SetActive(activateTargets);
            }
        }

        // Activate the next target if specified
        if (nextTarget != null)
        {
            nextTarget.SetActive(true);
        }

        // Disable the current target if deactivating
        if (!activateTargets)
        {
            gameObject.SetActive(false);
        }

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
