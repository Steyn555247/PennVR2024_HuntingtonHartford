using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Management;
using UnityEngine.Rendering;
using Unity.XR.Oculus;


public class XRF_CameraPlatformManager : MonoBehaviour
{
    [Header("Flythrough Camera Stuff")]
    public GameObject FlythroughCamera;

    [Header("VR Stuff")]
    public GameObject OVRCameraRig;

    private void Awake()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        Debug.Log("Stand Alone Windows");
        bool headsetOn = isHeadsetConnected();

        if (headsetOn)
        {
            Debug.Log("VR Mode");
            FlythroughCamera.SetActive(false);
            OVRCameraRig.SetActive(true);
        }
        else
        {
            Debug.Log("Flythrough Mode");
            FlythroughCamera.SetActive(true);
            OVRCameraRig.SetActive(false);
        }

#elif UNITY_ANDROID
        Debug.Log("Android");
        //inside of unity_android we will need to differentiate between oculus and mobile android...
        FlythroughCamera.SetActive(false);
        OVRCameraRig.SetActive(true);
#endif
    }

    public bool isHeadsetConnected()
    {
        List<XRDisplaySubsystem> displaySubsystems = new List<XRDisplaySubsystem>();
        SubsystemManager.GetInstances<XRDisplaySubsystem>(displaySubsystems);
        foreach (var subsystem in displaySubsystems)
        {
            if (subsystem.running)
            {
                return true;
            }
        }
        return false;
    }
}