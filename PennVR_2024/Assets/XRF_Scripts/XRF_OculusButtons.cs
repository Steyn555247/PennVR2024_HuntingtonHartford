using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRF_OculusButtons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //OCULUS TOUCH BUTTONS
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch))
        {
            Debug.Log("I pressed down the X button on the Left Controller");
        }
        if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.LTouch))
        {
            Debug.Log("I pressed down the Y button on the Left Controller");
        }
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            Debug.Log("I pressed down the A button on the Right Controller");
        }
        if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch))
        {
            Debug.Log("I pressed down the B button on the Right Controller");
        }

        //OCULUS TOUCH JOYSTICKS
        Vector2 LJoystick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);//(X/Y range of -1.0f to 1.0f)
        if (LJoystick.x != 0 || LJoystick.y != 0)
        {
            Debug.Log("Left Controller joystick touched.");
        }
        Vector2 RJoystick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch);//(X/Y range of -1.0f to 1.0f)
        if (RJoystick.x != 0 || RJoystick.y != 0)
        {
            Debug.Log("Right Controller joystick touched.");
        }

        //OCULUS TOUCH TRIGGERS
        float LTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);//(range of 0.0f to 1.0f)
        if (LTrigger != 0)
        {
            Debug.Log("Left Controller Trigger touched.");
        }
        float LGrip = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger);//(range of 0.0f to 1.0f)
        if (LGrip != 0)
        {
            Debug.Log("Left Controller Grip touched.");
        }
        float RTrigger = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);//(range of 0.0f to 1.0f)
        if (RTrigger != 0)
        {
            Debug.Log("Right Controller Trigger touched.");
        }
        float RGrip = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger);//(range of 0.0f to 1.0f)
        if (RGrip != 0)
        {
            Debug.Log("Right Controller Grip touched.");
        }
    }
}
