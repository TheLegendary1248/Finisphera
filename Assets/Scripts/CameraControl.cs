using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float lerp = 0.05f;

    // Update is called once per frame
    void Update()
    {
        if(BallScript.self.gameObject)
        {
            float angle = transform.eulerAngles.z;
            Vector2 refGrav = BallScript.self.objGrav.gravity;
            transform.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(angle, Mathf.Atan2(refGrav.y, refGrav.x) * Mathf.Rad2Deg + 90f, Time.deltaTime * lerp));
            transform.position = BallScript.self.transform.position;
        }
        
    }
    public static Vector2 MousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    public static Vector2 MousePos(Camera c)
    {
        return c.ScreenToWorldPoint(Input.mousePosition);
    }
}
