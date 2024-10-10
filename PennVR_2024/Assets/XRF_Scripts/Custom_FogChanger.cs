using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Custom_FogChanger : MonoBehaviour
{
    public float fogOnStartDistance = 2.0f;
    public float fogOnEndDistance = 5.0f;
    public float fogOffStartDistance = 0.0f;
    public float fogOffEndDistance = 300.0f;

    private bool turnOnFog;
    private bool fogIsOn;

    private float deltaTime;
    private float timeStamp;
    public float timeToChange = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        timeStamp = Time.time;

        RenderSettings.fogMode = FogMode.Linear;
        RenderSettings.fogEndDistance = fogOffEndDistance;
        RenderSettings.fogStartDistance = fogOffStartDistance;
    }

    public void ToggleFog()
    {
        Debug.Log("I will toggle fog");
        timeStamp = Time.time;
        turnOnFog = !turnOnFog;
    }

    // Update is called once per frame
    void Update()
    {
        if(turnOnFog == true)
        {
            if(fogIsOn == false)
            {
                //interpolate to make fog come on over time

                deltaTime = (Time.time - timeStamp) / timeToChange;


                //Debug.Log(deltaTime);

                float currentFogStart = Mathf.Lerp(fogOffStartDistance, fogOnStartDistance, deltaTime);
                float currentFogEnd = Mathf.Lerp( fogOffEndDistance, fogOnEndDistance, deltaTime);

                RenderSettings.fogEndDistance = currentFogEnd;
                RenderSettings.fogStartDistance = currentFogStart;

                if (currentFogEnd == fogOnEndDistance)
                {
                    fogIsOn = true;
                }
            }
        }
        else
        {
            if (fogIsOn == true)
            {
                //interpolate to make fog turn off over time

                deltaTime = (Time.time - timeStamp) / timeToChange;


                float currentFogStart = Mathf.Lerp(fogOnStartDistance, fogOffStartDistance, deltaTime);
                float currentFogEnd = Mathf.Lerp(fogOnEndDistance, fogOffEndDistance, deltaTime);

                RenderSettings.fogEndDistance = currentFogEnd;
                RenderSettings.fogStartDistance = currentFogStart;

                if (currentFogEnd == fogOffEndDistance)
                {
                    fogIsOn = false;
                }
            }
        }
    }
}
