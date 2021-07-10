using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class AudioAssets<T> : ScriptableObject
{
    public ClipObj[] clips;

    public Dictionary<T, List<AudioClip>> clipDict;

    [System.Serializable]
    public class ClipObj
    {
        public T type;
        public AudioClip clip;
    }

    public void Init()
    {
        clipDict = new Dictionary<T, List<AudioClip>>();

        var enumVals = Enum.GetValues(typeof(T));
        foreach (T type in enumVals)
        {
            clipDict.Add(type, new List<AudioClip>());
        }

        foreach (ClipObj clipObj in clips)
        {
            (clipDict[clipObj.type]).Add(clipObj.clip);
        }
    }
}

public sealed class SoundManager : MonoBehaviour
{
    #region AudioTypes
    public enum SfxType
    {
        // Player
		PlayerDmgTaken,
		PlayerFootstep,    
		PlayerWeaponChange,    
	
		// Enemies
		ChickDmgDone,    
		FastChickDmgDone,    
		SzulerChickShot,    
	
		// Weapons
		/* single-shots */
		basicGunShot,
		redDeadRevolverShot,
		crossedFingersShot,
		bobbysSniperShot,
		diracsShotgunShot,
		roosterRoasterShot, 
		canonicalLaserShot,
		cowboiShot,		
		/* loops */
		roosterRoasterFiring,    
		canonicalLaserFiring,    
		cowboiFiring,    
		/* inits */  
		cowboiInit,
	
		// Waves
		waveStart,
		betweenWaves,
		waveLast5sec,    
	
		// UI
		menuButtonHover,
		menuButtonClick,
		
		// Misc
		//teleport    
    }

    public enum MusicType
    {
        Background1,
		Background2,
        Background3,
		Background4,
        Menu1,
		Menu2,
    }
    #endregion

	#region General Settings    
	public void PauseAudio()
    {
        //PauseMusic();
        PauseSfx();
    }

    public void UnPauseAudio()
    {
        //UnPauseMusic();
        UnPauseSfx();
    }
	
	public float SfxVolume
	{
		get { return sfxVolume; }
		set 
		{
				if (value >= 0.0f && value <= 1.0f) 
				{
					sfxVolume = value;
					ApplyToAllSfxTracks(track => track.volume = sfxVolume);
				}
		}
	}
	
	public float MusicVolume
	{
		get { return musicVolume; }
		set 
		{
				if (value >= 0.0f && value <= 1.0f) 
				{
					musicVolume = value;
					ApplyToAllMusicTracks(track => track.volume = musicVolume);
				}
		}
	}
	
	public void MuteSfx() 
	{
		SfxVolume = 0.0f;
	}
	
	public void MuteMusic() 
	{
		MusicVolume = 0.0f;
	}
	
	#endregion

    #region Sound Effects
    public void PlaySfx(SfxType sfxType)
    {
        AudioClip clip = GetRandClip(sfxType);
        if (clip == null) {
            return; 
        }
        var track = sfxTracks[sfxType];
        track.clip = clip;
        track.Play();
    }

	public void PlaySfxIfFree(SfxType sfxType)
    {
        AudioClip clip = GetRandClip(sfxType);
        if (clip == null) {
            return; 
        }
        var track = sfxTracks[sfxType];
		if (track.isPlaying == false) {
			track.clip = clip;
			track.Play();
		}
    }

    public void PauseSfx()
    {
        foreach (var track in sfxTracks)
        {
            track.Value.Pause();
        }
    }
    
    public void UnPauseSfx()
    {
        foreach (var track in sfxTracks)
        {
            track.Value.UnPause();
        }
    }
    
    public void PlaySfxShot(SfxType sfxType)
    {
        AudioClip clip = GetRandClip(sfxType);
        if (clip == null) {
            return; 
        }
        sfxMiscTrack.PlayOneShot(clip);
    }

    public void PlaySfxShot(SfxType sfxType, int idx)
    {
        AudioClip clip = GetClip(sfxType, idx);
        if (clip == null) {
            return;
        }
        sfxMiscTrack.PlayOneShot(clip);
    }

    public void PlaySfxShot(AudioClip clip)
    {
        if (clip == null) {
            UnityEngine.Debug.Log("[SoundManager][PlayCustomSfx] null has been passed.");
            return; 
        }

        sfxMiscTrack.PlayOneShot(clip);
    }
    #endregion

    #region Music
    public bool PlayMusic(MusicType type)
    {
        var clip = GetClip(type);
        if (clip == null) { 
            return false;
        }

        
        if (musicTrack1.isPlaying) {
            UnityEngine.Debug.Log("[SoundMan][PlayMusic] musicTrack1 is busy.");
            return false;
        }

        musicTrack1.clip = clip;
        musicTrack1.Play();
        return true;
    }

    public void StopMusic()
    {
        musicTrack1.Stop();
    }

    public void PauseMusic()
    {
        musicTrack1.Pause();
    }
    
    public void UnPauseMusic()
    {
        musicTrack1.UnPause();
    }
    
    public bool IsMusicTrackFree()
    {
        return !musicTrack1.isPlaying;
    }
    #endregion
    
    #region Static Utility Functions
    static void ForeachEnumVal<T>(Action<T> lambda) where T : struct, IConvertible
    {
        var enumVals = Enum.GetValues(typeof(T));
        foreach (T type in enumVals)
        {
            lambda(type);
        }
    }
    #endregion

    #region Singleton & Initilisation
    private SoundManager() { }

    private static SoundManager instance;

    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();
                if (instance == null)
                {
                    instance = new GameObject("NewSoundManagerInstance", typeof(SoundManager)).GetComponent<SoundManager>(); 
                }
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        sfxMiscTrack = this.gameObject.AddComponent<AudioSource>();
        sfxMiscTrack.loop = false;
        musicTrack1 = this.gameObject.AddComponent<AudioSource>();
        musicTrack1.loop = true;

        sfxTracks = new Dictionary<SfxType, AudioSource>();
        ForeachEnumVal((SfxType type) =>
        {
            var audioSrc = this.gameObject.AddComponent<AudioSource>();
            audioSrc.loop = false;
            sfxTracks.Add(type, audioSrc);
        });

        musicAssets.Init();
        sfxAssets.Init();

        randGen = new System.Random();
    
		sfxVolume   = 1.0f;
		musicVolume = 1.0f;
	}
    #endregion

    #region Private Functions
    private AudioClip GetClip(MusicType type, int idx = 0)
    {
        return GetClip<MusicType>(musicAssets, type, idx);
    }

    private AudioClip GetClip(SfxType type, int idx = 0)
    {
        return GetClip<SfxType>(sfxAssets, type, idx);
    }

    private AudioClip GetClip<T>(AudioAssets<T> audioAssets, T type, int idx = 0)
    {
        var list = audioAssets.clipDict[type];
        if (list.Count< 1)
        {
            UnityEngine.Debug.Log("[SoundMan] AudioAssets<T> didnt contain any clips of type: " + type);
            return null;
        }
        return list[idx];
    }

    private AudioClip GetRandClip(MusicType type)
    {
        var list = musicAssets.clipDict[type];
        if (list.Count < 1)
        {
            UnityEngine.Debug.Log("[SoundMan] AudioAssets<T> didnt contain any clips of type: " + type);
            return null;
        }
        var idx = randGen.Next(0, list.Count);
        return list[idx];
    }

    private AudioClip GetRandClip(SfxType type)
    {
        var list = sfxAssets.clipDict[type];
        if (list.Count < 1)
        {
            UnityEngine.Debug.Log("[SoundMan] AudioAssets<T> didnt contain any clips of type: " + type);
            return null;
        }
        var idx = randGen.Next(0, list.Count);
        return list[idx];
    }
    
	private void ApplyToAllSfxTracks(Action<AudioSource> func) 
	{
		func(sfxMiscTrack);
		
		foreach (var track in sfxTracks)
        {
            func(track.Value);
        }
	}
	
	private void ApplyToAllMusicTracks(Action<AudioSource> func) 
	{
		func(musicTrack1);
	}
	#endregion

    #region Variables
    // Settings
	private float sfxVolume;
	private float musicVolume;
	
	// Audio Tracks
    private AudioSource sfxMiscTrack;
    private Dictionary<SfxType, AudioSource> sfxTracks;
    private AudioSource musicTrack1;

    // Audio Data
    public SfxAssetsScriptable sfxAssets;
    public MusicAssetsScriptable musicAssets;

    // Systems
    System.Random randGen;
    #endregion
}