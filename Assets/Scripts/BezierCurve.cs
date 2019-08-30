using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // for (float i = 0; i<=1; i+= 0.25f)
        //     Debug.Log(Bezier(i, new Vector2[] {new Vector2(0,0),new Vector2(100,100)}));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2? ToPoint(float t, Vector2[] array, int i1 = 0, int i2 = 0)
    {
        if (i2 == 0)
            i2 = array.Length - 1;
        int length = i2 - i1 + 1;
        if(length > 2)
        {
            return (1 - t)*ToPoint(t, array, i1, i2-1) + t*ToPoint(t, array, i1+1, i2);
        }
        else if(length >= 2)
        {
            return (1 - t)*array[i1] + t*array[i2];
        }
        else if(length >= 1)
        {
            return array[i1];
        }
        else
        {
            return null;
        }
    }
}
