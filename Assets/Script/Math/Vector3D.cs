using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoftRender
{
    public class Vector3D
    {
        public float x;
        public float y;
        public float z;

        public Vector3D(float x, float y,float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector4D ToVector4D()
        {
            Vector4D vector = new Vector4D(x, y, z, 0);
            return vector;
        }

        public void Set(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public float GetLength()
        {
            return Mathf.Sqrt(x * x + y * y + z * z);
        }

        public float GetSquaredLength()
        {
            return x * x + y * y + z * z;
        }

        public void Normalize()
        {
            float length = GetLength();
            if (MathTool.Equal(length,1f) || MathTool.Equal(length, 0f))
                return;
            float scaleFactor = 1.0f / length;
            x *= scaleFactor;
            y *= scaleFactor;
            z *= scaleFactor;
        }

        public void MultipyNum(float num)
        {
            x *= num;
            y *= num;
            z *= num;
        }

        public Vector3D GetNormalize()
        {
            this.Normalize();
            return this;
        }

        public static float Dot(Vector3D v1,Vector3D v2)
        {
            return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
        }

        public static Vector3D Cross(Vector3D v1, Vector3D v2)
        {
            return new Vector3D(v1.y * v2.z - v1.z * v2.y, v1.z * v2.x - v1.x * v2.z, v1.x * v2.y - v1.y * v2.x);
        }

        public static Vector3D Lerp(Vector3D v1, Vector3D v2,float factor)
        {
            return new Vector3D(
                (1 - factor) * v1.x + v2.x * factor,
                (1 - factor) * v1.y + v2.y * factor,
                (1 - factor) * v1.z + v2.z * factor
                );
        }

        public static Vector3D Minus(Vector3D v1, Vector3D v2)
        {
            return new Vector3D(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
        }

        public static Vector3D Minus(Vector3D v1, Vector4D v2)
        {
            return new Vector3D(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
        }

        public static Vector3D Add(Vector3D v1, Vector3D v2)
        {
            return new Vector3D(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
        }

        //值乘
        public static Vector3D Multipy(Vector3D v1, Vector3D v2)
        {
            return new Vector3D(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
        }

        public Vector3D Copy()
        {
            return new Vector3D(x, y,z);
        }

    }
}

