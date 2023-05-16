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
    //Proper event subscription
    public void OnEnable()
    {
        GameManager.onBouncesChanged += UpdateCurrentState;
    }
    public void OnDisable()
    {
        GameManager.onBouncesChanged -= UpdateCurrentState;
    }
    /// <summary>
    /// Updates the bounce counter
    /// </summary>
    public void UpdateCurrentState()
    {
        int newCount = (int)GameManager.bouncesLeft;
        Debug.Log($"New : {newCount} / Internal : {internalCount} / arr.Length : {array.Length}");
        //Do nothing
        if (internalCount == newCount)
            return;
        //Add some
        if (internalCount < newCount)
        {
            //If we need more counters
            if (newCount > array.Length)
            { 
                GameObject[] concat = new GameObject[newCount - array.Length];
                for (int i = 0; i < concat.Length; i++)
                {
                    GameObject go = Instantiate(prefab, transform);
                    ((RectTransform)go.transform).anchoredPosition = new Vector2((internalCount + i) * spacing, 0);
                    concat[i] = go;
                }
                array = array.Concat(concat).ToArray();
            }
            
            GameObject[] needsUpdate = array[(int)internalCount..newCount];
            for (int i = 0; i < needsUpdate.Length; i++)
            {
                needsUpdate[i].GetComponent<Animation>().Play();
            }
        }
        //Remove some
        if(internalCount > newCount)
        {
            GameObject[] needsUpdate = array[newCount..(int)internalCount];
            for (int i = 0; i < needsUpdate.Length; i++)
            {
                Animation anim = needsUpdate[i].GetComponent<Animation>();
                anim.Play();
            }
        }
        //Finalization
        internalCount = (uint)newCount;
        SetAnim();
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
        Vector2 endStatePosition = new Vector2((internalCount - 1) * -spacing / 2f, selfTS.anchoredPosition.y);
        //Animate
        while (curve.keys[^1].time > (timesince = Time.time - timestamp))
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
