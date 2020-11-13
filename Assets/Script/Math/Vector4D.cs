using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoftRender
{
    public class Vector4D
    {
        public float x;
        public float y;
        public float z;
        public float w;

        public Vector4D(float x, float y, float z,float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public Vector3D ToVector3D()
        {
            Vector3D vector3D = new Vector3D(x,y,z);
            return vector3D;
        }

        public void Set(float x, float y, float z,float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public void MultiplyNum(float num)
        {
            x *= num;
            y *= num;
            z *= num;
            w *= num;
        }

        public Vector4D Copy()
        {
            return new Vector4D(x, y, z,w);
        }

        public static Vector4D Minus(Vector4D v1, Vector4D v2)
        {
            return new Vector4D(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z, v1.w - v2.w);
        }

        //public float GetLength()
        //{
        //    return Mathf.Sqrt(x * x + y * y + z * z);
        //}

        //public float GetSquaredLength()
        //{
        //    return x * x + y * y + z * z;
        //}

        //public void Normalize()
        //{
        //    float length = GetLength();
        //    if (length == 1f || length == 0f)
        //        return;
        //    float scaleFactor = 1.0f / length;
        //    x *= scaleFactor;
        //    y *= scaleFactor;
        //    z *= scaleFactor;
        //}

        //public Vector3D GetNormalize()
        //{
        //    this.Normalize();
        //    return this;
        //}

        public static float Dot(Vector4D v1, Vector4D v2)
        {
            return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z + v1.w * v2.w;
        }

        //public static Vector4D Cross(Vector4D v1, Vector4D v2)
        //{
        //    return new Vector4D(v1.y * v2.z - v1.z * v2.y, v1.z * v2.x - v1.x * v2.z, v1.x * v2.y - v1.y * v2.x);
        //}

        public static Vector4D Lerp(Vector4D v1, Vector4D v2, float factor)
        {
            return new Vector4D(
                (1 - factor) * v1.x + v2.x * factor,
                (1 - factor) * v1.y + v2.y * factor,
                (1 - factor) * v1.z + v2.z * factor,
                (1 - factor) * v1.w + v2.w * factor
                );
        }


    }
}

