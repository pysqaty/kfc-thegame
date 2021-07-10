using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTest : MonoBehaviour
{
    public GameObject Chicken1;
    public GameObject Chicken2;

    private TeleportEffect te1;
    private TeleportEffect te2;
    void Start()
    {
        te1 = Chicken1.GetComponentInChildren<TeleportEffect>();
        te2 = Chicken2.GetComponentInChildren<TeleportEffect>();
    }

    public void Disappear()
    {
        te1.Disappear();
        te2.Appear();
    }

    public void Appear()
    {
        te1.Appear();
        te2.Disappear();
    }
}
