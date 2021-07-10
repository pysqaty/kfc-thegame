using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WaveIndicatorScript : MonoBehaviour
    {
        public TMP_Text indicatorText;

        public float fadeInTime;
        public float stayTime;
        public float fadeOutTime;
        
        public void SetTextAlpha(float alpha)
        {
            indicatorText.color = new Color(indicatorText.color.r,
                                            indicatorText.color.g, 
                                            indicatorText.color.b,
                                            alpha);
        }

        public IEnumerator FadeIn(float duration)
        {
            SetTextAlpha(0f);
            while (indicatorText.color.a < 1.0f)
            {
                yield return null;
                float newAlpha = indicatorText.color.a + (Time.deltaTime / duration);
                SetTextAlpha(newAlpha > 1f ? 1f: newAlpha);
            }
        }
 
        public IEnumerator FadeOut(float duration)
        {
            SetTextAlpha(1f);
            while (indicatorText.color.a > 0f)
            {
                yield return null;
                float newAlpha = indicatorText.color.a - (Time.deltaTime / duration);
                SetTextAlpha(newAlpha < 0f ? 0f : newAlpha);
            }
        }
        
        public IEnumerator IndicateNewWave()
        {
            yield return FadeIn(fadeInTime);
            yield return new WaitForSeconds(stayTime);
            yield return FadeOut(fadeOutTime);
        }
    }
}
