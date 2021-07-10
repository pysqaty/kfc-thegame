using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Slider healthBarSlider;
    public PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        UpdateHealthBar();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        healthBarSlider.value = (int)player.GetHealthPercentile();
    }
}
