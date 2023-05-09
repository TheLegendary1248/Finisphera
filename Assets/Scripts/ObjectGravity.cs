using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGravity : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 gravity;
    const float scale = -20f;
    private void FixedUpdate()
    {
        rb.AddForce(rb.mass * gravity);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        gravity = collision.GetContact(0).normal * scale;   
    }
}
