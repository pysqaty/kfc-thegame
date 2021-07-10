using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WaveDifficultyFunction : ScriptableObject
{
    public abstract float GetDifficultyForWave(int waveNumber);
}

public abstract class WaveDifficultyFunction<T> : WaveDifficultyFunction where T : ScriptableObject
{
    public T[] DifficultySettings;
    public int CurrentDifficulty;
}
