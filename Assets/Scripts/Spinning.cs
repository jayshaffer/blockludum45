﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning : MonoBehaviour
{
    public float speed = 1;
    void Start()
    {
        
    }

    void Update()
    {
       transform.Rotate(new Vector3(0, 0, speed)); 
    }
}
