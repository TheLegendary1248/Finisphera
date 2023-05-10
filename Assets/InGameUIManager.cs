using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InGameUIManager : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        timestamp = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        TimeFormatted();
    }

    float timestamp;
    public TMPro.TextMeshProUGUI timeElaspedText;
    void TimeFormatted()
    {
        if(timeElaspedText)
        {
            var t = System.TimeSpan.FromSeconds(Time.time - timestamp);
            timeElaspedText.text = t.ToString(@"mm\:ss\'fff");
        }
    }
}
