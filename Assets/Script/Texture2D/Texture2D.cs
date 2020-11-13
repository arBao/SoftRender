using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoftRender
{
    public class Texture2D
    {
        public Color[] colors;
        public int width;
        public int height;

        private float oneOver255 = 0.003921568627451f;

        public Texture2D()
        {

        }

        //采样 双线性插值 Bilinear Interpolation
        public Color Sample(float u,float v)
        {
            //Color ret1 = new Color();
            //return ret1;

            if (u > 1)
            {
                u = u - (int)u;
            }
            if(v > 1)
            {
                v = v - (int)v;
            }
            if( u < 0)
            {
                u = 1 + u - (int)u;
            }
            if (v < 0)
            {
                v = 1 + v - (int)v;
            }

            float indexUF = u * (width - 1);
            float indexVF = v * (height - 1);

            int indexU = (int)indexUF;
            int indexV = (int)indexVF;

            float lerpU = indexUF - indexU;
            float lerpV = indexVF - indexV;

            if(indexU + width * indexV >= colors.Length || indexU + width * indexV < 0)
            {
                Debug.LogError("indexU + width * indexV  " + indexU + width * indexV 
                    + " width " + width + " height " + height + " u " + u + " v " + v);
            }

            Color colorLeftTop = colors[indexU + width * indexV];
            Color colorRightTop;
            Color colorLeftBottom;
            Color colorRightBottom;
            if (indexU + 1 >= width)
            {
                colorRightTop = colorLeftTop;
            }
            else
            {
                colorRightTop = colors[indexU + 1 + width * indexV];
            }

            if(indexV + 1 >= height)
            {
                colorLeftBottom = colorLeftTop;
            }
            else
            {
                colorLeftBottom = colors[indexU + width * (indexV + 1)];
            }

            if(indexU + 1 >= width && indexV + 1 >= height)
            {
                colorRightBottom = colorLeftTop;
            }
            else if(indexU + 1 >= width && indexV + 1 < height)
            {
                colorRightBottom = colorLeftBottom;
            }
            else if(indexU + 1< width && indexV + 1 >= height)
            {
                colorRightBottom = colorRightTop;
            }
            else
            {
                colorRightBottom = colors[indexU + 1 + width * (indexV + 1)];
            }

            Color lerp1 = new Color();
            lerp1.r = colorLeftTop.r * (1 - lerpU) + colorRightTop.r * lerpU;
            lerp1.g = colorLeftTop.g * (1 - lerpU) + colorRightTop.g * lerpU;
            lerp1.b = colorLeftTop.b * (1 - lerpU) + colorRightTop.b * lerpU;
            lerp1.a = colorLeftTop.a * (1 - lerpU) + colorRightTop.a * lerpU;

            Color lerp2 = new Color();
            lerp2.r = colorLeftBottom.r * (1 - lerpU) + colorRightBottom.r * lerpU;
            lerp2.g = colorLeftBottom.g * (1 - lerpU) + colorRightBottom.g * lerpU;
            lerp2.b = colorLeftBottom.b * (1 - lerpU) + colorRightBottom.b * lerpU;
            lerp2.a = colorLeftBottom.a * (1 - lerpU) + colorRightBottom.a * lerpU;

            //if(lerp1.r > 0)
            //{
            //    Debug.Log("a");
            //}

            Color ret = new Color();
            ret.r = lerp1.r * (1 - lerpV) + lerp2.r * lerpV;
            ret.g = lerp1.g * (1 - lerpV) + lerp2.g * lerpV;
            ret.b = lerp1.b * (1 - lerpV) + lerp2.b * lerpV;
            ret.a = lerp1.a * (1 - lerpV) + lerp2.a * lerpV;

            //if (UnityEngine.Random.value > 0.99)
            //Debug.LogError("color.r " + ret.r + " " + ret.g + " " + ret.b + " " + ret.a + "  lerpV " + lerpV + "  lerpU  " + lerpU);

            return ret;
        }

        public void Load(string path)
        {
            UnityEngine.Texture2D texture = Resources.Load(path) as UnityEngine.Texture2D;
            UnityEngine.Color[] colorsRead = texture.GetPixels();
            colors = new Color[colorsRead.Length];
            for (int i=0;i< colorsRead.Length;i++)
            {
                Color color = new Color();
                color.r = colorsRead[i].r;
                color.g = colorsRead[i].g;
                color.b = colorsRead[i].b;
                color.a = colorsRead[i].a;
                colors[i] = color;

            }

            width = texture.width;
            height = texture.height;
        }
    }
}

