using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using Oculus;

public class InClassScript2 : MonoBehaviour
{
    [Tooltip("hey this next field is the integer we do something to")]
    public int myInteger = 5;//this value is overridden by changes in the editor
    private Vector3 myVector3 = new Vector3(0,1,1);
    public GameObject myGameobject;
    private Transform myTransform;
    public MeshRenderer mr;
    private MeshCollider mc;
    private Image myImage;

    private float theFloat;


    void Start()
    {
        //this function runs when the program begins 
        //and it runs only one time

        myInteger = myInteger + 1;

        Debug.Log("hello, after the start function, myInteger is: " + myInteger);


        mySuperDuperFunction();

        theFloat = EditTheFloat(myInteger);
        Debug.Log("the float after my method is: " + theFloat);


        float remappedThing = map(40.0f, 0.0f, 100.0f, 0.0f, 1.0f);
        Debug.Log("remapped is: " + remappedThing);

        //int whatever = myGameobject.GetComponent<MeshFilter>().mesh.vertices.Length;

        myGameobject.GetComponent<MeshFilter>().mesh = new Mesh();
    }

    void Update()
    {
        //this built in function runs over and over for the duration of 
        //the script being active


        myInteger = myInteger + 1;

        //Debug.Log("hello, after the update function, myInteger is: " + myInteger);

    }


    private void mySuperDuperFunction()
    {
        Debug.Log("hello, i am executing code in my helper function");


    }

    private float EditTheFloat(int theThingToEdit)
    {
        float localFloat = (float)(theThingToEdit);

        localFloat = localFloat + 5000.0f;
        localFloat = localFloat / 200.0f;
        localFloat = localFloat + 10.0f;
        localFloat = localFloat / 2.55644f;

        return localFloat;

    }


    // c#
    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
}
