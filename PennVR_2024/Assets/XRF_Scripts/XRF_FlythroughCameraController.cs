using UnityEngine;


public class XRF_FlythroughCameraController : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject cameraRig;

    public int speedMultiplier = 6;
    public int movementSpeed = 5;
    public float mouseSensitivity = 0.25f;


    private Vector3 lastMouseRot = new Vector3(255, 255, 255);
    private Vector3 lastMousePitch = new Vector3(255, 255, 255);
    private bool mouseClicked = false;



    private void Start()
    {

    }
    private void Update()
    {

        //HANDLE ROTATION BELOW >>>
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            mouseClicked = true;
        }
        else if (Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2))
        {
            mouseClicked = false;
        }

        //if my mouse is clicked, start from the click point and rotate from there
        //if my mouse is unClicked, don't rotate at all

        if (mouseClicked)
        {
            //rotation of game object
            lastMouseRot = Input.mousePosition - lastMouseRot;
            lastMouseRot = new Vector3(0.0f, lastMouseRot.x * mouseSensitivity, 0.0f);
            lastMouseRot = new Vector3(0.0f, cameraRig.transform.eulerAngles.y + lastMouseRot.y, 0.0f);
            //mainCamera.transform.eulerAngles = lastMouseRot;



            //pitch of head
            lastMousePitch = Input.mousePosition - lastMousePitch;
            lastMousePitch = new Vector3(-lastMousePitch.y * mouseSensitivity, 0.0f, 0.0f);
            lastMousePitch = new Vector3(mainCamera.transform.localEulerAngles.x + Mathf.Clamp(lastMousePitch.x, -89, 89), 0.0f, 0.0f);
            //mainCamera.transform.localEulerAngles = lastMousePitch;


            //Vector3 cameraRotation = lastMouseRot + lastMousePitch;
            //mainCamera.transform.eulerAngles = cameraRotation;

            cameraRig.transform.eulerAngles = lastMouseRot;
            mainCamera.transform.localEulerAngles = lastMousePitch;

        }

        lastMouseRot = Input.mousePosition;
        lastMousePitch = Input.mousePosition;
        //<<< HANDLE ROTATION ABOVE



        //HANDLE MOVEMENT BELOW >>>
        Vector3 p = GetBaseInput();

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.Space))
        {
            p = p * speedMultiplier;
        }
        else
        {
            p = p * movementSpeed;
        }

        p = p * Time.deltaTime;
        Vector3 newPosition = cameraRig.transform.position;

        cameraRig.transform.Translate(p);

        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E))
        {
            newPosition.y = cameraRig.transform.position.y;
        }
        else
        {
            newPosition.x = cameraRig.transform.position.x;
            newPosition.z = cameraRig.transform.position.z;
        }

        cameraRig.transform.position = newPosition;


        //<<< HANDLE MOVEMENT ABOVE





    }

    private Vector3 GetBaseInput()
    {

        Vector3 p_Velocity = new Vector3();
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            p_Velocity += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            p_Velocity += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            p_Velocity += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            p_Velocity += new Vector3(1, 0, 0);
        }
        if (Input.GetKey(KeyCode.E))
        {
            p_Velocity += new Vector3(0, 1, 0);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            p_Velocity += new Vector3(0, -1, 0);
        }
        return p_Velocity;
    }
}
