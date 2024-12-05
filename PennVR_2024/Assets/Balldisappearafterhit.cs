using UnityEngine;

public class DisappearAfterTime : MonoBehaviour
{
    public float destroyDelay = 5f; // Time in seconds before the object disappears
    private bool isSpawned = false; // Tracks whether this instance is a spawned ball

    void OnEnable()
    {
        // This will only run when the ball is activated (spawned by the shooter)
        isSpawned = true;

        // Schedule this specific instance to be destroyed
        Invoke(nameof(DestroySelf), destroyDelay);
    }

    void DestroySelf()
    {
        // Destroy only the instance that was spawned and marked as "isSpawned"
        if (isSpawned)
        {
            Destroy(gameObject);
        }
    }

    void OnDisable()
    {
        // Cancel destruction if the object is deactivated before it gets destroyed
        CancelInvoke(nameof(DestroySelf));
    }
}
