using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InGameUIManager : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        TimeFormatted();
    }

    public TMPro.TextMeshProUGUI courseTimeElaspedText;
    public TMPro.TextMeshProUGUI levelTimeElaspedText;
    void TimeFormatted()
    {
        if(levelTimeElaspedText)
        {
            var t = System.TimeSpan.FromSeconds(GameManager.timeSinceLevelStart);
            levelTimeElaspedText.text = t.ToString(@"mm\:ss\'fff");
        }
        if (courseTimeElaspedText)
        {
            var t = System.TimeSpan.FromSeconds(GameManager.timeSinceCourseStart);
            courseTimeElaspedText.text = t.ToString(@"mm\:ss\'fff");
        }
    }
}
