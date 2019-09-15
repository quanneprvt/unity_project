namespace Graphic.Math
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    public static class GraphicMath
    {
        // Start is called before the first frame update
        private static bool m_DebugFrame = false;

        public static double Distance2Point(Vector3 p1, Vector3 p2)
        {
            float x = p2.x - p1.x;
            float y = p2.y - p1.y;
            float z = p2.z - p1.z;
            return Math.Sqrt(x*x + y*y + z*z);
        }

        public static double Angle2Point(Vector3 p1, Vector3 p2)
        {
            float xDiff = p2.x - p1.x;
            float yDiff = p2.y - p1.y;

            return Math.Atan2(yDiff, xDiff);
        }

        public static Vector3 Normalize (Vector3 p1, Vector3 p2 = new Vector3(), double scale = 1)
        {
            double x = p2.x - p1.x;
            double y = p2.y - p1.y;
            double z = p2.z - p1.z;
            double length = Math.Sqrt(x*x + y*y + z*z);
            return new Vector3(
                (float)(scale * x/length),
                (float)(scale * y/length),
                (float)(scale * z/length)
            );
        }

        public static Vector3 MoveToPoint(float dt, Vector3 f, Vector3 t, bool isUpdateX = true, bool isUpdateY = true, bool isUpdateZ = true)
        {
            Vector3 temp = new Vector3(0,0,0);
            bool isFinish = false;
            // double a = Angle2Point(f, t);
            // temp.x = (float)(f.x + dt*Math.Cos(a));
            // temp.y = (float)(f.y + dt*Math.Sin(a));
            temp = Vector3.MoveTowards(f, t, dt);
            Vector3 d;
            d.x = Normalize(temp, t).x/Normalize(f,t).x;
            d.y = Normalize(temp, t).y/Normalize(f,t).y;
            d.z = Normalize(temp, t).z/Normalize(f,t).z;
            if (m_DebugFrame)
            {
                m_DebugFrame = false;
                // Debug.Log(f.x);
                // Debug.Log(t.x);
                // Debug.Log(a);
            }
            if (isUpdateX && isUpdateY && isUpdateZ)
            {
                if (d.x < 0 && d.y < 0 && d.z < 0)
                    isFinish = true;
            }
            else 
            {
                if (isUpdateX)
                {
                    if (d.x < 0)
                        isFinish = true;
                }
                if (isUpdateY)
                {
                    if (d.y <0)
                        isFinish = true;
                }
                if (isUpdateZ)
                {
                    if (d.z < 0)
                        isFinish = true;
                }
            }
            if (isFinish)
                return t;
            else return temp;
        }

        public static Vector3 BezierToPoint(float t, Vector3[] array, int i1 = 0, int i2 = 0)
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
                return new Vector3(0,0);
            }
        }
    }
}
