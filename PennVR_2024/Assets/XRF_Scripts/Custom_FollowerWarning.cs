using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Custom_FollowerWarning : MonoBehaviour
{
    public GameObject followerObject;
    public GameObject cameraObject;

    public TMP_Text myWarningText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float myDistance = Vector3.Distance(followerObject.transform.position, cameraObject.transform.position);
        //Debug.Log(myDistance);


        myWarningText.text = myDistance.ToString();
        myWarningText.color = Color.Lerp(Color.red, Color.white, myDistance / 10.0f);
    }
}
