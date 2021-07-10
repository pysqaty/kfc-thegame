using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradePanelScript : MonoBehaviour
{
    [SerializeField]
    private GameObject weaponPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseScript.IsGamePaused && (weaponPanel.activeInHierarchy == false))
        {
            return;
        }

        var first = weaponPanel.GetComponentsInChildren<Selectable>().Where(i => i.interactable).FirstOrDefault();
        if (first != null)
        {
            FindObjectOfType<ControllerHandler>().SetDefaultSelected(first.gameObject);
        }

        if(Input.GetButtonDown("Upgrade Menu"))
        {
            weaponPanel.SetActive(!weaponPanel.activeInHierarchy);
            if(weaponPanel.activeInHierarchy)
            {
                PauseScript.Instance.Pause(false);
                EventSystem.current.SetSelectedGameObject(null);
                if(first != null)
                {
                    EventSystem.current.SetSelectedGameObject(first.gameObject);
                }
            }
            else
            {
                PauseScript.Instance.Resume();
            }
        }
    }
}
