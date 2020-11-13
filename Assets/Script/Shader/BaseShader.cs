using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoftRender
{
    public class BaseShader
    {
        protected Texture2D texture;
        protected Matrix4x4 modelMatrix;//model->world
        protected Matrix4x4 modelMatrixInverse;
        protected Matrix4x4 viewMatrix;
        protected Matrix4x4 projectedMatrix;
        protected Light light;
        protected Vector3D eyePos;
        public bool backFaceCulling = true;

        protected Color ambientColor;
        protected Color diffuseColor;
        protected Color specularColor;

        public bool ZWrite = true;
        public bool ZTest = true;

        public virtual VertexOut VertexShader(Vertex vertexIn)
        {
            return null;
        }

        public virtual Color FragmentShader(VertexOut vertexIn)
        {
            return null;
        }

        public void SetMainTexture(Texture2D texture)
        {
            this.texture = texture;
        }

        public void SetModelMatrix(Matrix4x4 modelMatrix)
        {
            this.modelMatrix = modelMatrix;
            modelMatrixInverse = this.modelMatrix.Copy();
            modelMatrixInverse.InverseTranspose();
        }

        public void SetViewMatrix(Matrix4x4 viewMatrix)
        {
            this.viewMatrix = viewMatrix;
        }

        public void SetProjectMatrix(Matrix4x4 projectedMatrix)
        {
            this.projectedMatrix = projectedMatrix;
        }

        public void SetLight(Light light)
        {
            this.light = light;
        }

        public void SetEyePos(Vector3D eyePos)
        {
            this.eyePos = eyePos;
        }

        //处理透视纹理映射
        public void ProjectedMap(VertexOut vertexOut)
        {
            //处理透视纹理映射
            vertexOut.oneDivZ = 1.0f / vertexOut.posProjective.w;
            vertexOut.posWorld.MultiplyNum(vertexOut.oneDivZ);
            vertexOut.texcoord.MultiplyNum(vertexOut.oneDivZ);
            vertexOut.color.MultiplyNum(vertexOut.oneDivZ);
        }

        public void ProjectedMapRestore(VertexOut vertexOut)
        {
            float w = 1.0f / vertexOut.oneDivZ;
            vertexOut.posWorld.MultiplyNum(w);
            vertexOut.texcoord.MultiplyNum(w);
            vertexOut.color.MultiplyNum(w);
        }

    }
}

