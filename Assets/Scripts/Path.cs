using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct path {
    public Vector2[] points;
    public bool isBezier;
    public path (Vector2[] p, bool b)
    {
        this.points = p;
        this.isBezier = b;
    }
};

public class Path : MonoBehaviour
{
    [SerializeField] private GraphicMath m_Math;
    [SerializeField] private bool m_IsUseBezier = false;
    [ConditionalField("m_IsUseBezier")] [SerializeField] private GameObject[] points;
    //
    private Vector2 m_drawPos;
    private Vector2[] m_controlPoints;
    public path path = new path(m_controlPoints, m_IsUseBezier);
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    private void OnDrawGizmos()
    {
        m_controlPoints = new Vector2[points.Length];
        for (int i =0; i< m_controlPoints.Length; i++)
            m_controlPoints[i] = points[i].transform.position;
        if (m_IsUseBezier && m_controlPoints.Length > 2)
        {
            for (float i = 0; i <= 1 ; i += 0.1f)
            {
                m_drawPos = m_Math.BezierToPoint(i, m_controlPoints);
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
