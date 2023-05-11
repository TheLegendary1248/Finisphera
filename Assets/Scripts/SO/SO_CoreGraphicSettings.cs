using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This an asset for customizing the materials each 
/// </summary>
[CreateAssetMenu(fileName = "Settings", menuName = "Settings/Core Graphic Settings")]
public class SO_CoreGraphicSettings : ScriptableObject
{
    public Material[] materials;
    public Color[] colors;
}
