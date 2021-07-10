using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPortalTraveller : PortalTraveller
{
    NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public override void Teleport(Transform startPortal, Transform dstPortal, Vector3 pos, Quaternion rot)
    {
        //navMeshAgent.updatePosition = false;
        GetComponent<Rigidbody>().isKinematic = false;

        //transform.position = pos;

        navMeshAgent.Warp(pos);
        //GetComponent<Rigidbody>().isKinematic = true;
        //navMeshAgent.updatePosition = true;
    }
}


