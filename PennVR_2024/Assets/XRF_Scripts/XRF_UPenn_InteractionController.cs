using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class XRF_UPenn_InteractionController : MonoBehaviour
{

    public Material HighlightMaterial;

    [System.Serializable]
    public enum InteractionType // your custom enumeration
    {
        OnOffController,
        AnimationController,
        SceneChangeController,
        GrabAndReturn,
        GrabAndStay,
        TeleportController,
        AudioPlayer,
        MoveFromTo,
        CallEvent,
        GrabAttachToHand,
        MakeObjectChild
    };

    public InteractionType myType = InteractionType.OnOffController;  // this public var should appear as a drop down

    //animation stuff
    public GameObject ObjectWithAnimation;
    private Animator theAnimator;

    public bool isSelected;
    public bool isTeleporter;


    //scene load stuff
    public string SceneToLoad;

    //on off stuff
    public int NumberOfThingsToTurnON = 1;
    public int NumberOfThingsToTurnOFF = 1;
    public GameObject[] StartOFFClickON;
    public GameObject[] StartONClickOFF;
    private bool OnOffSwitch;



    //grab stuff
    public Vector3 originalPos = Vector3.zero;
    public bool isGrabbable;
    public bool handGrab;

    //audio stuff
    public AudioSource theAudioSource;
    public AudioClip theAudioClip;

    //move from to
    public GameObject thingToMove;
    public Transform transformPositionToMoveTo;


    //call event
    public UnityEvent EventToCall;

    //make object child
    public GameObject thingToChange;
    public GameObject newParentObject;

    private void Start()
    {
        if (myType == InteractionType.OnOffController)
        {
            OnOff(false, true);
        }
        else if (myType == InteractionType.AnimationController)
        {
            theAnimator = ObjectWithAnimation.GetComponent<Animator>();
            if (theAnimator != null)
            {
                string animName = theAnimator.runtimeAnimatorController.animationClips[0].name;
                //Debug.Log("my animation is called: " + animName);
                //play on start but set to false so it stops
                theAnimator.Play(animName, 0, 0);
                theAnimator.enabled = false;
            }
        }
        else if (myType == InteractionType.TeleportController)
        {
            isTeleporter = true;
        }
        else if (myType == InteractionType.GrabAndReturn)
        {
            originalPos = gameObject.transform.position;
            isGrabbable = true;
        }
        else if (myType == InteractionType.GrabAndStay)
        {
            isGrabbable = true;
        }
        else if (myType == InteractionType.AudioPlayer)
        {
            //stop the audio clip
            //Debug.Log("hello im an audio player");
            theAudioSource.clip = theAudioClip;
            theAudioSource.playOnAwake = false;
            theAudioSource.loop = false;
            theAudioSource.Pause();
        }
        else if (myType == InteractionType.MoveFromTo)
        {
        }
        else if (myType == InteractionType.CallEvent)
        {
        }
        else if (myType == InteractionType.GrabAttachToHand)
        {
            originalPos = gameObject.transform.position;
            handGrab = true;
        }
        else if (myType == InteractionType.MakeObjectChild)
        {
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("i bumped into something called: " + other.name);
        DoTheThing();
    }

    public void DoTheThing()
    {
        //i clicked on this thing with an interaction controller on it
        //Debug.Log("I did the thing");
        if (myType == InteractionType.AnimationController)
        {
            //play or pause animation
            //note, the animator must make a transition to exit if it is not on loop.

            string animName = theAnimator.runtimeAnimatorController.animationClips[0].name;
            Debug.Log("my animation is called: " + animName);

            Debug.Log("my animator state info normalized time: " + theAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime); //< if this is at 1 its done
            Debug.Log("my animator state info is loop true: " + theAnimator.runtimeAnimatorController.animationClips[0].isLooping); //< if this is at 1 its done

            if(theAnimator.runtimeAnimatorController.animationClips[0].isLooping) //if loop is true
            {
                if (theAnimator.isActiveAndEnabled) //if i am currently on, turn off
                {
                    Debug.Log("my animation was playing and enabled, it will stop now");
                    theAnimator.enabled = false;
                }
                else //if i am currently off, turn on
                {
                    theAnimator.enabled = true;
                }
            }
            else //loop is false
            {
                if (theAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1) //if i am done with my animation sequence (at the end)
                {
                    theAnimator.Play(animName, 0, 0);
                    theAnimator.enabled = true;
                }
                else //either i am paused or i am playing and not done
                {
                    if (theAnimator.isActiveAndEnabled) //if i am currently on, turn off
                    {
                        Debug.Log("my animation was playing and enabled, it will stop now");
                        theAnimator.enabled = false;
                    }
                    else //if i am currently off, turn on
                    {
                        theAnimator.enabled = true;
                    }
                }
            }

        }
        else if (myType == InteractionType.SceneChangeController)
        {
            //load a scene
            Debug.Log("scene");
            if (SceneToLoad != null && SceneToLoad != "")
            {
                if (Application.CanStreamedLevelBeLoaded(SceneToLoad))
                {
                    SceneManager.LoadScene(SceneToLoad);

                }
                else
                {
                    Debug.LogError("Hey, I couldn't find that scene name. Check if it is spelled correctly in the TriggerInteraction and if it is added to build settings (file > build settings)");
                }
            }
            else
            {
                Debug.LogError("Hey, your scene name is blank, please enter the scene name and ensure that the scene is added to your build settings (file > build settings)");
            }
        }
        else if (myType == InteractionType.OnOffController)
        {
            Debug.Log("on off");

            OnOffSwitch = !OnOffSwitch;
            if (OnOffSwitch)
            {
                OnOff(true, false);
            }
            else
            {
                OnOff(false, true);
            }
        }
        else if (myType == InteractionType.GrabAndReturn)
        {
            //movement handled in vr conroller raycast interactions .cs

        }
        else if (myType == InteractionType.GrabAndStay)
        {
            //movement handled in vr conroller raycast interactions .cs

        }
        else if (myType == InteractionType.TeleportController)
        {
            //movement handled in vr conroller raycast interactions .cs
        }
        else if (myType == InteractionType.AudioPlayer)
        {
            //play the audio from 0

            //need audio source
            //need audio clip
            if (theAudioSource.isPlaying && theAudioSource.clip == theAudioClip)
            {
                theAudioSource.Stop();
            }
            else
            {
                theAudioSource.clip = theAudioClip;
                theAudioSource.Play(0);
            }
        }

        else if (myType == InteractionType.MoveFromTo)
        {
            thingToMove.transform.position = transformPositionToMoveTo.position;
        }
        else if (myType == InteractionType.CallEvent)
        {
            Debug.Log("im going to call this event");
            EventToCall.Invoke();
            Debug.Log("i called my event");

        }
        else if (myType == InteractionType.GrabAttachToHand)
        {
            //make it a child of your hand... when you let go, let it go
            //movement handled in vr conroller raycast interactions .cs

        }
        else if (myType == InteractionType.MakeObjectChild)
        {
            thingToChange.transform.parent = newParentObject.transform;
        }
    }
    void OnOff(bool bool1, bool bool2)
    {
        foreach (GameObject g in StartOFFClickON)
        {
            if (g != null)
            {
                g.SetActive(bool1);
            }
        }
        foreach (GameObject g in StartONClickOFF)
        {
            if (g != null)
            {
                g.SetActive(bool2);
            }
        }
    }
}
