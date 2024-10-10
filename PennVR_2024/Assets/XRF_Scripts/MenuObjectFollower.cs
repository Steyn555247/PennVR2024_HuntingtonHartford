using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuObjectFollower : MonoBehaviour
{
    public GameObject camToFollow;
    public GameObject objectFollowing;

    public float farDistanceTolerance = 0.5f;
    public float closeDistanceTolerance = 0.2f;

    public float speed = 0.02f;

    private bool startFollow = false;
    private float startTime;
    private float journeyLength;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camPos = camToFollow.transform.position;
        Vector3 camPosXZ = new Vector3(camPos.x, 0, camPos.z);

        Vector3 followPos = objectFollowing.transform.position;
        Vector3 followPosXZ = new Vector3(followPos.x, 0, followPos.z);

        if (!startFollow)
        {
            if (Vector3.Distance(camPosXZ, followPosXZ) > farDistanceTolerance)
            {
                startTime = Time.time;
                journeyLength = Vector3.Distance(followPosXZ, camPosXZ);
                startFollow = true;
            }
        }
        else
        {
            if(Vector3.Distance(camPosXZ, followPosXZ) > closeDistanceTolerance)
            {
                float distCovered = (Time.time - startTime) * speed;

                // Fraction of journey completed equals current distance divided by total distance.
                float fractionOfJourney = distCovered / journeyLength;

                objectFollowing.transform.position = Vector3.Lerp(objectFollowing.transform.position, camToFollow.transform.position, fractionOfJourney);

            }
            else
            {
                startFollow = false;
            }

        }
    }
}
