using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class NextWaveCountdown : MonoBehaviour
    {
        public TMP_Text countdownText;

        private void Awake()
        {
            countdownText.gameObject.SetActive(false);
        }

        public void SetText(float currentSeconds)
        {
            this.countdownText.text = $"Time to next wave: {currentSeconds:0.00} s";
        }
        
        public IEnumerator StartCountdown(float duration)
        {
            SetText(duration);
            countdownText.gameObject.SetActive(true);
            while (duration > 0f)
            {
                yield return null;
                duration -= Time.deltaTime;
                SetText(duration < 0f ? 0f: duration);
            }
            countdownText.gameObject.SetActive(false);
        }
    }
}
