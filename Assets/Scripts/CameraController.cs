using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    private Vector3 offset;
    private Vector3 pos;

    // Use this for initialization
    void Start()
    {
        // offset = transform.position - Player.transform.position;
        pos =  transform.position;
    }

    //Update each frame
    void Update()
    {
        // offset.x = Player.transform.position.x - transform.position.x;
        //
        // pos = transform.position + 3* offset * Time.deltaTime;
        pos.x = Math.Max(1, transform.position.x + 3 * (Player.transform.position.x - transform.position.x) * Time.deltaTime);
        //
        transform.position = pos;
    }

    // LateUpdate is called after Update each frame
    // void LateUpdate()
    // {
    //     transform.position = transform.position + 3* offset * Time.fixedDeltaTime;
    // }
}
