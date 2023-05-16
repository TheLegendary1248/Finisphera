using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    int unregisterdKeyChainHits = 0;
    void Awake()
    {
        
    }
    
    void Update()
    {
        GetInput();    
    }
    void GetInput()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameManager.Inputs["Submit"]();
        }
    }
}
