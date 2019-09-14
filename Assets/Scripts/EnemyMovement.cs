using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D m_RigidBody;
    [SerializeField] private PathMgr m_PathMgr;
    [SerializeField] private float m_MoveSpeed = 0f;
    private float m_DeltaX = 0f, m_DeltaY = 0f;
    private float m_Delta = 0f;
    private Vector2 m_Destination;
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
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = (m_RigidBody.velocity.y <= 0.2 && m_RigidBody.velocity.y >= -0.2) ? true : false;
        float dt = Time.deltaTime;
        _UpdatePath(dt);
        _UpdateDeltaTime(dt);
        _UpdatePosition(dt);
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
        if (m_RigidBody.gravityScale > 0)
        {
            if (isGrounded)
                // m_DeltaX = Math.Min(1, m_DeltaX += dt);
                m_DeltaX += m_MoveSpeed*dt;
        }
        else
            if (m_RigidBody.gravityScale == 0)
                m_Delta = Math.Min(1, m_Delta += dt);
    }

    private void _UpdatePosition(float dt)
    {
        if (m_RigidBody.gravityScale > 0)
        {
            m_Destination.x = m_PathMgr.GetTargetPosition(m_DeltaX).x;
            m_Destination.y = transform.position.y;
        }
        else
        {
            if (m_RigidBody.gravityScale == 0)
            {
                m_Destination = m_PathMgr.GetTargetPosition(m_Delta);
            }
        }

        transform.position = m_Destination;
    }

    private void _CompleteMoveHandler()
    {
        m_Delta = m_DeltaX = 0;
        m_PathMgr.CompleteMoveHandler();
    }
}
