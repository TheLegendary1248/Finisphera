using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    
    public GameObject fx;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
            GameManager.WinCondition();
        Debug.Log(collision);
    }
    // Update is called once per frame
    void RegisterComplete()
    {
        
    }
}
