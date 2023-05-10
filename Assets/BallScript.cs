using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public static event System.Action onBounce;
    public static Vector2 spawnpoint;

    private void OnCollisionEnter2D()
    {
        onBounce?.Invoke();
    }
}
