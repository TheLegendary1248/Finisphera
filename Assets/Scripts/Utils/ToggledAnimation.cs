using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//thank you for your inferior animation crap, unity

public class ToggledAnimation : MonoBehaviour
{
    public AnimationClip animation;
    bool _activeState;
    public bool activeState
    {
        get => _activeState;
        set
        {
            if(value != _activeState)
            {
                _activeState = value;
                Toggle();
            }
        }
    }

    Coroutine routine;
    float elaspedTime = 0f;
    public bool playScaled = false;
    public bool playOnFixed = false;
    void Toggle()
    {
        if (routine == null) StartCoroutine(Animate());
        else elaspedTime = animation.length - elaspedTime;
    }
    //Clean up because routines can't exist while disabled
    private void OnDisable()
    {
        if (routine == null) return;
        StopCoroutine(routine);
        animation.SampleAnimation(gameObject, _activeState ? animation.length : 0f);
    }
    IEnumerator Animate()
    {
        //Animation
        while (elaspedTime < animation.length)
        {
            //Get time
            elaspedTime +=
                playScaled ?
                    playOnFixed ? Time.fixedDeltaTime         : Time.deltaTime
                :   playOnFixed ? Time.fixedUnscaledDeltaTime : Time.unscaledDeltaTime;

            animation.SampleAnimation(gameObject, (_activeState ? elaspedTime :  animation.length - elaspedTime) );
            yield return playOnFixed ? new WaitForFixedUpdate() : new WaitForEndOfFrame();
        }
        //Final state
        animation.SampleAnimation(gameObject, _activeState ? animation.length : 0f);
        elaspedTime = 0f;
        routine = null;
    }
    
}
