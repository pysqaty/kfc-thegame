using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShadowsDisable : MonoBehaviour
{
    private float oldShadowsDist;

    private void OnPreRender()
    {
        oldShadowsDist = QualitySettings.shadowDistance;
        QualitySettings.shadowDistance = 0;
    }

    private void OnPostRender()
    {
        QualitySettings.shadowDistance = oldShadowsDist;
    }

}
