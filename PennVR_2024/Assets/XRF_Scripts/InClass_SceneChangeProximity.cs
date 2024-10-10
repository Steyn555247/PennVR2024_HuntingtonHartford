using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InClass_SceneChangeProximity : MonoBehaviour
{
    //this is a script which restarts the scene if two gameobjects get too close to each other

    public GameObject myCharacter;
    public GameObject thingToGetTooCloseToo;

    public float distanceThreshold;

    private float distanceBetween;

    public string theSceneToLoad;

    public Transform moveMeToHere;

    // Update is called once per frame
    void Update()
    {
        distanceBetween = Vector3.Distance(myCharacter.transform.position,thingToGetTooCloseToo.transform.position);


        if(distanceBetween < distanceThreshold)
        {
            //reload this scene
            //SceneManager.LoadScene(theSceneToLoad);
            myCharacter.transform.position = moveMeToHere.position;
        }
        else
        {
            Debug.Log("hey, nothing to worry about! you are " + distanceBetween + " meters from the thing.");
        }

    }
}
