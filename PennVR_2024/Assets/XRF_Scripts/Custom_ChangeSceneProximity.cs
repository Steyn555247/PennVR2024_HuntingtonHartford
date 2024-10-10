using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Custom_ChangeSceneProximity : MonoBehaviour
{
    public GameObject theCamera;
    private GameObject thisGameobject;
    public float distanceToLoadScene = 1.0f;
    public string sceneToLoad;

    // Start is called before the first frame update
    void Start()
    {
        thisGameobject = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(thisGameobject.transform.position, theCamera.transform.position) < distanceToLoadScene)
            {
            SceneManager.LoadScene(sceneToLoad);

        }
        
    }
}
