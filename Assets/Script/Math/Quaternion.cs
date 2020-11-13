using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoftRender
{
    // Z-Y-X Euler angles yaw pitch row
    public class Quaternion
    {
        public float x = 0;
        public float y = 0;
        public float z = 0;
        public float w = 1;

        public void Identity()
        {
            w = 1;
            x = 0;
            y = 0;
            z = 0;
        }

        public void FromMatrix(Matrix4x4 matrix)
        {
            //探测w x y z中的最大值（书本169页）
            float WSquare = matrix.Get(1,1) + matrix.Get(2,2) + matrix.Get(3,3);
            float XSquare = matrix.Get(1,1) - matrix.Get(2,2) - matrix.Get(3,3);
            float YSquare = matrix.Get(2,2) - matrix.Get(1,1) - matrix.Get(3,3);
            float ZSquare = matrix.Get(3,3) - matrix.Get(1,1) - matrix.Get(2,2);

            float max = WSquare;
            int biggestIndex = 0;
            if(XSquare > max)
            {
                max = XSquare;
                biggestIndex = 1;
            }
            if(YSquare > max)
            {
                max = YSquare;
                biggestIndex = 2;
            }
            if(ZSquare > max)
            {
                max = ZSquare;
                biggestIndex = 3;
            }

            float sqrt = Mathf.Sqrt(max + 1f) * 0.5f;
            float mult = 0.25f / sqrt;
            switch(biggestIndex)
            {
                case 0:
                    w = sqrt;
                    x = (matrix.Get(3,2) - matrix.Get(2,3)) * mult;
                    y = (matrix.Get(1,3) - matrix.Get(3,1)) * mult;
                    z = (matrix.Get(2,1) - matrix.Get(1,2)) * mult;
                    break;
                case 1:
                    x = sqrt;
                    w = (matrix.Get(3,2) - matrix.Get(2,3)) * mult;
                    y = (matrix.Get(1,2) + matrix.Get(2,1)) * mult;
                    z = (matrix.Get(3,1) + matrix.Get(1,3)) * mult;
                    break;
                case 2:
                    y = sqrt;
                    w = (matrix.Get(1,3) - matrix.Get(3,1)) * mult;
                    x = (matrix.Get(1,2) + matrix.Get(2,1)) * mult;
                    z = (matrix.Get(2,3) + matrix.Get(3,2)) * mult;
                    break;
                case 3:
                    z = sqrt;
                    w = (matrix.Get(2,1) - matrix.Get(1,2)) * mult;
                    x = (matrix.Get(3,1) + matrix.Get(1,3)) * mult;
                    y = (matrix.Get(2,3) + matrix.Get(3,2)) * mult;
                    break;
            }
            
            //w = 0.5f * Mathf.Sqrt(1 + matrix.entries[11] + matrix.entries[22] + matrix.entries[33]);
            //x = (matrix.entries[32] - matrix.entries[23]) / (4 * w);
            //y = (matrix.entries[13] - matrix.entries[31]) / (4 * w);
            //z = (matrix.entries[21] - matrix.entries[12]) / (4 * w);
        }

        //列矩阵    
        public Matrix4x4 ToMatrix()
        {

            Matrix4x4 matrix = new Matrix4x4();
            matrix.Set(1, 1, 1 - 2 * y * y - 2 * z * z);
            matrix.Set(1, 2, 2 * (x * y - z * w));
            matrix.Set(1, 3, 2 * (x * z + y * w));
            matrix.Set(2, 1, 2 * (x * y + z * w));
            matrix.Set(2, 2, (1 - 2 * x * x - 2 * z * z));
            matrix.Set(2, 3, 2 * (y * z - x * w));
            matrix.Set(3, 1, 2 * (x * z - y * w));
            matrix.Set(3, 2, 2 * (y * z + x * w));
            matrix.Set(3, 3, 1 - 2 * x * x - 2 * y * y);

            return matrix;
        }

        //输入弧度 输入为绕x轴y轴z轴
        public void FromEular(Vector3D eulars)
        {
            float alpha = eulars.z;
            float beta = eulars.y;
            float rumda = eulars.x;

            float cosA = Mathf.Cos(alpha / 2);
            float sinA = Mathf.Sin(alpha / 2);

            float cosB = Mathf.Cos(beta / 2);
            float sinB = Mathf.Sin(beta / 2);

            float cosR = Mathf.Cos(rumda / 2);
            float sinR = Mathf.Sin(rumda / 2);

            w = cosA * cosB * cosR + sinA * sinB * cosR;
            x = sinA * cosB * cosR - cosA * sinB * sinR;
            y = cosA * sinB * cosR + sinA * cosB * sinR;
            z = cosA * cosB * sinR - sinA * sinB * cosR;

        }

        //共轭四元数，用于取相反方向,与求逆是一样的
        public void Conjugate()
        {
            float norm = Mathf.Sqrt(x * x + y * y + z * z + w * w);
            x = -x;
            y = -y;
            z = -z;

            x /= norm;
            y /= norm;
            z /= norm;
            w /= norm;
        }

        //四元数求逆，用于求四元数之间的差值,与共轭是一样的
        public void Inverse()
        {
            float norm = Mathf.Sqrt(x * x + y * y + z * z + w * w);
            x = -x;
            y = -y;
            z = -z;

            x /= norm;
            y /= norm;
            z /= norm;
            w /= norm;
        }

        //求两个四元数的间隔
        public static Quaternion Delta(Quaternion q1,Quaternion q2)
        {
            q1.Conjugate();
            return Mutlipy(q1, q2);
        }

        /*
         * w
         * x
         * y
         * z
         */
        //输出弧度 输出为绕x轴y轴z轴
        public Vector3D ToEular()// Z-Y-X Euler angles yaw pitch row
        {
            Vector3D eular = new Vector3D(0, 0, 0);
            double Epsilon = 0.0009765625f;
            double Threshold = 0.5f - Epsilon;
            double TEST = w * y - x * z;
            //绕y轴旋转为90度的时候，万向节死锁  https://www.cnblogs.com/21207-iHome/p/6894128.html
            if (TEST < -Threshold || TEST > Threshold)
            {
                float sign = Mathf.Sign((float)TEST);//当x>0，sign(x)=1;当x=0，sign(x)=0; 当x<0， sign(x)=-1；
                eular.z = -2 * sign * Mathf.Atan2(x, w);
                eular.y = sign * (MathTool.M_PI / 2.0f);
                eular.x = 0;
            }
            else
            {
                eular.x = Mathf.Atan2(2 * (y * z + w * x), w * w - x * x - y * y + z * z);
                eular.y = Mathf.Asin(-2f * (x * z - w * y));
                eular.z = Mathf.Atan2(2 * (x * y + w * z), w * w + x * x - y * y - z * z);
            }

            return eular;
        }

        public void Normalize()
        {
            float norm = Mathf.Sqrt(x * x + y * y + z * z + w * w);
            x /= norm;
            y /= norm;
            z /= norm;
            w /= norm;
        }

        //点乘
        public static float Dot(Quaternion q1,Quaternion q2)
        {
            return q1.w * q2.w + q1.x * q2.x + q1.y * q2.y + q1.z * q2.z;
        }

        //相乘 得到新的四元数
        public static Quaternion Mutlipy(Quaternion q1,Quaternion q2)
        {
            Quaternion q = new Quaternion();
            q.w = q1.w * q2.w - q1.x * q2.x - q1.y * q2.y - q1.z * q2.z;
            q.x = q1.w * q2.x + q1.x * q2.w + q1.y * q2.z - q1.z * q2.y;
            q.y = q1.w * q2.y - q1.x * q2.z + q1.y * q2.w + q1.z * q2.x;
            q.z = q1.w * q2.z + q1.x * q2.y - q1.y * q2.x + q1.z * q2.w;

            return q;
        }



        //四元数求幂，用于插值
        public static Quaternion Pow(Quaternion q,float exp)
        {
            q.Normalize();
            if(Mathf.Abs(q.w) > 0.9999f)
            {
                return q;
            }

            //提取半角 alpha = theta/2
            float alpha = Mathf.Acos(q.w);
            float newAlpha = alpha * exp;
            q.w = Mathf.Cos(newAlpha);

            float mult = Mathf.Sin(newAlpha) / Mathf.Sin(alpha);
            q.x *= mult;
            q.y *= mult;
            q.z *= mult;
            return q;

        }

        public static Quaternion Slerp(Quaternion q1,Quaternion q2,float t)
        {
            if(t <= 0.0f)
            {
                return q1;
            }

            if(t >= 1.0f)
            {
                return q2;
            }

            float cosOmega = Dot(q1, q2);
            float k0, k1;
            float q2w = q2.w;
            float q2x = q2.x;
            float q2y = q2.y;
            float q2z = q2.z;

            //如果点乘为负，即为钝角，q和-q表示相同的角位移，但可能产生不同的差值，使用锐角取插值
            if(cosOmega < 0.0f)
            {
                q2w = -q2w;
                q2x = -q2x;
                q2y = -q2y;
                q2z = -q2z;
                cosOmega = -cosOmega;
            }

            if(cosOmega > 0.9999f)
            {
                //两方向非常接近，使用线性插值即可
                k0 = 1 - t;
                k1 = t;
            }
            else
            {
                //用三角函数 sin^2(omega) + cos^2(omega) = 1 计算sin值
                float sinOmega = Mathf.Sqrt(1 - cosOmega * cosOmega);
                //根据sin和cos用atan计算omega
                float omega = Mathf.Atan2(sinOmega, cosOmega);

                //由pow的定义知道sin的比值就是求幂
                k0 = Mathf.Sin((1 - t) * omega) / sinOmega;
                k1 = Mathf.Sin(t * omega) / sinOmega;
            }

            Quaternion quaternion = new Quaternion();
            quaternion.x = k0 * q1.x + k1 * q2x;
            quaternion.y = k0 * q1.y + k1 * q2y;
            quaternion.z = k0 * q1.z + k1 * q2z;
            quaternion.w = k0 * q1.w + k1 * q2w;

            return quaternion;
        }

        public static Quaternion GetRotateX(float theta)
        {
            Quaternion quaternion = new Quaternion();
            quaternion.w = Mathf.Cos(theta / 2);
            quaternion.x = Mathf.Sin(theta / 2);
            quaternion.y = 0;
            quaternion.z = 0;

            return quaternion;
        }

        public static Quaternion GetRotateY(float theta)
        {
            Quaternion quaternion = new Quaternion();
            quaternion.w = Mathf.Cos(theta / 2);
            quaternion.x = 0;
            quaternion.y = Mathf.Sin(theta / 2);
            quaternion.z = 0;

            return quaternion;
        }

        public static Quaternion GetRotateZ(float theta)
        {
            Quaternion quaternion = new Quaternion();
            quaternion.w = Mathf.Cos(theta / 2);
            quaternion.x = 0;
            quaternion.y = 0;
            quaternion.z = Mathf.Sin(theta / 2);

            return quaternion;
        }

        public static Quaternion GetRotateAxis(Vector3D axis,float theta)
        {
            Quaternion quaternion = new Quaternion();
            float sinThetaOver2 = Mathf.Sin(theta / 2);

            quaternion.w = Mathf.Cos(theta / 2);
            quaternion.x = axis.x * sinThetaOver2;
            quaternion.y = axis.y * sinThetaOver2;
            quaternion.z = axis.z * sinThetaOver2;

            return quaternion;
        }

        //四元数和向量相乘得到新的向量 q * v = （q） *（ v） *（ q−1）；
        public static Vector3D MutlipyVector3D(Quaternion quaternion,Vector3D vec3)
        {
            quaternion.Normalize();
            Quaternion quaternionInverse = new Quaternion();
            quaternionInverse.w = quaternion.w;
            quaternionInverse.x = quaternion.x;
            quaternionInverse.y = quaternion.y;
            quaternionInverse.z = quaternion.z;
            quaternionInverse.Inverse();

            //向量转四元数
            Quaternion qVector = new Quaternion();
            qVector.w = 0;
            qVector.x = vec3.x;
            qVector.y = vec3.y;
            qVector.z = vec3.z;

            Quaternion q = Quaternion.Mutlipy(quaternion, qVector);
            q = Quaternion.Mutlipy(q, quaternionInverse);

            Vector3D ret = new Vector3D(q.x,q.y,q.z);
            return ret;

        }
    }
}

