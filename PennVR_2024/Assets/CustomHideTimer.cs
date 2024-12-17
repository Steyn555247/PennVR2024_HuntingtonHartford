using UnityEngine;

public class HideAfterTime : MonoBehaviour
{
    // Time in seconds before the GameObject is hidden
    public float activeTime = 5f;

    // Internal timer
    private float timer = 0f;

    private void OnEnable()
    {
        // Reset the timer when the object is enabled
        timer = 0f;
    }

    private void Update()
    {
        // Increment the timer by the time elapsed since the last frame
        timer += Time.deltaTime;

        // Check if the timer has reached the specified active time
        if (timer >= activeTime)
        {
            // Disable the GameObject
            gameObject.SetActive(false);
        }
    }
}