using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave Saw Difficulty Function", menuName = "KFC/Wave Difficulty/Saw/Function")]
public class WaveSawDifficultyFunction : WaveDifficultyFunction<WaveSawDifficultySettings>
{
    //https://www.desmos.com/calculator/83udtsxfb3
    public override float GetDifficultyForWave(int waveNumber)
    {
        var settings = DifficultySettings[CurrentDifficulty];

        float n = settings.PeakHeight / Mathf.Pow(settings.PeakPeriod, settings.PeakExponent);
        float m(float x) => x % settings.PeakPeriod;
        float s(float x) => n * Mathf.Pow(x, settings.PeakExponent);
        float t(float x) => settings.DifficultyFactor * Mathf.Pow(x, settings.DifficultyExponent);
        float f(float x) => s(m(x)) + t(x) + settings.BaseDanger;

        return f(waveNumber);
    }
}
