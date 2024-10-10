using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DontDestroyConditionHandler : MonoBehaviour
{
    public static DontDestroyConditionHandler instance;

    public List<bool> theConditions = new List<bool>();
    public List<UnityEvent> theEventsTrue = new List<UnityEvent>();
    public List<UnityEvent> theEventsFalse = new List<UnityEvent>();

    //create singleton pattern to avoid multiples of the same game manager moving from scene to scene
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    //invoke events based on if conditions are met
    private void Start()
    {
        for (int i = 0; i< theConditions.Count; i ++)
        {
            if(theConditions[i])
                theEventsTrue[i].Invoke();
            else
                theEventsFalse[i].Invoke();
        }
    }
}
