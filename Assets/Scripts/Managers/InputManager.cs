using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    int unregisterdKeyChainHits;
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
        if(Input.GetKeyDown(KeyCode.R))
        {
            GameManager.Inputs["Reset"]();
        }
    }
}
