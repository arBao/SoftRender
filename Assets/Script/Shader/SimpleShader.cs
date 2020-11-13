using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoftRender
{
    public class SimpleShader:BaseShader
    {

        public SimpleShader()
        {
            ambientColor = new Color(0.1f, 0, 0, 1);//Color.Blue();
            diffuseColor = Color.White();
            specularColor = Color.White();
            //ambientColor = Color.Black();
            //diffuseColor = Color.Black();
            //specularColor = Color.Black();
        }

        public override VertexOut VertexShader(Vertex vertexIn)
        {
            VertexOut vertexOut = new VertexOut();
            vertexOut.posWorld = Matrix4x4.MultiplyVector4D(modelMatrix, vertexIn.position);
            vertexOut.posProjective = Matrix4x4.MultiplyVector4D(viewMatrix, vertexOut.posWorld);
            vertexOut.posProjective = Matrix4x4.MultiplyVector4D(projectedMatrix, vertexOut.posProjective);

            vertexOut.color = vertexIn.color.Copy();
            vertexOut.normal = vertexIn.normal.Copy();

            vertexOut.normal = Matrix4x4.MultiplyVector4D(modelMatrixInverse, vertexOut.normal.ToVector4D()).ToVector3D();
            vertexOut.texcoord = vertexIn.texcoord.Copy();

            //vertexOut.oneDivZ = 1.0f / vertexOut.posProjective.w;
            //vertexOut.posWorld.MultiplyNum(vertexOut.oneDivZ);
            //vertexOut.texcoord.MultiplyNum(vertexOut.oneDivZ);
            //vertexOut.color.MultiplyNum(vertexOut.oneDivZ);

            return vertexOut;
        }

        public override Color FragmentShader(VertexOut vertexIn)
        {
            Color color = texture.Sample(vertexIn.texcoord.x, vertexIn.texcoord.y);
            //Color color = Color.Red();
            if (light != null)
            {
                if(light.GetType() == typeof(DirectionalLight))
                {
                    color = color * ((DirectionalLight)light).Lighting(
                        ambientColor, 
                        diffuseColor , 
                        specularColor, 
                        vertexIn.posWorld.ToVector3D(), 
                        vertexIn.normal, 
                        Vector3D.Minus(eyePos, vertexIn.posWorld));
                    color.a = 1;
                }
            }
            //return vertexIn.color;
            return color;
        }
    }
}