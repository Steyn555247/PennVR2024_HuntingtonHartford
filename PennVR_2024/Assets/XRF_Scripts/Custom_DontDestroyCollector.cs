using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Custom_DontDestroyCollector : MonoBehaviour
{
    public static Custom_DontDestroyCollector instance;


    public bool collectedUmbrella;
    public bool collectedFireExtinguisher;
    public bool collectedHat;

    private GameObject umbrella;
    private GameObject fireExtinguisher;
    private GameObject hat;


    private GameObject rain;
    private GameObject fire;
    private GameObject crane;


    //this is a script that will follow you scene-to-scene. to check for if you have collected 
    //certain items.
    //it needs to check for tagged items
    //it needs to remember bool values for if you have itmes
    //it needs to turn certain things off if you have items


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            instance.CheckforStuff();
            Destroy(gameObject);
        }


        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        CheckforStuff();
    }
    private void CheckforStuff()
    {
        umbrella = GameObject.FindGameObjectWithTag("umbrella");
        fireExtinguisher = GameObject.FindGameObjectWithTag("fireExtinguisher");
        hat = GameObject.FindGameObjectWithTag("hat");

        rain = GameObject.FindGameObjectWithTag("rain");
        fire = GameObject.FindGameObjectWithTag("fire");
        crane = GameObject.FindGameObjectWithTag("crane");


        Debug.Log("hello i am checking for stuff");


        if (collectedUmbrella)
        {
            if (umbrella != null)
            {
                umbrella.SetActive(false);
                //we will also turn off the rain somehow...
            }
            if(rain !=null)
            {
                rain.SetActive(false);
            }
        }
        if (collectedFireExtinguisher)
        {
            if (fireExtinguisher != null)
            {
                fireExtinguisher.SetActive(false);
                //we will also turn off the fire somehow
            }
            if(fire !=null)
            {
                fire.SetActive(false);
            }
        }
        if (collectedHat)
        {
            if (hat != null)
            {
                hat.SetActive(false);
                //we will also turn off the fog? somehow
            }
            if(crane !=null)
            {
                crane.SetActive(false);
            }
        }

    }


    private void Update()
    {
        //CheckforStuff();

        if (!collectedUmbrella)
        {
            if (umbrella != null)
            {
                if(umbrella.activeSelf==false)
                {
                    collectedUmbrella = true;
                }
            }
        }
        if (!collectedFireExtinguisher)
        {

            if (fireExtinguisher != null)
            {
                //Debug.Log("i found the fire extinguisher tag");

                if (fireExtinguisher.activeSelf == false)
                {
                    collectedFireExtinguisher = true;
                }
            }
        }
        if (!collectedHat)
        {
            if (hat != null)
            {
                if (hat.activeSelf == false)
                {
                    collectedHat = true;
                }
            }
        }
    }
}
