using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject optionsMenu;
    [SerializeField]
    private GameObject highscoresMenu;

    [SerializeField]
    private GameObject mainMenuFirst;
    [SerializeField]
    private GameObject optionsMenuFirst;
    [SerializeField]
    private GameObject highscoresMenuFirst;

    private void Update()
    {
        FindObjectOfType<ControllerHandler>().SetDefaultSelected(mainMenuFirst);
    }

    private void SetSelected(GameObject go)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(go);
    }

    private void Start()
    {
        SoundManager.Instance.PlayMusic(SoundManager.MusicType.Menu1);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Map");
    }

    public void Options()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
        SetSelected(optionsMenuFirst);
    }

    public void Highscores()
    {
        mainMenu.SetActive(false);
        highscoresMenu.SetActive(true);
        SetSelected(highscoresMenuFirst);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
