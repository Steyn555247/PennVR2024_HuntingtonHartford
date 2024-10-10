using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Custom_ProximitySceneChange : MonoBehaviour
{
    public Transform cameraRigOrigin;
    public Transform proximityObject;
    public float distanceThreshold = 1.0f;
    public string sceneToLoad;

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(cameraRigOrigin.position, proximityObject.position) < distanceThreshold)
        {
            //SceneManager.LoadScene(sceneToLoad);
            //turn on some gameobject
            //or play audio clip
        }
    }
}
