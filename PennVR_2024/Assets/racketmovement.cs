using UnityEngine;

public class RacketController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotateSpeed = 100f;
    private Rigidbody rb;
    private Vector3 lastPosition;
    private Quaternion lastRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastPosition = transform.position;
        lastRotation = transform.rotation;
    }

    void Update()
    {
        // Movement input
        float moveHorizontal = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveVertical = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        // Apply movement
        transform.Translate(moveHorizontal, 0, moveVertical, Space.World);

        // Rotation input
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0, Space.World);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0, Space.World);
        }

        // Update Rigidbody velocities
        UpdateRigidbodyVelocities();
    }

    void UpdateRigidbodyVelocities()
    {
        // Linear velocity
        Vector3 newPosition = transform.position;
        rb.velocity = (newPosition - lastPosition) / Time.deltaTime;
        lastPosition = newPosition;

        // Angular velocity
        Quaternion newRotation = transform.rotation;
        Quaternion deltaRotation = newRotation * Quaternion.Inverse(lastRotation);
        deltaRotation.ToAngleAxis(out float angleInDegrees, out Vector3 rotationAxis);
        angleInDegrees = (angleInDegrees > 180) ? angleInDegrees - 360 : angleInDegrees;
        Vector3 angularDisplacement = rotationAxis * angleInDegrees * Mathf.Deg2Rad;
        rb.angularVelocity = angularDisplacement / Time.deltaTime;
        lastRotation = newRotation;
    }
}
