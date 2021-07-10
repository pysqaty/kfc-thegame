using UnityEngine;

// Generate a screenshot and save to disk with the name SomeLevel.png.

public class Screenshooter : MonoBehaviour
{
    private static int count = 1;
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            ScreenCapture.CaptureScreenshot("./screen" + count++ + ".jpg");
        }
        
    }
}