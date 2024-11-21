using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Custom_ShootBall : MonoBehaviour
{

    public GameObject theBall;
    private GameObject spawnBall;
    public int delayTime = 5000;
    private int currentTime;
    private Vector3 thePosition;

    // Start is called before the first frame update
    void Start()
    {
        theBall.SetActive(false);
        thePosition = theBall.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        currentTime++;

        if (currentTime >= delayTime)
        {
            //optional.. delete previous spawn ball 
            //if(spawnBall!=null)
            //{         Destroy(spawnBall)}


            spawnBall = Instantiate(theBall);
            spawnBall.transform.position = thePosition;
            spawnBall.SetActive(true);
            currentTime = 0;
        }


    }
}
