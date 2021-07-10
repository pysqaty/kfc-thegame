using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public static bool IsGamePaused = false;
    public static PauseScript Instance { get; private set; }

    public GameObject pauseMenuUI;
    public GameObject crosshairObj;
    public GameObject weaponPanel;

    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject pauseMenuFirst;
    [SerializeField]
    private GameObject optionsMenu;
    [SerializeField]
    private GameObject optionsMenuFirst;

    private void Start()
    {
        SoundManager sm = FindObjectOfType<SoundManager>();
        if(sm == null)
        {
            SoundManager soundManager = FindObjectOfType<SoundManager>(true);
            if (soundManager.gameObject.activeInHierarchy == false)
            {
                soundManager.gameObject.SetActive(true);
            }
        }

        Resume();
    }

    // Update is called once per frame
    void Update()
    {
        Instance = this;
        if (Input.GetButtonDown("Pause"))
        {
            if(IsGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();

                pauseMenu.SetActive(true);
                optionsMenu.SetActive(false);
                SetSelected(pauseMenuFirst);
            }
        }

        if (FindObjectOfType<ControllerHandler>().CurrentDevice == ControllerHandler.Device.MouseAndKeyboard)
        { 
            if(IsGamePaused)
            {
                Cursor.visible = true;
            }
        }
        else
        {
            Cursor.visible = false;
        }
    }

    public void Resume(bool hidePanel = true)
    {
        Time.timeScale = 1f;
        IsGamePaused = false;
        SoundManager.Instance.UnPauseAudio();
    
        if(hidePanel)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            pauseMenuUI.SetActive(false);
            crosshairObj.SetActive(true);
            weaponPanel.SetActive(false);
        }
	}

    public void Pause(bool showMenu = true)
    {
        if(showMenu)
        {
            pauseMenuUI.SetActive(true);
        }
        Time.timeScale = 0f;
        IsGamePaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SoundManager.Instance.PauseAudio();
    
		crosshairObj.SetActive(false);
	}

    public void MainMenu()
    {
        FindObjectOfType<GameManager>().GoToMainMenu();
    }

    public void Options()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
        SetSelected(optionsMenuFirst);
    }

    private void SetSelected(GameObject go)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(go);
    }
}
