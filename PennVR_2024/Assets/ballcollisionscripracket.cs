using UnityEngine;

public class BallHit : MonoBehaviour
{
    public float forceMultiplier = 1.0f; // Adjusts the impact force
    public float spinMultiplier = 1.0f;  // Adjusts the spin effect

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Racket"))
        {
            Rigidbody ballRb = GetComponent<Rigidbody>();
            Rigidbody racketRb = collision.gameObject.GetComponent<Rigidbody>();

            if (ballRb != null && racketRb != null)
            {
                // Get the point of contact
                ContactPoint contact = collision.contacts[0];

                // Calculate the relative velocity between racket and ball
                Vector3 relativeVelocity = racketRb.velocity - ballRb.velocity;

                // Determine the force direction
                Vector3 forceDirection = (contact.point - racketRb.position).normalized;

                // Compute the impact force based on racket's speed
                Vector3 impactForce = forceDirection * relativeVelocity.magnitude * forceMultiplier;

                // Apply the force to the ball
                ballRb.AddForce(impactForce, ForceMode.Impulse);

                // Calculate spin based on racket's angular velocity
                Vector3 spinAxis = racketRb.angularVelocity.normalized;
                float spinSpeed = racketRb.angularVelocity.magnitude;

                // Apply torque to the ball
                ballRb.AddTorque(spinAxis * spinSpeed * spinMultiplier, ForceMode.Impulse);
            }
        }
    }
}
