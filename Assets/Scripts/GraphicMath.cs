

namespace Graphic.Math
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    public static class GraphicMath
    {
        // Start is called before the first frame update
        static void Start()
        {
            // for (float i = 0; i<=1; i+= 0.25f)
            //     Debug.Log(Bezier(i, new Vector2[] {new Vector2(0,0),new Vector2(100,100)}));
        }

        public static double Angle2Point(Vector2 p1, Vector2 p2)
        {
            float xDiff = p2.x - p1.x;
            float yDiff = p2.y - p1.y;
            return Math.Atan2(yDiff, xDiff);
        }

        public static Vector2 MoveToPoint(float dt, Vector2 f, Vector2 t)
        {
            Vector2 temp = new Vector2(0,0);
            double a = Angle2Point(f, t);
            temp.x = (float)(f.x + dt*Math.Cos(a));
            temp.y = (float)(f.y + dt*Math.Sin(a));
            // Debug.Log(temp.x/f.x);
            if (temp.x/t.x >=1)
                return t;
            else return temp;
        }

        public static Vector2 BezierToPoint(float t, Vector2[] array, int i1 = 0, int i2 = 0)
        {
            if (i2 == 0)
                i2 = array.Length - 1;
            int length = i2 - i1 + 1;
            if(length > 2)
            {
                return (1 - t)*BezierToPoint(t, array, i1, i2-1) + t*BezierToPoint(t, array, i1+1, i2);
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
                return new Vector2(0,0);
            }
        }
    }
}
