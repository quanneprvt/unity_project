using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public float moveSpeed = 50f;
    //
    float hMove = 0f;
    bool isJump = false;
    bool isCrouch = false;
    //
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
        //
        if (Input.GetButtonDown("Jump"))
            isJump = true;
        //
        if (Input.GetButtonDown("Crouch"))
            isCrouch = true;
        else if (Input.GetButtonUp("Crouch"))
            isCrouch = false;
        //
    }

    void FixedUpdate()
    {
        controller.Move(hMove * Time.fixedDeltaTime, isCrouch, isJump);
        if (isJump)
            isJump = false;
    }
}
