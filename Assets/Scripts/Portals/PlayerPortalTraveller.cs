using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPortalTraveller : PortalTraveller
{
    private Cinemachine.CinemachineFreeLook freelook;
    private CharacterController characterController;

    private void Start()
    {
        freelook = FindObjectOfType<Cinemachine.CinemachineFreeLook>();
        characterController = GetComponent<CharacterController>();
    }
    public override void Teleport(Transform startPortal, Transform dstPortal, Vector3 pos, Quaternion rot)
    {
        Debug.Log($"Teleporting from {startPortal.position} to {pos}, {rot}");

        characterController.enabled = false;

        freelook.m_XAxis.Value = rot.eulerAngles.y;

        transform.SetPositionAndRotation(pos, rot);

        characterController.enabled = true;
    }
}
