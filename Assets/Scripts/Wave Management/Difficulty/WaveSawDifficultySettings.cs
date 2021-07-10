using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave Saw Difficulty Settings", menuName = "KFC/Wave Difficulty/Saw/Settings")]
public class WaveSawDifficultySettings : ScriptableObject
{
    public int PeakPeriod;
    public float PeakHeight;
    public float DifficultyExponent;
    public float DifficultyFactor;
    public float PeakExponent;
    public float BaseDanger;
}
