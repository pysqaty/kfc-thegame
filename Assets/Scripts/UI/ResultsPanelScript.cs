using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ResultsPanelScript : MonoBehaviour
    {
        public Text resultsText;

        [SerializeField]
        private GameObject highscoresMenu;
        [SerializeField]
        private GameObject resultMenu;
        [SerializeField]
        private InputField nameInput; 
        [SerializeField]
        private HighscoreMenu highscoreMenuScript;
        private int score = 0;
        public void Start()
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void UpdateResults(int score, int wave)
        {
            this.resultsText.text = $"Final score: {score}\nFinal wave: {wave}";
        }
        public void SetScore(int score)
        {
            this.score = score;
        }
        public void Continue()
        {
            string name = nameInput.text.Length == 0 ? "ANON" : nameInput.text;
            highscoreMenuScript.AddHighscoreEntry(score, name);
            resultMenu.SetActive(false);
            highscoresMenu.SetActive(true);
        }
    }
}