using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Custom_SceneTimer : MonoBehaviour
{
    public GameObject[] thingsToTurnOn;
    public float timeBetweenSteps;

    private float timeStamp;
    private float deltaTime;
    private int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        timeStamp = Time.time;

        for(int i = 0; i < thingsToTurnOn.Length;i++)
        {
            thingsToTurnOn[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - timeStamp > timeBetweenSteps)
        {
            //turn something on
            if (counter < thingsToTurnOn.Length)
            {
                thingsToTurnOn[counter].SetActive(true);
            }

            //stamp the current time
            timeStamp = Time.time;

            //add to count
            counter++;
        }
    }
}
