using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    GameObject[] array;
    public AnimationCurve curve;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void UpdateCurrentState()
    {
        //Do nothing
        if (internalCount == GameManager.BouncesLeft)
            return;
        //Remove some
        if(internalCount < )
            
        //Finalization
        internalCount = GameManager.BouncesLeft;
    }
    void AnimateRemoval()
    {

    }
    void AnimateAddition()
    {

    }
    IEnumerable Animate()
    {
        yield return null;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
