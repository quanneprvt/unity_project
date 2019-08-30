using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D m_rigidBody;
    //
    bool isGrounded = false;
    //
    private void Awake()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isGrounded = (m_rigidBody.velocity.y <= 0.2 && m_rigidBody.velocity.y >= -0.2) ? true : false;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = (m_rigidBody.velocity.y <= 0.2 && m_rigidBody.velocity.y >= -0.2) ? true : false;
        // Debug.Log(isGrounded);
    }
}
