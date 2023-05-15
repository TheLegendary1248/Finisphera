using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class is in charge of the whole Canvas. Its name is Mainuil. 
/// it's a terrible pun, i know
/// </summary>
public class MainUIManager : MonoBehaviour
{
    
    GameObject[] subViews;
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
/// <summary>
/// Interface that should be inherited by scripts managing the other UI views
/// </summary>
public interface UISubViewManager
{
    public void SelfDisable();
    public void SelfEnable();
}