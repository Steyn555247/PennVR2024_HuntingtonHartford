using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class XRF_RaycastInteractions_VRController : MonoBehaviour
{
    #region PUBLIC VARIABLES
    public float laserDistance = 100.0f;
    public GameObject controllerGameObject;
    public GameObject cameraRig;
    public GameObject cameraEye;
    public GameObject feetIcon;
    public GameObject pointerPrefab;
    #endregion

    #region PRIVATE VARIABLES
    private bool dontHighlight;
    private Material[] tempMaterialsHigh;
    private Material[] matsHigh;
    private GameObject tempSelectedObject;
    private GameObject hitObject;
    private Vector3 endPointRotation;
    private bool Clickable;
    private bool Teleportable;
    private Vector3 endPoint;
    private bool iGrabbedYou;
    private bool grabable;
    private bool handGrab;
    private GameObject grabbedObject;
    private GameObject handGrabParent;
    private float moveLength;
    private float tempDistance;
    private Vector3 basePosObject;
    private Vector3 clickOrigin;
    private LineRenderer lineRend;
    private bool triggerDown = false;
    private float triggerDownTolerance = 0.6f;
    private float triggerUpTolerance = 0.4f;
    private bool joystickDown = false;
    private float joystickDownTolerance = 0.6f;
    private float joystickUpTolerance = 0.4f;
    public bool isLeftHand = false;

    #endregion

    #region START FUNCTION

    void Start()
    {
        lineRend = controllerGameObject.gameObject.AddComponent<LineRenderer>();
        lineRend.material = new Material(Shader.Find("Standard"));
        lineRend.material.color = Color.red;
        lineRend.startWidth = 0.002f;
        lineRend.endWidth = 0.002f;
        lineRend.positionCount = 2;
        pointerPrefab = Instantiate(pointerPrefab);
        pointerPrefab.SetActive(false);
        feetIcon = Instantiate(feetIcon);
        feetIcon.SetActive(false);
    }
    #endregion

    private void Update()
    {
        //check for input at the start of each frame
        if (cameraRig.activeSelf)
        {
            checkForOculusInput();

            Vector3 origin = controllerGameObject.transform.position;
            Vector3 direction = controllerGameObject.transform.forward;
            Ray ray = new Ray(origin, direction);

            RaycastHit myRayHit;
            if (Physics.Raycast(ray, out myRayHit, laserDistance) && controllerGameObject.activeSelf)
            {
                //i shot out a ray and it hit something
                //Debug.Log("I hit something");
                hitObject = myRayHit.transform.gameObject;
                endPointRotation = myRayHit.normal;

                if (!hitObject.GetComponent<Collider>().isTrigger && hitObject.GetComponent<XRF_UPenn_InteractionController>())
                {

                    //i shot out a ray and hit something with an interaction controller
                    Debug.Log("I hit something with an interaction controller on it");
                    endPoint = myRayHit.point;


                    if (hitObject.GetComponent<XRF_UPenn_InteractionController>().isTeleporter)
                    {
                        RayMissed();
                        Teleportable = true;
                        feetIcon.transform.position = endPoint;
                        //feetIcon.transform.rotation = Quaternion.FromToRotation(Vector3.up, endPointRotation);
                        feetIcon.transform.eulerAngles = new Vector3(feetIcon.transform.eulerAngles.x, controllerGameObject.transform.eulerAngles.y, feetIcon.transform.eulerAngles.z);
                        feetIcon.SetActive(true);
                        grabable = false;
                    }
                    else if (hitObject.GetComponent<XRF_UPenn_InteractionController>().isGrabbable)
                    {
                        tempDistance = Vector3.Distance(origin, myRayHit.point);
                        RayHit(hitObject);
                        Clickable = false;
                        feetIcon.SetActive(false);
                        Teleportable = false;
                        handGrab = false;
                        Debug.Log("hey i hit a grabbable");

                        if (iGrabbedYou)
                        {
                            Debug.Log("hey i grabbed is true");
                            Vector3 grabEndPoint = origin + direction * moveLength;
                            Vector3 movePosition = basePosObject + (grabEndPoint - clickOrigin);
                            grabbedObject.transform.position = movePosition;
                        }
                        else
                        {
                            grabable = true;
                        }
                    }
                    else if(hitObject.GetComponent<XRF_UPenn_InteractionController>().handGrab)
                    {
                        tempDistance = Vector3.Distance(origin, myRayHit.point);
                        RayHit(hitObject);
                        Clickable = false;
                        feetIcon.SetActive(false);
                        Teleportable = false;
                        grabable = false;
                        Debug.Log("hey i hit a grabbable");

                        if (iGrabbedYou)
                        {
                            Debug.Log("hey i grabbed is true");
                            //Vector3 grabEndPoint = origin + direction * moveLength;
                            //Vector3 movePosition = basePosObject + (grabEndPoint - clickOrigin);
                            //grabbedObject.transform.position = movePosition;
                        }
                        else
                        {
                            handGrab = true;
                        }
                    }
                    else
                    {
                        RayHit(hitObject);
                        Clickable = true;
                        feetIcon.SetActive(false);
                        Teleportable = false;
                        grabable = false;
                        handGrab = false;
                    }
                }
                else
                {
                    endPoint = origin + direction * 0.5f;
                    RayMissed();
                    feetIcon.SetActive(false);
                    Teleportable = false;
                    grabable = false;
                    handGrab = false;
                }
            }
            else
            {
                endPoint = origin + direction * 0.5f;
                RayMissed();
                feetIcon.SetActive(false);
                Teleportable = false;
                grabable = false;
                handGrab = false;
            }

            pointerPrefab.transform.position = endPoint;
            lineRend.SetPosition(0, origin);
            lineRend.SetPosition(1, endPoint);
        }
    }

    public void ClickTheButton(GameObject hitObject)
    {
        XRF_UPenn_InteractionController[] myInteractions = hitObject.GetComponents<XRF_UPenn_InteractionController>();
        foreach (XRF_UPenn_InteractionController t in myInteractions)
        {
            t.DoTheThing();
        }
    }

    void RayHit(GameObject touchObject)
    {
        if (tempSelectedObject != touchObject)
        {
            if (tempSelectedObject != null)
            {
                UnHighlightObj(tempSelectedObject);
            }
        }
        tempSelectedObject = touchObject;
        HighlightObj(tempSelectedObject);
    }
    void RayMissed()
    {
        //Debug.Log("ray missed");
        Clickable = false;

        if (tempSelectedObject != null)
        {
            UnHighlightObj(tempSelectedObject);
            tempSelectedObject = null;
        }
    }
    void HighlightObj(GameObject highlightThis)
    {
        MeshRenderer rend = highlightThis.transform.gameObject.GetComponent<MeshRenderer>();
        if (rend != null)
        {
            if (!dontHighlight && highlightThis.GetComponent<XRF_UPenn_InteractionController>().isSelected == false)
            {
                tempMaterialsHigh = highlightThis.transform.gameObject.GetComponent<MeshRenderer>().sharedMaterials;
                matsHigh = new Material[tempMaterialsHigh.Length];

                Material highlightMaterial = highlightThis.GetComponent<XRF_UPenn_InteractionController>().HighlightMaterial;

                for (int i = 0; i < tempMaterialsHigh.Length; i++)
                {
                    matsHigh[i] = highlightMaterial;
                }
                highlightThis.transform.gameObject.GetComponent<MeshRenderer>().sharedMaterials = matsHigh;
                dontHighlight = true;
                highlightThis.GetComponent<XRF_UPenn_InteractionController>().isSelected = true;
            }
        }
    }
    void UnHighlightObj(GameObject unHighlightThis)
    {
        MeshRenderer rend = unHighlightThis.GetComponent<MeshRenderer>();
        if (rend != null && unHighlightThis.GetComponent<XRF_UPenn_InteractionController>().isSelected == true)
        {
            unHighlightThis.transform.gameObject.GetComponent<MeshRenderer>().sharedMaterials = tempMaterialsHigh;
            dontHighlight = false;
            unHighlightThis.GetComponent<XRF_UPenn_InteractionController>().isSelected = false;
        }
    }



    void checkForOculusInput()
    {
        //OCULUS TOUCH TRIGGERS
        if(isLeftHand)
        {
            float LTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);//(range of 0.0f to 1.0f)
            if (!triggerDown)
            {
                if (LTrigger > triggerDownTolerance)
                {
                    Debug.Log("Trigger clicked.");
                    triggerDown = true;
                    TriggerClick();
                }
            }
            else
            {
                if (LTrigger < triggerUpTolerance)
                {
                    Debug.Log("Trigger unclicked.");
                    triggerDown = false;
                    TriggerUnClick();
                }
            }
        }
        else
        {
            float RTrigger = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);//(range of 0.0f to 1.0f)
            if (!triggerDown)
            {
                if (RTrigger > triggerDownTolerance)
                {
                    Debug.Log("Trigger clicked.");
                    triggerDown = true;
                    TriggerClick();
                }
            }
            else
            {
                if (RTrigger < triggerUpTolerance)
                {
                    Debug.Log("Trigger unclicked.");
                    triggerDown = false;
                    TriggerUnClick();
                }
            }
        }



        if (isLeftHand)
        {
            //OCULUS TOUCH JOYSTICKS
            Vector2 LJoystick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);//(X/Y range of -1.0f to 1.0f)
            if (!joystickDown)
            {
                if (LJoystick.x > joystickDownTolerance)
                {
                    Debug.Log("Joystick clicked right.");
                    joystickDown = true;
                    JoyRightClick();
                }
                else if (LJoystick.x < -joystickDownTolerance)
                {
                    Debug.Log("Joystick clicked left.");
                    joystickDown = true;
                    JoyLeftClick();
                }
            }
            else
            {
                //if the absolute value of both joysticks is less than the tolerance, we have unclicked
                if (Math.Abs(LJoystick.x) < joystickUpTolerance)
                {
                    Debug.Log("Joystick unclicked.");
                    joystickDown = false;
                }
            }
        }
        else
        {
            //OCULUS TOUCH JOYSTICKS
            Vector2 RJoystick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch);//(X/Y range of -1.0f to 1.0f)
            if (!joystickDown)
            {
                if (RJoystick.x > joystickDownTolerance)
                {
                    Debug.Log("Joystick clicked right.");
                    joystickDown = true;
                    JoyRightClick();
                }
                else if (RJoystick.x < -joystickDownTolerance)
                {
                    Debug.Log("Joystick clicked left.");
                    joystickDown = true;
                    JoyLeftClick();
                }
            }
            else
            {
                //if the absolute value of both joysticks is less than the tolerance, we have unclicked
                if (Math.Abs(RJoystick.x) < joystickUpTolerance)
                {
                    Debug.Log("Joystick unclicked.");
                    joystickDown = false;
                }
            }
        }



    }


    void TriggerClick()
    {
        Debug.Log("hey i clicked the trigger button");

        if (Clickable)
        {
            ClickTheButton(hitObject);
        }
        else if (grabable)
        {
            grabbedObject = hitObject;
            iGrabbedYou = true;
            moveLength = tempDistance;
            basePosObject = grabbedObject.transform.position;
            clickOrigin = endPoint;

        }
        else if (handGrab)
        {
            //make this a child of the hand...
            iGrabbedYou = true;
            grabbedObject = hitObject;
            handGrabParent = hitObject.transform.parent.gameObject;
            basePosObject = grabbedObject.transform.position;
            grabbedObject.transform.SetParent(controllerGameObject.transform);
        }
        else if (Teleportable)
        {
            cameraRig.transform.position = new Vector3(endPoint.x + (cameraRig.transform.position.x - cameraEye.transform.position.x), endPoint.y, endPoint.z + (cameraRig.transform.position.z - cameraEye.transform.position.z));

            //this was testing teleporting and rotating around
            /*
            if (cameraRig.transform.up == endPointRotation && cameraRig.transform.up == Vector3.up)
            {
                //
                cameraRig.transform.position = new Vector3(endPoint.x + (cameraRig.transform.position.x - cameraEye.transform.position.x), endPoint.y, endPoint.z + (cameraRig.transform.position.z - cameraEye.transform.position.z));
            }
            else
            {
                //here we're going to add an optional rotation thing... 
                cameraRig.transform.rotation = Quaternion.FromToRotation(cameraRig.transform.up, Vector3.up);
                cameraRig.transform.position = endPoint;
                cameraRig.transform.rotation = Quaternion.FromToRotation(cameraRig.transform.up, endPointRotation);
            }
            */
        }
    }

    void TriggerUnClick()
    {
        Debug.Log("hey i UN clicked the trigger button");

        if (iGrabbedYou)
        {
            if (grabbedObject.GetComponent<XRF_UPenn_InteractionController>().originalPos != Vector3.zero)
            {
                grabbedObject.transform.position = grabbedObject.GetComponent<XRF_UPenn_InteractionController>().originalPos;
            }
            if(handGrab)
            {
                grabbedObject.transform.SetParent(handGrabParent.transform);
                grabbedObject.transform.position = grabbedObject.GetComponent<XRF_UPenn_InteractionController>().originalPos;
            }
            handGrab = false;
            iGrabbedYou = false;
            grabbedObject = null;
            RayMissed();//clear everything after you let go
        }
    }
    
    void JoyRightClick()
    {
        //Right
        Vector3 eyelocation = cameraEye.transform.position;
        Vector3 eyeFloor = new Vector3(eyelocation.x, cameraRig.transform.position.y, eyelocation.z);
        cameraRig.transform.RotateAround(eyeFloor, cameraRig.transform.up, 45.0f);
    }

    void JoyLeftClick()
    {
        //Left
        Vector3 eyelocation = cameraEye.transform.position;
        Vector3 eyeFloor = new Vector3(eyelocation.x, cameraRig.transform.position.y, eyelocation.z);
        cameraRig.transform.RotateAround(eyeFloor, cameraRig.transform.up, -45.0f);
    }
}