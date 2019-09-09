using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Graphic.Math;
public class Path : MonoBehaviour
{
    // [SerializeField] private GraphicMath m_Math;
    [SerializeField] private GameObject[] points;
    //
    private Vector2 m_drawPos;
    private Vector2[] m_controlPoints;
    public bool isUseBezier = false;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (points.Length >0)
        {
            m_controlPoints = new Vector2[points.Length];
            for (int i =0; i< m_controlPoints.Length; i++)
            {
                if (points[i])
                    m_controlPoints[i] = points[i].transform.position;
            }
        }
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

    public Vector2[] GetRoute()
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
            m_controlPoints = new Vector2[points.Length];
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
                if (m_controlPoints.Length == 2)
                    Gizmos.DrawLine(m_controlPoints[0], m_controlPoints[1]);
            }
        }
    }
}
