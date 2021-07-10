using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    TMPro.TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
    }
    // Update is called once per frame
    void Update()
    {
        text.SetText(((int)(1f / Time.unscaledDeltaTime)).ToString());
    }
}
