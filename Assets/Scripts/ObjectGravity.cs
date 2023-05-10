using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGravity : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject attractor;
    public Vector2 gravity;
    public float scale = -20f;
    private void Start()
    {
        gravity = new Vector2(0, scale);
    }
    private void FixedUpdate()
    {
        if (attractor)
            rb.AddForce(rb.mass * (transform.position - attractor.transform.position).normalized * scale);
        else
            rb.AddForce(rb.mass * gravity);
    }
    public void SetGravity(Vector2 vec)
    {
        attractor = null;
        gravity = vec;
    }
    public void SetAttractor(GameObject gb) => attractor = gb;
}
