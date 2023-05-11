using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
public class GravityModifier : MonoBehaviour
{
    public enum GravityNormalType
    {
        /// <summary>
        /// Gravity is constant, equal to the surface normal
        /// </summary>
        Surface,
        /// <summary>
        /// Gravity is variable, equal to the object normal
        /// </summary>
        Object,
        /// <summary>
        /// Gravity is constant, equal to the surface normal parallel
        /// </summary>
        Parallel,
        /// <summary>
        /// Gravity is constant, equal to the previous gravity normal parallel
        /// </summary>
        Rotate
    }
    public GravityNormalType type;
    public bool isTowards;
    // Start is called before the first frame update
    void Start()
    {
        SetColor();
    }
    void SetColor()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        int colCount = collision.contactCount;
        for (int i = 0; i < colCount; i++)
        {
            ContactPoint2D contact = collision.GetContact(i);
            Collider2D collider = contact.collider;
            ObjectGravity otherObj;
            if (otherObj = collider.GetComponent<ObjectGravity>());
            {
                float flip = (isTowards ? 1 : -1);
                switch (type)
                {
                    case GravityNormalType.Surface:
                        otherObj.SetGravity(contact.normal * otherObj.scale * flip);
                        break;
                    case GravityNormalType.Object:
                        otherObj.SetAttractor(gameObject);
                        break;
                    case GravityNormalType.Parallel:
                        otherObj.SetGravity(new Vector2(contact.normal.y, -contact.normal.x) * flip * otherObj.scale);
                        break;
                    case GravityNormalType.Rotate:
                        otherObj.SetGravity(new Vector2(otherObj.gravity.y, -otherObj.gravity.x) * flip);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

/// <summary>
/// This an asset for customizing the materials each 
/// </summary>
[CreateAssetMenu(fileName = "Settings", menuName ="Settings/Core Graphic Settings")]
public class SO_MaterialSettings : ScriptableObject
{
    public Material[] materials;
    public UnityEngine.Color[] colors;
}
