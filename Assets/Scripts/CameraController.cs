﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    private Vector3 offset;
    private Vector3 pos;
    float maxX = 0f;
    float maxY = 0f;

    // Use this for initialization
    void Start()
    {
        // offset = transform.position - Player.transform.position;
        pos =  transform.position;
        offset = transform.position - Player.transform.position;
        //
        maxX = 1;
        maxY = transform.position.y - 0.5f;
    }

    //Update each frame
    void Update()
    {
        // offset.x = Player.transform.position.x - transform.position.x;
        //
        // pos = transform.position + 3* offset * Time.deltaTime;
        pos.x = Math.Max(maxX, transform.position.x + 3 * (Player.transform.position.x - transform.position.x) * Time.deltaTime);
        pos.y = Math.Min(maxY , transform.position.y + 5 * (Player.transform.position.y - (transform.position.y - offset.y - 0.5f)) * Time.deltaTime);
        //
        transform.position = pos;
    }

    // LateUpdate is called after Update each frame
    // void LateUpdate()
    // {
    //     transform.position = transform.position + 3* offset * Time.fixedDeltaTime;
    // }
}
