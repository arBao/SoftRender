using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SoftRender
{
    public class MathTool
    {
        public static double tolerance = 1e-5f;
        public static float M_PI = 3.141592653589f;

        public static bool Equal(float a, float b)
        {
            return Mathf.Abs(a - b) < tolerance;
        }

        //public double Radians(float angle)
        //{
        //    return angle * M_PI / 180.0f;
        //}

        //public double Angles(double radians)
        //{
        //    return radians * 180f / M_PI;
        //}

    }
}

