using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Graphic.Math;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D m_RigidBody;
    [SerializeField] private PathMgr m_PathMgr;
    [SerializeField] private float m_MoveSpeed = 0f;
    [SerializeField] private int m_Type = 0;
    [SerializeField] private bool m_IsLoopFromBegin = false;
    [SerializeField] private float m_JumpForce = 600f;
    [SerializeField] private GameObject m_Target;
    private Vector3 m_Delta;
    private float m_DeltaBezier;
    private Vector2 m_Destination;
    Camera m_CameraMain;
    //
    [HideInInspector] public bool isGrounded = false;
    //
    void Awake()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // m_RigidBody = GetComponent<Rigidbody2D>();
        isGrounded = (m_RigidBody.velocity.y <= 0.2 && m_RigidBody.velocity.y >= -0.2) ? true : false;
        m_CameraMain = Camera.main;
        // m_RigidBody.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = (m_RigidBody.velocity.y <= 0.2 && m_RigidBody.velocity.y >= -0.2) ? true : false;
    }

    void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;
        _UpdatePath(dt);
        _UpdateDeltaTime(dt);
        _UpdatePosition(dt);
        _UpdateRotation(dt);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "pipe") 
        {
            // Debug.Log("ignore");
            Physics2D.IgnoreCollision(col.collider, GetComponent<BoxCollider2D>());
        }
        if (col.gameObject.tag == "dead" && m_Type != 2)
        {
            Physics2D.IgnoreCollision(col.collider, GetComponent<BoxCollider2D>());
        }
    }

    private void _UpdatePath(float dt)
    {
        if (m_PathMgr.isCompletePath)
        {
            _CompleteMoveHandler();
        }
    }

    private void _UpdateDeltaTime(float dt)
    {
        switch (m_Type)
        {
            case 0:
                if (isGrounded)
                // m_DeltaX = Math.Min(1, m_DeltaX += dt);
                    m_Delta.x += m_MoveSpeed*dt;
            break;

            case 1:
                m_DeltaBezier = Math.Min(1, m_DeltaBezier += m_MoveSpeed*dt);
            break;
        }
    }

    private void _UpdatePosition(float dt)
    {
        switch (m_Type)
        {
            case 0:
                m_Destination.x = m_PathMgr.GetTargetPosition(m_Delta.x).x;
                m_Destination.y = transform.position.y;
                transform.position = m_Destination;
            break;

            case 1:
                m_Destination = m_PathMgr.GetTargetPosition(m_DeltaBezier);
                transform.position = m_Destination;
            break;

            case 2:
                Vector3 p = m_Target.transform.position;
                // Debug.Log(GraphicMath.Distance2Point(transform.position, p));
                if (GraphicMath.Distance2Point(transform.position, p) <= 4 && m_RigidBody.IsSleeping())
                {
                    m_RigidBody.WakeUp();
                    m_RigidBody.AddForce(new Vector3(0f, m_JumpForce, 0f));
                }
            break;
        }
    }

    private void _UpdateRotation(float dt)
    {
        switch (m_Type)
        {
            case 0:
            break;

            case 1:
                Vector3 r = transform.rotation.eulerAngles;
                r.z += (float)(500*dt);
                transform.rotation = Quaternion.Euler(r);
            break;
        }
    }

    private void _CompleteMoveHandler()
    {
        m_Delta = Vector3.zero;
        m_DeltaBezier = 0;
        switch (m_Type)
        {
            case 0:
                m_PathMgr.CompleteMoveHandler();
            break;

            case 1:
            if (m_IsLoopFromBegin && m_PathMgr.isMoveComplete)
            {
                // Debug.Log("reset");
                m_PathMgr.ResetPoint();
            }
            else m_PathMgr.CompleteMoveHandler();
            break;
        }
    }
}
