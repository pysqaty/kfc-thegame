using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WaveCounterScript : MonoBehaviour
    {
        public TMP_Text scoreText;

        public void UpdateWave(int wave)
        {
            this.scoreText.text = $"Wave: {wave}";
        }
    }
}
