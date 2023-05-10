using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelSerializer : MonoBehaviour
{

    [MenuItem("Assets/Save Current Level", priority = 100)]
    static void SaveLevel()
    {
        List<UnityEngine.Object> objectsInScene = new List<UnityEngine.Object>();

        foreach (UnityEngine.Object go in Resources.FindObjectsOfTypeAll(typeof(UnityEngine.Object)) as UnityEngine.Object[])
        {
            GameObject cGO = go as GameObject;
            if (cGO != null && !EditorUtility.IsPersistent(cGO.transform.root.gameObject) && !(go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave))
                Debug.Log(go, go);
        }
       
    }
    void LoadLevel()
    {

    }
}
