using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    static event System.Action onBounce;
    public static Vector2 spawnpoint;
    public static BallScript self;
    public ObjectGravity objGrav;
    public Rigidbody2D rb;
    public GameObject[] disableGOList;
    private void Start()
    {
        self = this;
    }
    private void OnCollisionEnter2D()
    {
        onBounce?.Invoke();
        GameManager.bouncesLeft -= 1;
    }
    public void Update()
    {
        ProcessUserInput();
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
    void ProcessUserInput()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 force = CameraControl.MousePos() - (Vector2)transform.position;
            rb.AddForce(force * rb.mass, ForceMode2D.Impulse);
        }

    }
}
