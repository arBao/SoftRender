using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoftRender
{
    public class Color
    {
        public Color()
        {

        }

        public static Color Black()
        {
            Color color = new Color();
            color.r = 0;
            color.g = 0;
            color.b = 0;
            color.a = 1;

            return color;
        }

        public static Color White()
        {
            Color color = new Color();
            color.r = 1;
            color.g = 1;
            color.b = 1;
            color.a = 1;

            return color;
        }

        public static Color Red()
        {
            Color color = new Color();
            color.r = 1;
            color.g = 0;
            color.b = 0;
            color.a = 1;

            return color;
        }

        public static Color Blue()
        {
            Color color = new Color();
            color.r = 0;
            color.g = 0;
            color.b = 1;
            color.a = 1;

            return color;
        }

        public void MultiplyNum(float num)
        {
            r *= num;
            g *= num;
            b *= num;
            a *= num;
        }

        public static Color MultiplyNum(Color color, float num)
        {
            Color ret = new Color();
            ret.r = color.r * num;
            ret.g = color.g * num;
            ret.b = color.b * num;
            ret.a = color.a * num;

            return ret;
        }

        public static Color operator * (Color color, float num)
        {
            Color ret = new Color();
            ret.r = color.r * num;
            ret.g = color.g * num;
            ret.b = color.b * num;
            ret.a = color.a * num;

            return ret;
        }

        public static Color operator *(Color color1, Color color2)
        {
            Color ret = new Color();
            ret.r = color1.r * color2.r;
            ret.g = color1.g * color2.g;
            ret.b = color1.b * color2.b;
            ret.a = color1.a * color2.a;

            return ret;
        }

        //public static Color Add(Color color1, Color color2)
        //{
        //    Color ret = new Color();
        //    ret.r = color1.r + color2.r;
        //    ret.g = color1.g + color2.g;
        //    ret.b = color1.b + color2.b;
        //    ret.a = color1.a + color2.a;

        //    return ret;
        //}

        public static Color operator +(Color color1, Color color2)
        {
            Color ret = new Color();
            ret.r = color1.r + color2.r;
            ret.g = color1.g + color2.g;
            ret.b = color1.b + color2.b;
            ret.a = color1.a + color2.a;

            return ret;
        }

        public Color Copy()
        {
            return new Color(r, g, b, a);
        }

        public static Color Lefp(Color color1, Color color2,float weight)
        {
            Color color = new Color();
            color.r = (1 - weight) * color1.r + weight * color2.r;
            color.g = (1 - weight) * color1.g + weight * color2.g;
            color.b = (1 - weight) * color1.b + weight * color2.b;
            color.a = (1 - weight) * color1.a + weight * color2.a;
            return color;
        }

        public Color(float r, float g, float b, float a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }
        public float r;
        public float g;
        public float b;
        public float a;
    }
}
