using UnityEngine;

public class BallDestruction : MonoBehaviour
{
    public float destructionDelay = 5f; // Time after being shot to destroy the ball
    private bool hasBeenShot = false;   // Flag to track if the ball has been shot

    void OnCollisionEnter(Collision collision)
    {
        // Start the destruction timer once the ball is shot
        if (!hasBeenShot)
        {
            hasBeenShot = true; // Ball is marked as shot
            Destroy(gameObject, destructionDelay);
        }
    }
}
