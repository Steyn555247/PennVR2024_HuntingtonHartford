using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Custom_ClimbInteraction : MonoBehaviour
{
    private float gripTolerance = 0.5f;
    private bool leftGripped;
    private bool rightGripped;
    public GameObject leftControllerOrigin;
    private Vector3 leftControllerStartPosition;
    public GameObject rightControllerOrigin;
    private Vector3 rightControllerStartPosition;
    public GameObject cameraRigOrigin;
    private Vector3 cameraRigStartPosition;

    // Update is called once per frame
    void Update()
    {
        float LGrip = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger);//(range of 0.0f to 1.0f)
        if (LGrip != 0 && !rightGripped)
        {
            if(leftGripped)
            {
                if(LGrip<gripTolerance)
                {
                    //i was gripping and i let go
                    leftGripped = false;
                    Debug.Log("Left Controller Ungripped.");
                }
            }
            else
            {
                if(LGrip > gripTolerance)
                {
                    //i was not gripping and i gripped
                    leftGripped = true;
                    Debug.Log("Left Controller Gripped.");
                    leftControllerStartPosition = leftControllerOrigin.transform.position;
                    cameraRigStartPosition = cameraRigOrigin.transform.position;
                }
            }
        }
        float RGrip = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger);//(range of 0.0f to 1.0f)
        if (RGrip != 0 && !leftGripped)
        {
            if (rightGripped)
            {
                if (RGrip < gripTolerance)
                {
                    //i was gripping and i let go
                    rightGripped = false;
                    Debug.Log("Right Controller Ungripped.");
                }
            }
            else
            {
                if (RGrip > gripTolerance)
                {
                    //i was not gripping and i gripped
                    rightGripped = true;
                    Debug.Log("Right Controller Gripped.");
                    rightControllerStartPosition = rightControllerOrigin.transform.position;
                    cameraRigStartPosition = cameraRigOrigin.transform.position;
                }
            }
        }

        if(leftGripped)
        {
            //handle "pulling" camera
            Vector3 delta = leftControllerStartPosition - leftControllerOrigin.transform.position;
            Vector3 transformedPosition = cameraRigStartPosition + delta;
            cameraRigOrigin.transform.position = transformedPosition;
        }


        if (rightGripped)
        {
            //handle "pulling" camera
            Vector3 delta = rightControllerStartPosition - rightControllerOrigin.transform.position;
            Vector3 transformedPosition = cameraRigStartPosition + delta;
            cameraRigOrigin.transform.position = transformedPosition;
        }
    }
}
