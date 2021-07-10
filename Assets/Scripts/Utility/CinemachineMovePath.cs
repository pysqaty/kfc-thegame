using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineMovePath : MonoBehaviour
{
    public float Speed;
    public int Max;

    private CinemachineTrackedDolly dolly;

    // Start is called before the first frame update
    void Start()
    {
        dolly = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTrackedDolly>();
    }

    // Update is called once per frame
    void Update()
    {
        dolly.m_PathPosition += Speed * Time.deltaTime;
        if(dolly.m_PathPosition > Max)
        {
            dolly.m_PathPosition -= Max;
        }
    }
}
