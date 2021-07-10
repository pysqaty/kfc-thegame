using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class WaveManager : MonoBehaviour
{
    public WaveDifficultyFunction DifficultyFunction;
    [HideInInspector]
    public List<WaveEnemyType> WaveEnemyTypes = new List<WaveEnemyType>();
    public int MaxEnemyTypesPerWave = 2;

    public List<Transform> spawnPoints;
    
    public static int enemiesRemaining;
    public int EnemiesRemaining => enemiesRemaining;
    public int CurrentWaveNumber { get; private set; }
    public float TimeFromBeginning { get; private set; }
    public bool WaveInProgress { get; private set; }

    private List<WaveEnemyType> PossibleEnemies => WaveEnemyTypes.Where(e => e.MinimalWaveNumber <= CurrentWaveNumber).ToList();

    // Start is called before the first frame update
    void Start()
    {
        enemiesRemaining = 0;
        CurrentWaveNumber = 0;
        TimeFromBeginning = 0;
        WaveInProgress = false;
    }

    public void StartNextWave()
    {       
		++CurrentWaveNumber;
        float dangerValue = DifficultyFunction.GetDifficultyForWave(CurrentWaveNumber);
        Debug.Log($"Started wave {CurrentWaveNumber}. Danger level: {dangerValue}");
        SpawnEnemies(dangerValue);

        TimeFromBeginning = 0;
        WaveInProgress = true;
    
        SoundManager.Instance.StopMusic();
		SoundManager.Instance.PlayMusic(SoundManager.MusicType.Background1);
		SoundManager.Instance.PlaySfx(SoundManager.SfxType.waveStart);
	}

    private List<float> GetRandomSplit(int count)
    {
        List<float> percentages = new List<float>();
        for (int i = 0; i < count; i++)
        {
            percentages.Add(1 + Random.Range(0f, 1f));
        }
        float sum = percentages.Sum();
        for (int i = 0; i < percentages.Count; i++)
        {
            percentages[i] /= sum;
        }

        return percentages;
    }

    private void SpawnEnemies(float dangerLevel)
    {
        var possibleEnemies = PossibleEnemies;
        if(possibleEnemies.Count > MaxEnemyTypesPerWave)
        {
            possibleEnemies = possibleEnemies.OrderBy(e => System.Guid.NewGuid()).Take(MaxEnemyTypesPerWave).ToList();
        }

        var percentages = GetRandomSplit(possibleEnemies.Count);
        for (int i = 0; i < possibleEnemies.Count; i++)
        {
            Spawn(possibleEnemies[i], Mathf.CeilToInt(percentages[i] * (dangerLevel / possibleEnemies[i].DangerLevel)));
        }
    }

    private void Spawn(WaveEnemyType enemyType, int count)
    {
        Debug.Log($"Spawned {count} {enemyType.Name}s");
        for (int i = 0; i < count; i++)
        {
            Instantiate(enemyType.Prefab, spawnPoints[Random.Range(0, spawnPoints.Count)]);
            enemiesRemaining++;
        }
    }
}
