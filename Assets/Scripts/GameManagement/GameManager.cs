using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public WaveManager waveManager;
	public PortalManager portalManager;
    private WeaponSpawner weaponSpawner;

    public WaveCounterScript waveCounter;
    public WaveIndicatorScript waveIndicator;
    public NextWaveCountdown waveCountdown;
    public ResultsPanelScript resultsScript;

    public TextMeshProUGUI scoreCounter;
    
    public GameObject figtingUI;
    public GameObject resultsUI;
	
    public GameObject crosshairObj;

    public CinemachineFreeLook cinemachineFreeLook;

    private PlayerController playerController;

    public int scorePerWaveCleared = 300;
    
    public static int score = 0;
    private int Score => score;
    
    private bool waitingForNextWave;
    private bool ifGameEnded = false;
    private bool firstWaveSpawned;

    private void Awake()
    {
        score = 0;
        waitingForNextWave = false;
        firstWaveSpawned = false;
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Start()
    {
        waveCounter.UpdateWave(waveManager.CurrentWaveNumber);
		portalManager.ShufflePortalLinks();
        SoundManager.Instance.StopMusic();
        SoundManager.Instance.PlayMusic(SoundManager.MusicType.Background1);

        weaponSpawner = FindObjectOfType<WeaponSpawner>();
    }

    public void ReloadScene()
    {
        if (PauseScript.IsGamePaused)
        {
            Time.timeScale = 1f;
        }   
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        FindObjectOfType<PauseScript>().Resume(false);
        
        SoundManager.Instance.StopMusic();
        SoundManager.Instance.PlayMusic(SoundManager.MusicType.Menu1);
        SceneManager.LoadScene("Menu");
    }
    
    private IEnumerator InitiateNextWaveStart()
    {
        score += scorePerWaveCleared * waveManager.CurrentWaveNumber;
        SoundManager.Instance.StopMusic();
		SoundManager.Instance.PlayMusic(SoundManager.MusicType.Background2);
		yield return waveCountdown.StartCountdown(10f);
        waveManager.StartNextWave();
        waveCounter.UpdateWave(waveManager.CurrentWaveNumber);
        StartCoroutine(waveIndicator.IndicateNewWave());
        this.waitingForNextWave = false;
        firstWaveSpawned = true;
    }
    
    private void ManageWaves()
    {
        if (waveManager.EnemiesRemaining == 0 && !waitingForNextWave)
        {
            if(firstWaveSpawned)
            {
                //We're waiting for next wave, so we have just finished one, spawn a new weapon
                SpawnRandomWeapon();
            }
            this.waitingForNextWave = true;
            StartCoroutine(InitiateNextWaveStart());
        }
    }

    private void SpawnRandomWeapon()
    {
        weaponSpawner.SpawnWeapon();
    }

    private void ManageGameState()
    {
        if (playerController.GetHealthPercentile() <= 0) //fox dies
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            figtingUI.SetActive(false);
            if (!ifGameEnded)
            {
                resultsUI.SetActive(true);
                ifGameEnded = true;
                FindObjectOfType<PauseScript>().Pause(false);
            }
            resultsScript.UpdateResults(Score, waveManager.CurrentWaveNumber);
            crosshairObj.SetActive(false);
            resultsScript.SetScore(Score);
            this.cinemachineFreeLook.enabled = false;
        }
    }
    
    private void Update()
    {
        ManageWaves();
        ManageGameState();
        scoreCounter.text = score.ToString() + " $";
    }
}
