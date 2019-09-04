using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGraphics : MonoBehaviour
{
    [SerializeField] private GraphicMath m_Math;
    [SerializeField] private bool m_IsUseBezier = false;
    [ConditionalField("m_IsUseBezier")] [SerializeField] private GameObject[] points;
    //
    private Vector2 m_drawPos;
    private Vector2[] m_controlPoints;
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
        if (m_IsUseBezier)
        {
            m_controlPoints = new Vector2[points.Length];
            for (int i =0; i< m_controlPoints.Length; i++)
                m_controlPoints[i] = points[i].transform.position;

            for (float i = 0; i <= 1 ; i += 0.1f)
            {
                m_drawPos = m_Math.BezierToPoint(i, m_controlPoints);
                Gizmos.DrawSphere(m_drawPos, 0.25f);
            }
        }
    }
}
