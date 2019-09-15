using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Graphic.Math;
public class Path : MonoBehaviour
{
    // [SerializeField] private GraphicMath m_Math;
    public bool isUseBezier = false;
    public bool isLoop = false;
    [ConditionalField("isLoop", true)] [SerializeField] private int m_LoopFrom;
    [ConditionalField("isLoop", true)] [SerializeField] private int m_LoopTo;
    [SerializeField] private GameObject[] points;
    //
    private Vector3 m_drawPos;
    private Vector3[] m_controlPoints;
    private Vector3[] m_LoopPath;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (points.Length >0)
        {
            m_controlPoints = new Vector3[points.Length];
            for (int i =0; i< m_controlPoints.Length; i++)
            {
                if (points[i])
                    m_controlPoints[i] = points[i].transform.position;
            }
        }
        Vector3[] p;
        p = m_controlPoints.SubArray(m_LoopFrom, m_LoopTo - m_LoopFrom + 1);
        m_LoopPath = (m_controlPoints.Length == p.Length) ? m_controlPoints : p;
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {
        // if (points.Length >0)
        // {
        //     m_controlPoints = new Vector2[points.Length];
        //     for (int i =0; i< m_controlPoints.Length; i++)
        //     {
        //         if (points[i])
        //             m_controlPoints[i] = points[i].transform.position;
        //     }
        // }
        // Vector2[] p;
        // p = m_controlPoints.SubArray(m_LoopFrom, m_LoopTo - m_LoopFrom + 1);
        // m_LoopPath = (m_controlPoints.Length == p.Length) ? m_controlPoints : p;
        // Debug.Log(m_LoopPath.Length);
    }

    public Vector3[] GetLoopRoute()
    {
        return m_LoopPath;
    }

    public Vector3[] GetRoute()
    {
        return m_controlPoints;
    }

    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    private void OnDrawGizmos()
    {
        if (points.Length >0)
        {
            m_controlPoints = new Vector3[points.Length];
            for (int i =0; i< m_controlPoints.Length; i++)
            {
                if (points[i])
                    m_controlPoints[i] = points[i].transform.position;
            }
            if (isUseBezier && m_controlPoints.Length > 2)
            {
                for (float i = 0; i <= 1 ; i += 0.1f)
                {
                    m_drawPos = GraphicMath.BezierToPoint(i, m_controlPoints);
                    Gizmos.DrawSphere(m_drawPos, 0.25f);
                }
            }
            else
            {
                if (m_controlPoints.Length >= 2)
                    for (int i = 0; i< m_controlPoints.Length - 1; i++)
                        Gizmos.DrawLine(m_controlPoints[i], m_controlPoints[i + 1]);
            }
        }
    }
}
