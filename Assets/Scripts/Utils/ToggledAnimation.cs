using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//thank you for your inferior animation crap, unity

public class ToggledAnimation : MonoBehaviour
{
    public AnimationClip animation;
    bool _enabledState;
    public bool _enabled
    {
        get => _enabled;
    }

    Coroutine routine;
    float elaspedTime = 0f;
    public bool playScaled = false;
    public bool playOnFixed = false;
    void Toggle()
    {
        if (_enabled)
        {

        }
        else
        {

        }
    }
    IEnumerator Animate()
    {
        //Animation
        while (elaspedTime < animation.length)
        {
            elaspedTime +=
                playScaled ?
                    playOnFixed ? Time.fixedDeltaTime         : Time.deltaTime
                :   playOnFixed ? Time.fixedUnscaledDeltaTime : Time.unscaledDeltaTime;

            animation.SampleAnimation(gameObject, elaspedTime);
            yield return playOnFixed ? new WaitForFixedUpdate() : new WaitForEndOfFrame();
        }
        //End state
        animation.SampleAnimation(gameObject, _enabled ? animation.length : 0f);
    }
    
}
