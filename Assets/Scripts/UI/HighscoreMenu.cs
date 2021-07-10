using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class HighscoreMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject highscoresMenu;
    [SerializeField]
    private Transform entryContainer;
    [SerializeField]
    private Transform entryTemplate;

    [SerializeField]
    private GameObject mainMenuFirst;
    [SerializeField]
    private GameObject highscoreMenuFirst;

    private List<Transform> highscoreEntryTransformList;
    private static string prefsString = "highscoreTable";
    private static int maxHighscores = 10;
    private Highscores highscores;
    private void Awake()
    {
        entryTemplate.gameObject.SetActive(false);
        
        string jsonString = PlayerPrefs.GetString(prefsString);
        highscores = JsonUtility.FromJson<Highscores>(jsonString);
        if (highscores == null)
        {
            highscores = new Highscores();
        }
        while(highscores.highscoreEntries.Count < 10)
        {
            highscores.highscoreEntries.Add(new HighscoreEntry() { name = "PLAYER", score = 0 });
        }
        highscores.highscoreEntries.Sort((x, y) => x.score.CompareTo(y.score));
        highscores.highscoreEntries.Reverse();


        highscoreEntryTransformList = new List<Transform>();
        for(int i = 0; i < maxHighscores; i++)
        {
            CreateHighscoreEntryTransform(highscores.highscoreEntries[i], entryContainer, highscoreEntryTransformList);
        }

    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        Transform entryTransform = Instantiate(entryTemplate, container);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;

        entryTransform.Find("PositionText").GetComponent<Text>().text = rank.ToString();
        entryTransform.Find("ScoreText").GetComponent<Text>().text = highscoreEntry.score.ToString();
        entryTransform.Find("NameText").GetComponent<Text>().text = highscoreEntry.name;

        transformList.Add(entryTransform);
    }
    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntries;
        public Highscores()
        {
            highscoreEntries = new List<HighscoreEntry>();
        }
    }

    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;
    }

    public void AddHighscoreEntry(int score, string name)
    {
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };

        string jsonString = PlayerPrefs.GetString(prefsString);
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if(highscores == null)
        {
            highscores = new Highscores();
        }

        highscores.highscoreEntries.Add(highscoreEntry);

        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString(prefsString, json);
        PlayerPrefs.Save();
    }

    public void Back()
    {
        highscoresMenu.SetActive(false);
        mainMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainMenuFirst);
    }
    public void MainMenu()
    {
        FindObjectOfType<GameManager>().GoToMainMenu();
    }

    private void Update()
    {
        FindObjectOfType<ControllerHandler>().SetDefaultSelected(highscoreMenuFirst);

        if (Input.GetButton("Cancel"))
        {
            Back();
        }
    }
}
