using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject First;

    // Update is called once per frame
    void Update()
    {
        FindObjectOfType<ControllerHandler>().SetDefaultSelected(First);
    }
}
