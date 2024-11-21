using UnityEngine;

public class BallShooter : MonoBehaviour
{
    public Rigidbody ball;       // Assign the Rigidbody of the ball in the Inspector
    public Vector3 force = new Vector3(0, 300, 500); // Adjust initial force (x, y, z)

    void Start()
    {
        // Shoot the ball when the game starts
        ShootBall();
    }

    public void ShootBall()
    {
        if (ball != null)
        {
            ball.AddForce(force, ForceMode.Impulse); // Apply force once
        }
    }
}
