using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float lerp = 0.05f;
    public ObjectGravity ballRef;

    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float angle = transform.eulerAngles.z;
        Vector2 refGrav = ballRef.gravity;
        transform.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(angle, Mathf.Atan2(refGrav.y, refGrav.x) * Mathf.Rad2Deg + 90f, Time.deltaTime * lerp));
    }
}
