using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    private SoundManager soundManager;

    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject optionsMenu;
    [SerializeField]
    private Toggle soundToggle;
    [SerializeField]
    private Toggle musicToggle;
    [SerializeField]
    private Slider soundSlider;
    [SerializeField]
    private Slider musicSlider;

    [SerializeField]
    private Slider sensitivitySlider;

    [SerializeField]
    private GameObject mainMenuFirst;
    [SerializeField]
    private GameObject optionsMenuFirst;

    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();

        soundSlider.value = soundManager.SfxVolume;
        musicSlider.value = soundManager.MusicVolume;
        soundToggle.isOn = soundManager.SfxVolume > 0;
        musicToggle.isOn = soundManager.MusicVolume > 0;

        sensitivitySlider.value = CameraSpeedChange.Sensitivity;
    }
    public void Back()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainMenuFirst);
    }
    public void SetSoundLevel()
    {
        soundManager.SfxVolume = soundSlider.value;
        soundToggle.isOn = soundManager.SfxVolume > 0 ? true : false;
    }

    public void SetSensitivity()
    {
        CameraSpeedChange.Sensitivity = sensitivitySlider.value;
    }

    public void TurnOnTurnOffSound()
    {
        if (soundToggle.isOn)
            soundManager.SfxVolume = soundSlider.value;
        else
            soundManager.MuteSfx();
    }
    public void SetMusicLevel()
    {
        soundManager.MusicVolume = musicSlider.value;
        musicToggle.isOn = soundManager.MusicVolume > 0 ? true : false;
    }
    public void TurnOnTurnOffMusic()
    {
        if (musicToggle.isOn)
            soundManager.MusicVolume = musicSlider.value;
        else
            soundManager.MuteMusic();
    }

    private void Update()
    {
        FindObjectOfType<ControllerHandler>().SetDefaultSelected(optionsMenuFirst);

        if (Input.GetButton("Cancel"))
        {
            Back();
        }
    }
}
