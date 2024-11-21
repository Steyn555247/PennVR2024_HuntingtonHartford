using UnityEngine;

public class SuperBounce : MonoBehaviour
{
    public float bounceMultiplier = 2.0f; // Multiplier for the bounce height
    public string groundTag = "Ground"; // Tag for ground objects (optional)

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("No Rigidbody found on this GameObject.");
            return;
        }

        // Check if the object is hitting the ground
        if (collision.relativeVelocity.y < 0 &&
            (string.IsNullOrEmpty(groundTag) || collision.gameObject.CompareTag(groundTag)))
        {
            // Get the velocity and amplify the Y component for the bounce
            Vector3 velocity = rb.velocity;

            velocity.y = -collision.relativeVelocity.y * bounceMultiplier; // Amplify bounce
            rb.velocity = velocity;

            Debug.Log($"Bounce Velocity: {velocity.y}");
        }
    }
}
