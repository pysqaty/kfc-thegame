using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ScoreCounterScript : MonoBehaviour
    {
        public Text scoreText;

        public void UpdateScore(int score)
        {
            this.scoreText.text = $"Score: {score}";
        }
    }
}
