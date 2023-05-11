using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public static event System.Action onBounce;
    public static Vector2 spawnpoint;
    public static BallScript self;
    public ObjectGravity objGrav;
    private void Start()
    {
        self = this;
    }
    private void OnCollisionEnter2D()
    {
        onBounce?.Invoke();
    }
}
