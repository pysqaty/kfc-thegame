using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitchingUIScript : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public TMP_Text previousWeapon;
    public TMP_Text currentWeapon;
    public TMP_Text nextWeapon;

    public float timeTillFadeOut;
    public float fadeOutTime;

    private IEnumerator StartDelayedFadeOut() 
    {
        yield return new WaitForSeconds(timeTillFadeOut);

        float startingTime = Time.time;
        while (fadeOutTime > Time.time - startingTime)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, (Time.time - startingTime) / fadeOutTime);
            yield return null;
        }
        yield return null;
    }
    
    public void ShowUI(string previousWeapon, string currentWeapon, string nextWeapon)
    {
        this.previousWeapon.text = previousWeapon;
        this.currentWeapon.text = currentWeapon;
        this.nextWeapon.text = nextWeapon;

        StopAllCoroutines();

        canvasGroup.alpha = 1f;

        StartCoroutine(StartDelayedFadeOut());
    }

    private void Awake()
    {
        canvasGroup.alpha = 0f;
    }
    
}
