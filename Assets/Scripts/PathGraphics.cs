using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGraphics : MonoBehaviour
{
    [SerializeField] private GameObject[] points;
    [SerializeField] private BezierCurve Bezier;
    //
    private Vector2? m_drawPos;
    private Vector2[] m_controlPoints;
    // Start is called before the first frame update
    void Start()
    {
        m_controlPoints = new Vector2[points.Length];
        for (int i =0; i< m_controlPoints.Length; i++)
            m_controlPoints[i] = points[i].transform.position;
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
        // for (float i = 0; i <= 1 ; i += 0.1f)
        // {
        //     m_drawPos = Bezier.ToPoint(i, m_controlPoints);
        //     Debug.Log((Vector2)m_drawPos);
        //     // Gizmos.DrawSphere((Vector2)m_drawPos, 0.25f);
        // }
    }
}
