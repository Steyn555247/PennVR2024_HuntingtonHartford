using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Custom_StartAnimationWhenConditionMet : MonoBehaviour
{
    public GameObject ObjectWithAnimation;
    private Animator theAnimator;

    // Start is called before the first frame update
    void Start()
    {
        theAnimator = ObjectWithAnimation.GetComponent<Animator>();
        if (theAnimator != null)
        {
            string animName = theAnimator.runtimeAnimatorController.animationClips[0].name;
            //play on start but set to false so it stops
            theAnimator.Play(animName, 0, 0);
            theAnimator.enabled = false;
        }
    }

    public void StartTheAnimation()
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
