using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpeedChange : MonoBehaviour
{
    public static float Sensitivity = 1;

    private CinemachineFreeLook fl;
    private float baseSpeedX;
    private float baseSpeedY;

    // Start is called before the first frame update
    void Start()
    {
        fl = GetComponentInChildren<CinemachineFreeLook>();
        baseSpeedX = fl.m_XAxis.m_MaxSpeed;
        baseSpeedY = fl.m_YAxis.m_MaxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        fl.m_XAxis.m_MaxSpeed = baseSpeedX * Sensitivity;
        fl.m_YAxis.m_MaxSpeed = baseSpeedY * Sensitivity;
    }
}
