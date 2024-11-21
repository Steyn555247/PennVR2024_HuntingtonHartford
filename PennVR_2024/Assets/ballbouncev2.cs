using UnityEngine;

public class TennisBallBounce : MonoBehaviour
{
    public float energyRetention = 0.9f;  // Percentage of energy retained per bounce (0 to 1)
    public float upwardBoost = 1.2f;      // Multiplier for upward bounce
    public float bounceCooldown = 0.1f;   // Cooldown duration in seconds

    private Rigidbody rb;
    private float lastBounceTime = -1f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody is required on the tennis ball.");
            enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check for cooldown
        if (Time.time - lastBounceTime < bounceCooldown)
            return;

        // Check if the ball hits the ground
        if (collision.relativeVelocity.y < 0 && collision.gameObject.CompareTag("Ground"))
        {
            // Calculate new velocity
            float newYVelocity = Mathf.Abs(rb.velocity.y) * energyRetention * upwardBoost;

            // Apply new velocity
            rb.velocity = new Vector3(rb.velocity.x, newYVelocity, rb.velocity.z);

            // Update last bounce time
            lastBounceTime = Time.time;
        }
    }
}
