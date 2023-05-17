using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public static Vector2 spawnpoint;
    public static Vector2 lastStrokePoint;
    public static BallScript self;
    public ObjectGravity objGrav;
    public Rigidbody2D rb;
    public GameObject[] disableGOList;
    private void Start()
    {
        self = this;
    }
    private void Awake()
    {
        GameManager.onSwing += SetLastStrokePoint;
    }
    private void OnDestroy()
    {
        GameManager.onSwing -= SetLastStrokePoint;
    }
    void SetLastStrokePoint(bool state) => lastStrokePoint = state ? transform.position : lastStrokePoint;
    private void OnCollisionEnter2D()
    {
        GameManager.bouncesLeft -= 1;
    }
    public void Remove() => Toggle(false);
    public void Toggle(bool flip)
    {
        for (int i = 0; i < disableGOList.Length; i++)
            disableGOList[i].SetActive(flip);
        objGrav.enabled = flip;
        rb.simulated = flip;
    }
    /// <summary>
    /// Process input
    /// </summary>
    public void ResetToLastPoint()
    {
        transform.position = lastStrokePoint;
    }
    public void FullReset()
    {
        transform.position = spawnpoint;
    }    
}
