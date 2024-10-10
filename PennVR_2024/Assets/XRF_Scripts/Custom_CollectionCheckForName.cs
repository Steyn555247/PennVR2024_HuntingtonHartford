using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Custom_CollectionCheckForName : MonoBehaviour
{
    public static Custom_CollectionCheckForName instance;

    public string[] uniqueObjectNames;
    private List<GameObject> objects = new List<GameObject>();
    private List<bool> theThingsCollected = new List<bool>();


//on start i need to make a list of booleans that is the same length as the list of unique object names
//when i change scenes i need to check if those booleans are on or off
//when i collect an object it needs to turn itself off and change a boolean for future scenes.


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

        MakeBools();
        DontDestroyOnLoad(gameObject);
    }

    private void MakeBools()
    {
        foreach (string s in uniqueObjectNames)
        {
            theThingsCollected.Add(false);
        }
    }
    private void Start()
    {
        CheckforStuff();
    }
    private void CheckforStuff()
    {
        Debug.Log("checking for stuff");

        objects = new List<GameObject>();

        for (int i = 0; i<uniqueObjectNames.Length;i++)
        {
            GameObject g = GameObject.Find(uniqueObjectNames[i]);
            if (g!=null)
            {
                Debug.Log("found: " + g.name);

                objects.Add(g);
            }
            else
            {
                Debug.Log("couldnt find anything");

                objects.Add(null);
            }
        }


        for(int i = 0; i < theThingsCollected.Count; i++)
        {
            if (theThingsCollected[i] == true)
            {
                if(objects[i] != null)
                {
                    objects[i].SetActive(false);
                }
            }
        }

    }


    private void Update()
    {
        Debug.Log("object length is " + objects.Count);
        for (int i = 0; i < objects.Count; i++)
        {
            if (objects[i] != null)
            {
                Debug.Log("im looking for " + objects[i].name);


                if (objects[i].activeSelf == false)
                {
                    theThingsCollected[i] = true;
                }
            }
        }
    }
}
