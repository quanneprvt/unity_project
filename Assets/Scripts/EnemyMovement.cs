using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D m_RigidBody;
    private GameObject m_path;
    //
    bool isGrounded = false;
    //
    private void Awake()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isGrounded = (m_RigidBody.velocity.y <= 0.2 && m_RigidBody.velocity.y >= -0.2) ? true : false;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = (m_RigidBody.velocity.y <= 0.2 && m_RigidBody.velocity.y >= -0.2) ? true : false;
        // Debug.Log(isGrounded);
    }
}
