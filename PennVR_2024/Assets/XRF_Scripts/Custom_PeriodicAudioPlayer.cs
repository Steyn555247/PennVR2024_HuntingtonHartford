using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Custom_PeriodicAudioPlayer : MonoBehaviour
{

    //audio stuff
    public AudioSource theAudioSource;
    public AudioClip theAudioClip;

    public float timeInterval = 60.0f;


    // Start is called before the first frame update
    void Start()
    {
        theAudioSource.clip = theAudioClip;
        theAudioSource.playOnAwake = false;
        theAudioSource.loop = false;
        theAudioSource.Pause();

        StartCoroutine(playAudioPeriodic());
    }

    public IEnumerator playAudioPeriodic()
    {
        yield return new WaitForSeconds(timeInterval);

        theAudioSource.clip = theAudioClip;
        theAudioSource.Play(0);

        StartCoroutine(playAudioPeriodic());
    }
}
