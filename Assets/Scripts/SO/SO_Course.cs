using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="Block Type", menuName ="Block Type")]
public class SO_Course : ScriptableObject
{
    public float SpecialTime;
    public float BPM;
    public AnimationCurve Ranking;
    public AnimationCurve SpecialRanking;
    public AudioClip mainMusic;
    public AudioClip specialMusic;
    public GameObject[] levels;
    public GameObject[] specialLevels;
}
