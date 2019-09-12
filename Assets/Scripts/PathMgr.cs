using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Graphic.Math;

public class PathMgr : MonoBehaviour
{
    [SerializeField] private Path[] m_Paths;
    private Path m_CurrentPath;
    private Vector2 m_CurrentPoint;
    private Vector2 m_NextPoint;
    private Vector2[] m_LoopPath;
    [HideInInspector] public bool isCompletePath = false;
    [HideInInspector] public bool isMoveComplete = false;
    [HideInInspector] public float tempDelta = 0f;
    [HideInInspector] public Vector2[] route;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Path p in m_Paths)
            p.Init();
        m_CurrentPath = m_Paths[0];
        route = m_CurrentPath.GetRoute();
        m_CurrentPoint = !m_CurrentPath.isUseBezier ? route[0] : new Vector2();
        m_NextPoint = !m_CurrentPath.isUseBezier ? route[1] : new Vector2();
    }

    // Update is called once per frame
    void Update()
    {
        _UpdateComplete();
    }

    public void CompleteMoveHandler()
    {
        if (!isMoveComplete)
        {
            if (m_CurrentPath.isUseBezier)
            {
                _NextPath();
            }
            else
            {
                int index = Array.IndexOf(route, m_NextPoint) + 1;
                if (index >= route.Length)
                    _NextPath();
                else
                {
                    isCompletePath = false;
                    m_CurrentPoint = m_NextPoint;
                    m_NextPoint = route[index];
                }
            }
        }
    }

    public Vector2 GetTargetPosition(float dt)
    {
        tempDelta = dt;
        if (!isMoveComplete)
        {
            Debug.Log("false");
            if (m_CurrentPath.isUseBezier)
                return GraphicMath.BezierToPoint(dt, m_CurrentPath.GetRoute());
            else
                return GraphicMath.MoveToPoint(dt, m_CurrentPoint, m_NextPoint);
        } 
        else
        {
            // Debug.Log(dt);
            // Debug.Log("complete");
            return m_Paths[m_Paths.Length - 1].GetRoute()[m_Paths[m_Paths.Length - 1].GetRoute().Length-1];
        }
    }

    private void _UpdateComplete()
    {
        if (!isCompletePath)
        {
            if (m_CurrentPath.isUseBezier)
            {
                if (GraphicMath.BezierToPoint(tempDelta, m_CurrentPath.GetRoute()) == m_CurrentPath.GetRoute()[m_CurrentPath.GetRoute().Length - 1])
                    isCompletePath = true;
            }
            else
            {
                // Debug.Log(tempDelta);
                Debug.Log(m_CurrentPoint);
                Debug.Log(m_NextPoint);
                // Debug.Log(GraphicMath.MoveToPoint(tempDelta, m_CurrentPoint, m_NextPoint));
                if (GraphicMath.MoveToPoint(tempDelta, m_CurrentPoint, m_NextPoint) == m_NextPoint)
                // && m_NextPoint == m_CurrentPath.GetRoute()[m_CurrentPath.GetRoute().Length - 1])
                {
                    isCompletePath = true;
                }
            }
        }
    }

    private void _NextPath()
    {
        int index = Array.IndexOf(m_Paths, m_CurrentPath) + 1;
        if (index >= m_Paths.Length && !m_CurrentPath.isLoop)
        {
            isMoveComplete = true;
            return;
        }
        else
        {
            isCompletePath = false;
            m_CurrentPath = (!m_CurrentPath.isLoop) ? m_Paths[index] : m_CurrentPath;
            if (m_CurrentPath.isLoop)
            {
                route = m_CurrentPath.GetLoopRoute();
                Array.Reverse(route);
                // Debug.Log(route);
                Debug.Log("reverse");
            }
            else
            {
                route = m_CurrentPath.GetRoute();
            }
            m_CurrentPoint = !m_CurrentPath.isUseBezier ? route[0] : new Vector2();
            m_NextPoint = !m_CurrentPath.isUseBezier ? route[1] : new Vector2();
        }
    }
}
