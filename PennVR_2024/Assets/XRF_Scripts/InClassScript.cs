
//these are where you add libraries and other namespaces
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this is the start of my script/class
public class InClassScript : MonoBehaviour //in class script is the name of my script
{
    //i can add private and public varibles out side of the class functions

    public GameObject TheGameObject;
    private float someGlobalFloat = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        //this runs one time, good for setup
        TheGameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //this runs over and over once per frame

        MyOwnFunction();

        float someLocalFloat = 200.0f;
        float theFloat = MyOwnMethod(someLocalFloat);


        TheGameObject.GetComponent<MeshRenderer>().material.mainTextureScale = new Vector2(theFloat, someLocalFloat);
    }

    void MyOwnFunction()
    {
        //do something when called
    }

    float MyOwnMethod(float PassInValue)
    {
        //return some value when called

        float myFloat = PassInValue + 11 + someGlobalFloat;
        return myFloat;
    }
}
