using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SoftRender
{
    public class VertexOut
    {
        public Vector4D posWorld;
        public Vector4D posProjective;
        public Vector2D texcoord;
        public Vector3D normal;
        public Color color;
        public float oneDivZ;//用于透视纹理处理
    }
}
