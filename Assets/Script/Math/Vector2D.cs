using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoftRender
{
    public class Vector2D
    {
        public float x;
        public float y;
        public Vector2D(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public void Set(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public float GetLength()
        {
            return Mathf.Sqrt(x * x + y * y);
        }

        public float GetSquaredLength()
        {
            return x * x + y * y;
        }

        public void MultiplyNum(float num)
        {
            x *= num;
            y *= num;
        }

        public void Normalize()
        {
            float length = GetLength();
            if (MathTool.Equal(length, 1f) || MathTool.Equal(length, 0f))
                return;
            float scaleFactor = 1.0f / length;
            x *= scaleFactor;
            y *= scaleFactor;
        }

        public Vector2D GetNormalize()
        {
            this.Normalize();
            return this;
        }

        public static Vector2D Lerp(Vector2D v1, Vector2D v2,float weight)
        {
            Vector2D vector2D = new Vector2D(0,0);
            vector2D.x = (1 - weight) * v1.x + weight * v2.x;
            vector2D.y = (1 - weight) * v1.y + weight * v2.y;
            return vector2D;
        }

        public Vector2D Copy()
        {
            return new Vector2D(x, y);
        }

        //public Vector2D Add(Vector2D v2)
        //{

        //}

        //public void 
    }
}

