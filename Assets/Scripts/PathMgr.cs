using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Graphic.Math;

public class PathMgr : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_RigidBody;
    [SerializeField] private Path[] m_Paths;
    private Path m_CurrentPath;
    private Vector2 m_CurrentPoint;
    private Vector2 m_NextPoint;
    private Vector2 m_Destination;
    // Start is called before the first frame update
    void Start()
    {
        m_CurrentPath = m_Paths[0];
        m_CurrentPoint = m_CurrentPath.GetRoute()[0];
        m_NextPoint = !m_CurrentPath.isUseBezier ? m_CurrentPath.GetRoute()[1] : new Vector2(0,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetPath()
    {

    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        
    }
}
