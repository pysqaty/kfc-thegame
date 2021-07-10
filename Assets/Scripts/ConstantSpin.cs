﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantSpin : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}
