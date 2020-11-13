using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoftRender
{
    public class FrameBuffer
    {
        public Color[] colorBuffer;
        public float[] depthbuffer;
        public int width;
        public int height;

        public void SetWidthAndHeight(int width,int height)
        {
            this.width = width;
            this.height = height;

            colorBuffer = new Color[this.width * this.height];
            depthbuffer = new float[this.width * this.height];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    colorBuffer[i * width + j] = Color.Black();
                    depthbuffer[i * width + j] = 1f;
                }
            }
        }

        public void Clear()
        {
            //colorBuffer = new Color[this.width * this.height];
            //depthbuffer = new float[this.width * this.height];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    colorBuffer[i * width + j].r = 0;
                    colorBuffer[i * width + j].g = 0;
                    colorBuffer[i * width + j].b = 0;
                    colorBuffer[i * width + j].a = 1;
                    depthbuffer[i * width + j] = 1f;
                }
            }
        }

        public void DrawPixel(int x,int y,Color color)
        {
            if(x < width && y < height && x >=0 && y >=0)
                colorBuffer[y * width + x] = color;
        }

        public void DrawDepth(int x, int y,float depth)
        {
            if (x < width && y < height && x >= 0 && y >= 0)
                depthbuffer[y * width + x] = depth;
        }

        public Color GetColor(int x,int y)
        {
            return colorBuffer[y * width + x];
        }

        public float GetDepth(int x, int y)
        {
            if (x < width && y < height && x >= 0 && y >= 0)
                return depthbuffer[y * width + x];
            else
                return 0;
        }
    }
}