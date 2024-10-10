using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Oculus;

public class MySpecialScriptInClass : MonoBehaviour
{
    //you can write whatever you want and it gets ignored by the program


    public int myNum = 5;
    public float myFloat = 5.2f;
    public GameObject theGameObject;
    public string theMessage = "called on loop";



    public Color leftColor = Color.blue;
    public Color rightColor = Color.red;
    private float startPos;


    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log("called once on start");

        startPos = this.transform.position.x;
    }

    // Update is called once per frame
    private void Update()
    {
       
        
        if(startPos < this.transform.position.x)
        {
            //im to the right
            theGameObject.GetComponent<MeshRenderer>().material.color = rightColor;
        }
        else if (startPos > this.transform.position.x)
        {
            //im to the left
            theGameObject.GetComponent<MeshRenderer>().material.color = leftColor;

        }
        else
        {
            Debug.Log("i am equal, dont do anything!!!!!! please!!");

        }



    }


}
