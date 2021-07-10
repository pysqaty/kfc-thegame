using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControllerHandler : MonoBehaviour
{
    public enum Device
    {
        Joystick, MouseAndKeyboard
    }

    public Device CurrentDevice { get; private set; }

    Vector2? prevMousePos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsMouseUsed()) CurrentDevice = Device.MouseAndKeyboard;
        if (IsJoystickUsed()) CurrentDevice = Device.Joystick;
    }

    private bool IsJoystickUsed()
    {
        KeyCode[] joystickKeyCodes = new KeyCode[]
        {
            KeyCode.Joystick1Button0,
            KeyCode.Joystick1Button1,
            KeyCode.Joystick1Button2,
            KeyCode.Joystick1Button3,
            KeyCode.Joystick1Button4,
            KeyCode.Joystick1Button5,
            KeyCode.Joystick1Button6,
            KeyCode.Joystick1Button7,
            KeyCode.Joystick1Button8,
            KeyCode.Joystick1Button9,
            KeyCode.Joystick1Button10,
            KeyCode.Joystick1Button11,
            KeyCode.Joystick1Button12,
            KeyCode.Joystick1Button13,
            KeyCode.Joystick1Button14,
            KeyCode.Joystick1Button15,
            KeyCode.Joystick1Button16,
            KeyCode.Joystick1Button17,
            KeyCode.Joystick1Button18,
            KeyCode.Joystick1Button19,
        };

        if (Input.GetAxis("Joystick") != 0)
        {
            return true;
        }

        foreach (var kc in joystickKeyCodes)
        {
            if(Input.GetKeyDown(kc))
            {
                return true;
            }
        }

        return false;
    }

    private bool IsMouseUsed()
    {
        Vector2 currMousePos = Input.mousePosition;
        bool ret = false;

        if(prevMousePos.HasValue && currMousePos != prevMousePos.Value)
        {
            ret = true;
        }
        prevMousePos = currMousePos;
        return ret;
    }

    public void SetDefaultSelected(GameObject first)
    {
        if (CurrentDevice == ControllerHandler.Device.Joystick)
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(first);
            }
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}
