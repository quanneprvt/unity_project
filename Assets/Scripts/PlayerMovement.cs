using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public float moveSpeed = 50f;
    public Animator animator;
    //
    float hMove = 0f;
    bool isJump = false;
    bool isCrouch = false;
    bool isGrounded = false;
    Rigidbody2D r;
    Vector2 v;
    //
    private Vector3 savedStat;
    // Start is called before the first frame update
    void Start()
    {
        savedStat = transform.position;
        r = controller.GetRigid();
        isGrounded = controller.IsLanded();
    }

    // Update is called once per frame
    void Update()
    {
        hMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
        r = controller.GetRigid();
        isGrounded = controller.IsLanded();
        //
        animator.SetBool("Grounded", isGrounded);
        animator.SetFloat("Move Speed", Math.Abs(hMove));
        animator.SetFloat("Verticle Velocity", r.velocity.y);
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
        //
        if (isJump)
            isJump = false;
    }

    void Reset()
    {
        transform.position = savedStat;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // Debug.Log(col.gameObject.name);
        if (col.gameObject.tag == "dead")
        {
            this.Reset();
        }
    }
}
