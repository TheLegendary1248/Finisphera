using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public static event System.Action onBounce;
    public static Vector2 spawnpoint;
    public static BallScript self;
    public ObjectGravity objGrav;
    public Rigidbody2D rb;
    private void Start()
    {
        self = this;
    }
    private void OnCollisionEnter2D()
    {
        onBounce?.Invoke();
    }
    public void Update()
    {
        ProcessUserInput();
    }
    void ProcessUserInput()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 force = CameraControl.MousePos() - (Vector2)transform.position;
            rb.AddForce(force * rb.mass, ForceMode2D.Impulse);
        }

    }
}
