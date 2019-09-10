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
    [HideInInspector] public bool isCompletePath = false;
    bool isMoveComplete = false;
    float tempDelta = 0f;
    // Start is called before the first frame update
    void Start()
    {
        m_CurrentPath = m_Paths[0];
        m_CurrentPoint = !m_CurrentPath.isUseBezier ? m_CurrentPath.GetRoute()[0] : new Vector2();
        m_NextPoint = !m_CurrentPath.isUseBezier ? m_CurrentPath.GetRoute()[1] : new Vector2();
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
                int index = Array.IndexOf(m_CurrentPath.GetRoute(), m_NextPoint) + 1;
                if (index >= m_CurrentPath.GetRoute().Length)
                    _NextPath();
                else
                {
                    isCompletePath = false;
                    m_CurrentPoint = m_NextPoint;
                    m_NextPoint = m_CurrentPath.GetRoute()[index];
                }
            }
        }
    }

    public Vector2 GetTargetPosition(float dt)
    {
        tempDelta = dt;
        if (!isMoveComplete)
        {
            if (m_CurrentPath.isUseBezier)
                return GraphicMath.BezierToPoint(dt, m_CurrentPath.GetRoute());
            else
                return GraphicMath.MoveToPoint(dt, m_CurrentPoint, m_NextPoint);
        } 
        else
        {
            return m_Paths[m_Paths.Length - 1].GetRoute()[m_Paths[m_Paths.Length - 1].GetRoute().Length-1];
        }
    }

    private void _UpdateComplete()
    {
        if (m_CurrentPath.isUseBezier)
        {
            if (GraphicMath.BezierToPoint(tempDelta, m_CurrentPath.GetRoute()) == m_CurrentPath.GetRoute()[m_CurrentPath.GetRoute().Length - 1])
                isCompletePath = true;
        }
        else
        {
            if (GraphicMath.MoveToPoint(tempDelta, m_CurrentPoint, m_NextPoint) == m_NextPoint)
            // && m_NextPoint == m_CurrentPath.GetRoute()[m_CurrentPath.GetRoute().Length - 1])
                isCompletePath = true;
        }
    }

    private void _NextPath()
    {
        int index = Array.IndexOf(m_Paths, m_CurrentPath) + 1;
        if (index >= m_Paths.Length)
        {
            isMoveComplete = true;
            return;
        }
        else
        {
            isCompletePath = false;
            m_CurrentPath = m_Paths[index];
            m_CurrentPoint = !m_CurrentPath.isUseBezier ? m_CurrentPath.GetRoute()[0] : new Vector2();
            m_NextPoint = !m_CurrentPath.isUseBezier ? m_CurrentPath.GetRoute()[1] : new Vector2();
        }
    }
}
