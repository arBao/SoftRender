using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoftRender
{
    public class Matrix4x4
    {

        /*
         * 0 4 8  12
         * 1 5 9  13
         * 2 6 10 14
         * 3 7 11 15
         */        

        public float[] entries = new float[16];

        public Matrix4x4()
        {
            LoadIdentity();
        }

        public Matrix4x4(float e0, float e1, float e2, float e3,
                  float e4, float e5, float e6, float e7,
                  float e8, float e9, float e10, float e11,
                  float e12, float e13, float e14, float e15)
        {
            entries[0] = e0;
            entries[1] = e1;
            entries[2] = e2;
            entries[3] = e3;
            entries[4] = e4;
            entries[5] = e5;
            entries[6] = e6;
            entries[7] = e7;
            entries[8] = e8;
            entries[9] = e9;
            entries[10] = e10;
            entries[11] = e11;
            entries[12] = e12;
            entries[13] = e13;
            entries[14] = e14;
            entries[15] = e15;
        }

        public Matrix4x4 Copy()
        {
            Matrix4x4 matrix = new Matrix4x4();
            for(int i=0;i< matrix.entries.Length;i++)
            {
                matrix.entries[i] = entries[i];
            }

            return matrix;
        }

        public void SetEntry(int position, float value)
        {
            entries[position] = value;
        }

        public Vector4D GetColumn(int column)
        {
            return new Vector4D(entries[column * 4 + 0], entries[column * 4 + 1], entries[column * 4 + 2], entries[column * 4 + 3]);
        }

        public Vector4D GetRow(int row)
        {
            return new Vector4D(entries[row], entries[row + 4], entries[row + 8], entries[row + 12]);
        }

        //初始值为1
        public void Set(int row,int colume,float value)
        {
            entries[row - 1 + (colume - 1) * 4] = value;
        }
        //初始值为1
        public float Get(int row,int colume)
        {
            return entries[row - 1 + (colume - 1) * 4];
        }

        //单位矩阵
        public void LoadIdentity()
        {
            entries = new float[16];
            entries[0] = 1;
            entries[5] = 1;
            entries[10] = 1;
            entries[15] = 1;
        }

        //清零
        public void LoadZero()
        {
            entries = new float[16];
        }

        public static Matrix4x4 Add(Matrix4x4 m1, Matrix4x4 m2)
        {
            return new Matrix4x4(
                m1.entries[0] + m2.entries[0],
                m1.entries[1] + m2.entries[1],
                m1.entries[2] + m2.entries[2],
                m1.entries[3] + m2.entries[3],
                m1.entries[4] + m2.entries[4],
                m1.entries[5] + m2.entries[5],
                m1.entries[6] + m2.entries[6],
                m1.entries[7] + m2.entries[7],
                m1.entries[8] + m2.entries[8],
                m1.entries[9] + m2.entries[9],
                m1.entries[10] + m2.entries[10],
                m1.entries[11] + m2.entries[11],
                m1.entries[12] + m2.entries[12],
                m1.entries[13] + m2.entries[13],
                m1.entries[14] + m2.entries[14],
                m1.entries[15] + m2.entries[15]);
        }

        public static Matrix4x4 Minus(Matrix4x4 m1, Matrix4x4 m2)
        {
            return new Matrix4x4(
                m1.entries[0] - m2.entries[0],
                m1.entries[1] - m2.entries[1],
                m1.entries[2] - m2.entries[2],
                m1.entries[3] - m2.entries[3],
                m1.entries[4] - m2.entries[4],
                m1.entries[5] - m2.entries[5],
                m1.entries[6] - m2.entries[6],
                m1.entries[7] - m2.entries[7],
                m1.entries[8] - m2.entries[8],
                m1.entries[9] - m2.entries[9],
                m1.entries[10] - m2.entries[10],
                m1.entries[11] - m2.entries[11],
                m1.entries[12] - m2.entries[12],
                m1.entries[13] - m2.entries[13],
                m1.entries[14] - m2.entries[14],
                m1.entries[15] - m2.entries[15]);
        }

        public static Vector4D MultiplyVector4D(Matrix4x4 m, Vector4D vector4D)
        {
            float x = m.entries[0] * vector4D.x + m.entries[4] * vector4D.y + m.entries[8] * vector4D.z + m.entries[12] * vector4D.w;
            float y = m.entries[1] * vector4D.x + m.entries[5] * vector4D.y + m.entries[9] * vector4D.z + m.entries[13] * vector4D.w;
            float z = m.entries[2] * vector4D.x + m.entries[6] * vector4D.y + m.entries[10] * vector4D.z + m.entries[14] * vector4D.w;
            float w = m.entries[3] * vector4D.x + m.entries[7] * vector4D.y + m.entries[11] * vector4D.z + m.entries[15] * vector4D.w;
            return new Vector4D(x,y,z,w);
        }

        public static Matrix4x4 Multiply(Matrix4x4 m1, Matrix4x4 m2)
        {
            return new Matrix4x4(
                m1.entries[0] * m2.entries[0] + m1.entries[4] * m2.entries[1] + m1.entries[8] * m2.entries[2] + m1.entries[12] * m2.entries[3],
            m1.entries[1] * m2.entries[0] + m1.entries[5] * m2.entries[1] + m1.entries[9] * m2.entries[2] + m1.entries[13] * m2.entries[3],
            m1.entries[2] * m2.entries[0] + m1.entries[6] * m2.entries[1] + m1.entries[10] * m2.entries[2] + m1.entries[14] * m2.entries[3],
            m1.entries[3] * m2.entries[0] + m1.entries[7] * m2.entries[1] + m1.entries[11] * m2.entries[2] + m1.entries[15] * m2.entries[3],
            m1.entries[0] * m2.entries[4] + m1.entries[4] * m2.entries[5] + m1.entries[8] * m2.entries[6] + m1.entries[12] * m2.entries[7],
            m1.entries[1] * m2.entries[4] + m1.entries[5] * m2.entries[5] + m1.entries[9] * m2.entries[6] + m1.entries[13] * m2.entries[7],
            m1.entries[2] * m2.entries[4] + m1.entries[6] * m2.entries[5] + m1.entries[10] * m2.entries[6] + m1.entries[14] * m2.entries[7],
            m1.entries[3] * m2.entries[4] + m1.entries[7] * m2.entries[5] + m1.entries[11] * m2.entries[6] + m1.entries[15] * m2.entries[7],
            m1.entries[0] * m2.entries[8] + m1.entries[4] * m2.entries[9] + m1.entries[8] * m2.entries[10] + m1.entries[12] * m2.entries[11],
            m1.entries[1] * m2.entries[8] + m1.entries[5] * m2.entries[9] + m1.entries[9] * m2.entries[10] + m1.entries[13] * m2.entries[11],
            m1.entries[2] * m2.entries[8] + m1.entries[6] * m2.entries[9] + m1.entries[10] * m2.entries[10] + m1.entries[14] * m2.entries[11],
            m1.entries[3] * m2.entries[8] + m1.entries[7] * m2.entries[9] + m1.entries[11] * m2.entries[10] + m1.entries[15] * m2.entries[11],
            m1.entries[0] * m2.entries[12] + m1.entries[4] * m2.entries[13] + m1.entries[8] * m2.entries[14] + m1.entries[12] * m2.entries[15],
            m1.entries[1] * m2.entries[12] + m1.entries[5] * m2.entries[13] + m1.entries[9] * m2.entries[14] + m1.entries[13] * m2.entries[15],
            m1.entries[2] * m2.entries[12] + m1.entries[6] * m2.entries[13] + m1.entries[10] * m2.entries[14] + m1.entries[14] * m2.entries[15],
            m1.entries[3] * m2.entries[12] + m1.entries[7] * m2.entries[13] + m1.entries[11] * m2.entries[14] + m1.entries[15] * m2.entries[15]);
        }

        public void MultiplyNum(float num)
        {
            for(int i=0;i<this.entries.Length;i++)
            {
                entries[i] *= num;
            }
        }

        //逆矩阵
        public void Inverse()
        {
            this.InverseTranspose();
            this.Transpose();
        }

        //public Matrix4x4 GetInverse()
        //{

        //}

        //转置矩阵
        public void Transpose()
        {
            float tmp = 0;
            tmp = entries[4];
            entries[4] = entries[1];
            entries[1] = tmp;

            tmp = entries[8];
            entries[8] = entries[2];
            entries[2] = tmp;

            tmp = entries[9];
            entries[9] = entries[6];
            entries[6] = tmp;

            tmp = entries[12];
            entries[12] = entries[3];
            entries[3] = tmp;

            tmp = entries[13];
            entries[13] = entries[7];
            entries[7] = tmp;

            tmp = entries[14];
            entries[14] = entries[11];
            entries[11] = tmp;
        }

        //public Matrix4x4 GetTranspose()
        //{

        //}
        //逆转置
        public void InverseTranspose()
        {
            float[] result = new float[16];
            float[] tmp = new float[12];
            float det = 0;
            tmp[0] = entries[10] * entries[15];
            tmp[1] = entries[11] * entries[14];
            tmp[2] = entries[9] * entries[15];
            tmp[3] = entries[11] * entries[13];
            tmp[4] = entries[9] * entries[14];
            tmp[5] = entries[10] * entries[13];
            tmp[6] = entries[8] * entries[15];
            tmp[7] = entries[11] * entries[12];
            tmp[8] = entries[8] * entries[14];
            tmp[9] = entries[10] * entries[12];
            tmp[10] = entries[8] * entries[13];
            tmp[11] = entries[9] * entries[12];

            result[0] = tmp[0] * entries[5] + tmp[3] * entries[6] + tmp[4] * entries[7]
            - tmp[1] * entries[5] - tmp[2] * entries[6] - tmp[5] * entries[7];

            result[1] = tmp[1] * entries[4] + tmp[6] * entries[6] + tmp[9] * entries[7]
            - tmp[0] * entries[4] - tmp[7] * entries[6] - tmp[8] * entries[7];

            result[2] = tmp[2] * entries[4] + tmp[7] * entries[5] + tmp[10] * entries[7]
            - tmp[3] * entries[4] - tmp[6] * entries[5] - tmp[11] * entries[7];

            result[3] = tmp[5] * entries[4] + tmp[8] * entries[5] + tmp[11] * entries[6]
            - tmp[4] * entries[4] - tmp[9] * entries[5] - tmp[10] * entries[6];

            result[4] = tmp[1] * entries[1] + tmp[2] * entries[2] + tmp[5] * entries[3]
            - tmp[0] * entries[1] - tmp[3] * entries[2] - tmp[4] * entries[3];

            result[5] = tmp[0] * entries[0] + tmp[7] * entries[2] + tmp[8] * entries[3]
            - tmp[1] * entries[0] - tmp[6] * entries[2] - tmp[9] * entries[3];

            result[6] = tmp[3] * entries[0] + tmp[6] * entries[1] + tmp[11] * entries[3]
            - tmp[2] * entries[0] - tmp[7] * entries[1] - tmp[10] * entries[3];

            result[7] = tmp[4] * entries[0] + tmp[9] * entries[1] + tmp[10] * entries[2]
            - tmp[5] * entries[0] - tmp[8] * entries[1] - tmp[11] * entries[2];

            tmp[0] = entries[2] * entries[7];
            tmp[1] = entries[3] * entries[6];
            tmp[2] = entries[1] * entries[7];
            tmp[3] = entries[3] * entries[5];
            tmp[4] = entries[1] * entries[6];
            tmp[5] = entries[2] * entries[5];
            tmp[6] = entries[0] * entries[7];
            tmp[7] = entries[3] * entries[4];
            tmp[8] = entries[0] * entries[6];
            tmp[9] = entries[2] * entries[4];
            tmp[10] = entries[0] * entries[5];
            tmp[11] = entries[1] * entries[4];


            result[8] = tmp[0] * entries[13] + tmp[3] * entries[14] + tmp[4] * entries[15]
            - tmp[1] * entries[13] - tmp[2] * entries[14] - tmp[5] * entries[15];

            result[9] = tmp[1] * entries[12] + tmp[6] * entries[14] + tmp[9] * entries[15]
            - tmp[0] * entries[12] - tmp[7] * entries[14] - tmp[8] * entries[15];

            result[10] = tmp[2] * entries[12] + tmp[7] * entries[13] + tmp[10] * entries[15]
            - tmp[3] * entries[12] - tmp[6] * entries[13] - tmp[11] * entries[15];

            result[11] = tmp[5] * entries[12] + tmp[8] * entries[13] + tmp[11] * entries[14]
            - tmp[4] * entries[12] - tmp[9] * entries[13] - tmp[10] * entries[14];

            result[12] = tmp[2] * entries[10] + tmp[5] * entries[11] + tmp[1] * entries[9]
            - tmp[4] * entries[11] - tmp[0] * entries[9] - tmp[3] * entries[10];

            result[13] = tmp[8] * entries[11] + tmp[0] * entries[8] + tmp[7] * entries[10]
            - tmp[6] * entries[10] - tmp[9] * entries[11] - tmp[1] * entries[8];

            result[14] = tmp[6] * entries[9] + tmp[11] * entries[11] + tmp[3] * entries[8]
            - tmp[10] * entries[11] - tmp[2] * entries[8] - tmp[7] * entries[9];

            result[15] = tmp[10] * entries[10] + tmp[4] * entries[8] + tmp[9] * entries[9]
            - tmp[8] * entries[9] - tmp[11] * entries[10] - tmp[5] * entries[8];

            det = entries[0] * result[0]
            + entries[1] * result[1]
            + entries[2] * result[2]
            + entries[3] * result[3];

            if (det == 0.0f)
                return;

            for(int i=0;i<result.Length;i++)
            {
                result[i] /= det;

                entries[i] = result[i];
            }

        }

        public void SetTranslation(Vector3D translation)
        {
            LoadIdentity();
            entries[12] = translation.x;
            entries[13] = translation.y;
            entries[14] = translation.z;
        }

        public void SetScale(Vector3D scaleFactor)
        {
            LoadIdentity();
            entries[0] = scaleFactor.x;
            entries[5] = scaleFactor.y;
            entries[10] = scaleFactor.z;
        }

        public void SetRotationAxis(float angle, Vector3D axis)
        {
            axis.Normalize();
            float sinAngle = Mathf.Sin(angle * Mathf.Deg2Rad);
            float cosAngle = Mathf.Cos(angle * Mathf.Deg2Rad);
            float oneMinusCosAngle = 1 - cosAngle;

            LoadIdentity();
            entries[0] = (axis.x) * (axis.x) + cosAngle * (1 - (axis.x) * (axis.x));
            entries[4] = (axis.x) * (axis.y) * (oneMinusCosAngle) - sinAngle * axis.z;
            entries[8] = (axis.x) * (axis.z) * (oneMinusCosAngle) + sinAngle * axis.y;

            entries[1] = (axis.x) * (axis.y) * (oneMinusCosAngle) + sinAngle * axis.z;
            entries[5] = (axis.y) * (axis.y) + cosAngle * (1 - (axis.y) * (axis.y));
            entries[9] = (axis.y) * (axis.z) * (oneMinusCosAngle) - sinAngle * axis.x;

            entries[2] = (axis.x) * (axis.z) * (oneMinusCosAngle) - sinAngle * axis.y;
            entries[6] = (axis.y) * (axis.z) * (oneMinusCosAngle) + sinAngle * axis.x;
            entries[10] = (axis.z) * (axis.z) + cosAngle * (1 - (axis.z) * (axis.z));


        }

        public void SetRotationX(float angle)
        {
            LoadIdentity();
            entries[5] = Mathf.Cos(angle * Mathf.Deg2Rad);
            entries[6] = Mathf.Sin(angle * Mathf.Deg2Rad);
            entries[9] = -entries[6];
            entries[10] = entries[5];
        }

        public void SetRotationY(float angle)
        {
            LoadIdentity();
            entries[0] = Mathf.Cos(angle * Mathf.Deg2Rad);
            entries[2] = -Mathf.Sin(angle * Mathf.Deg2Rad);
            entries[8] = -entries[2];
            entries[10] = entries[0];
        }

        public void SetRotationZ(float angle)
        {
            LoadIdentity();
            entries[0] = Mathf.Cos(angle * Mathf.Deg2Rad);
            entries[1] = Mathf.Sin(angle * Mathf.Deg2Rad);
            entries[4] = -entries[1];
            entries[5] = entries[0];
        }

        public void SetRotationEuler(float angleX, float angleY, float angleZ)
        {
            float cr = Mathf.Cos(angleX * Mathf.Deg2Rad);
            float sr = Mathf.Sin(angleX * Mathf.Deg2Rad);
            float cp = Mathf.Cos(angleY * Mathf.Deg2Rad);
            float sp = Mathf.Sin(angleY * Mathf.Deg2Rad);
            float cy = Mathf.Cos(angleZ * Mathf.Deg2Rad);
            float sy = Mathf.Sin(angleZ * Mathf.Deg2Rad);

            entries[0] = cp * cy;
            entries[1] = cp * sy;
            entries[2] = -sp;

            float srsp = sr * sp;
            float crsp = cr * sp;

            entries[4] = srsp * cy - cr * sy;
            entries[5] = srsp * sy + cr * cy;
            entries[6] = sr * cp;
            entries[8] = crsp * cy + sr * sy;
            entries[9] = crsp * sy - sr * cy;
            entries[10] = cr * cp;

        }

        //fovy：y轴方向的视域角 aspect：屏幕的宽高比 P空间
        public void SetPerspective(float fovy,float aspect,float near,float far)
        {
            float rFovy = fovy * Mathf.Deg2Rad;
            float tanHalfFovy = Mathf.Tan(rFovy * 0.5f);
            entries[0] = 1.0f / (aspect * tanHalfFovy);
            entries[5] = 1.0f / (tanHalfFovy);
            entries[10] = -(far + near) / (far - near);
            entries[11] = -1.0f;
            entries[14] = (-2.0f * near * far) / (far - near);
        }

        public void SetOrtho(float left, float right, float bottom, float top, float near, float far)
        {
            LoadIdentity();
            entries[0] = 2.0f / (right - left);
            entries[5] = 2.0f / (top - bottom);
            entries[10] = -2.0f / (far - near);
            entries[12] = -(right + left) / (right - left);
            entries[13] = -(top + bottom) / (top - bottom);
            entries[14] = -(far + near) / (far - near);
        }

        //世界坐标转化到摄像机空间（view空间）
        public void SetLookAt(Vector3D cameraPos, Vector3D target,Vector3D worldUp)
        {
            Vector3D zAxis = Vector3D.Minus(cameraPos, target);
            //Vector3D zAxis = Vector3D.Minus(target, cameraPos);
            zAxis.Normalize();
            Vector3D xAxis = Vector3D.Cross(worldUp,zAxis);
            //Vector3D xAxis = Vector3D.Cross(zAxis, worldUp);
            xAxis.Normalize();
            Vector3D yAxis = Vector3D.Cross(zAxis, xAxis);
            //Vector3D yAxis = Vector3D.Cross(xAxis, zAxis);
            yAxis.Normalize();

            LoadIdentity();
            entries[0] = xAxis.x;
            entries[4] = xAxis.y;
            entries[8] = xAxis.z;

            entries[1] = yAxis.x;
            entries[5] = yAxis.y;
            entries[9] = yAxis.z;

            entries[2] = zAxis.x;
            entries[6] = zAxis.y;
            entries[10] = zAxis.z;

            entries[12] = -(Vector3D.Dot(xAxis, cameraPos));
            entries[13] = -(Vector3D.Dot(yAxis, cameraPos));
            entries[14] = -(Vector3D.Dot(zAxis, cameraPos));
        }

        public void SetViewPort(int left, int top, int width, int height)
        {
            entries[0] = -width / 2.0f;
            entries[5] = height / 2.0f;
            entries[12] = left + (width) / 2.0f;
            entries[13] = top + (height) / 2.0f;
        }
    }

}

