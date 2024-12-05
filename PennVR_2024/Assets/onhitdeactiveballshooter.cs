using UnityEngine;

public class DisableOnBallHit : MonoBehaviour
{
    public GameObject objectToDisable; // The object to disable when the ball hits the target

    private void OnCollisionEnter(Collision collision)
    {
        // Ensure the collider exists to avoid potential null references
        if (collision == null || collision.gameObject == null)
        {
            Debug.LogWarning("Collision or collided object is null!");
            return;
        }

        // Check if the colliding object is tagged as "ball"
        if (collision.gameObject.CompareTag("ball"))
        {
            // Disable the specified object
            if (objectToDisable != null)
            {
                objectToDisable.SetActive(false);
                Debug.Log($"{objectToDisable.name} has been disabled by {collision.gameObject.name}");
            }
            else
            {
                Debug.LogWarning("No object assigned to disable!");
            }
        }
    }
}
