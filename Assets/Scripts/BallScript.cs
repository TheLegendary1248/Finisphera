using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public Vector2 spawnpoint;
    public Vector2 lastStrokePoint;
    public static BallScript self;
    public ObjectGravity objGrav;
    public Rigidbody2D rb;
    public GameObject[] disableGOList;
    public List<BallCheckpoints> checkPoints = new List<BallCheckpoints>();
    private void Start()
    {
        self = this;
        AddCheckpoint();
    }
    ///<summary>Shorthand for adding a checkpoint</summary>
    void AddCheckpoint() { checkPoints.Add(new BallCheckpoints(rb.velocity, transform.position)); Debug.Log("Added cp"); }
    private void Awake()
    {
        GameManager.onSwing += SetLastStrokePoint;
    }
    private void OnDestroy()
    {
        GameManager.onSwing -= SetLastStrokePoint;
    }
    void SetLastStrokePoint(bool state) { if (state) AddCheckpoint(); }
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
        Debug.Log("reset");
        BallCheckpoints cp = checkPoints[^1];
        rb.velocity = cp.velocity;
        transform.position = cp.position;
    }
    public void FullReset()
    {
        transform.position = spawnpoint;
    }    
}
public struct BallCheckpoints
{
    public Vector2 velocity;
    public Vector2 position;
    //Constructor
    public BallCheckpoints(Vector2 velocity, Vector2 position)
    {
        this.velocity = velocity;
        this.position = position;
    }
}
