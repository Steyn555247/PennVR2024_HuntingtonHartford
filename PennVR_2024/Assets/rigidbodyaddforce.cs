using UnityEngine;

public class Ball1 : MonoBehaviour
{
    public float yForce = 5f;  // Upward force
    public float zForce = 20f; // Forward force
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody on this GameObject

        if (rb == null)
        {
            Debug.LogError("No Rigidbody found on this GameObject. Please add one!");
            return;
        }

        // Apply the force when the game starts
        Shoot();
    }

    public void Shoot()
    {
        // Randomize the x-force between -0.5 and 0.5
        float randomX = Random.Range(-0.5f, 0.5f);
        Vector3 initialForce = new Vector3(randomX, yForce, zForce);

        Debug.Log("Random X Force: " + randomX); // Log the chosen x-force
        rb.AddForce(initialForce, ForceMode.Impulse); // Apply an impulse force
    }
}
