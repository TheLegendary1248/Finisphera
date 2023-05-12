using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
/// <summary>
/// This funky cool lil script animates the bounce counter
/// </summary>
public class BounceCounterAnimator : MonoBehaviour
{
    /// <summary>
    /// The spacing of the images
    /// </summary>
    [Tooltip("Space of the images")]
    public float spacing;
    /// <summary>
    /// Internal count kept
    /// </summary>
    uint internalCount = 0;

    public GameObject prefab;
    GameObject[] array = { };
    public AnimationCurve curve;
    Coroutine playingAnim;
    //Update the bounce counter
    void UpdateCurrentState()
    {
        int newCount = (int)GameManager.BouncesLeft;
        //Do nothing
        if (internalCount == newCount)
            return;
        //Remove some
        if (internalCount < newCount)
        {
            //If we need more counters
            if (newCount > array.Length)
            { 
                //DONT WORRY ABOUT IT YET
                /*
                new GameObject[] = 
                array.Concat()*/
            }
            GameObject[] needsUpdate = array[newCount..(int)internalCount];
            for (int i = 0; i < needsUpdate.Length; i++)
            {
                needsUpdate[i].GetComponent<Animation>().Rewind();
            }
        }
        //Add some
        if(internalCount > newCount)
        {
            //Resize array to accomodate the new bounce count
            if(array.Length < newCount)
            {

            }
            GameObject[] needsUpdate = array[(int)internalCount..newCount];
            for (int i = 0; i < needsUpdate.Length; i++)
            {
                needsUpdate[i].GetComponent<Animation>().Play();
            }
        }
        //Finalization
        SetAnim();
        internalCount = (uint)newCount;
    }
    void SetAnim()
    {
        if (playingAnim != null) StopCoroutine(playingAnim);
        playingAnim = StartCoroutine(Animate());
    }
    IEnumerator Animate()
    {
        //Get values
        RectTransform selfTS = (RectTransform)transform;
        float timestamp, timesince; timestamp = timesince = Time.time;
        float currentX = selfTS.anchoredPosition.x;
        Vector2 endStatePosition = new Vector2(internalCount * spacing / 2f, selfTS.anchoredPosition.y);
        //Animate
        while (curve.keys[^1].time < (timesince = Time.time - timestamp))
        {
            float interpol = curve.Evaluate(timesince);
            selfTS.anchoredPosition = new Vector2(Mathf.Lerp(currentX, endStatePosition.x, interpol), endStatePosition.y); 
            yield return new WaitForEndOfFrame();
        }
        //Snap to expected result
        selfTS.anchoredPosition = endStatePosition;
        //Remove Self
        playingAnim = null;
    }
}
