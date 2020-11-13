using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoftRender
{
    public class Mesh
    {
        public Vertex[] vertices;
        public int[] indices;

        public void LoadFile(string path)
        {
            UnityEngine.GameObject obj = Resources.Load(path) as UnityEngine.GameObject;
            UnityEngine.Mesh umesh = obj.GetComponent<MeshFilter>().sharedMesh;

            vertices = new Vertex[umesh.vertices.Length];
            indices = new int[umesh.triangles.Length];
            for (int i=0;i< umesh.vertices.Length;i++)
            {
                UnityEngine.Vector3 vector = umesh.vertices[i];
                UnityEngine.Vector3 normal = umesh.normals[i];
                UnityEngine.Vector2 texcoord = umesh.uv[i];
                UnityEngine.Color color = umesh.colors[i];
                Vertex vertex = new Vertex();
                vertex.position = new Vector4D(vector.x, vector.y, vector.z, 1);
                vertex.normal = new Vector3D(normal.x, normal.y, normal.z);
                vertex.texcoord = new Vector2D(texcoord.x, texcoord.y);
                vertex.color = new Color(color.r, color.g, color.b, color.a);
                vertices[i] = vertex;
            }

            for(int i = 0;i< umesh.triangles.Length;i++)
            {
                indices[i] = umesh.triangles[i];
            }
            //Debug.LogError("obj 1111  " + obj);
        }

        public void AsTriangle()
        {
            float width = 1;

            vertices = new Vertex[3];
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = new Vertex();
            }

            vertices[0].position = new Vector4D(width, 0, 0, 1f);
            vertices[0].normal = new Vector3D(0.0f, 0.0f, 1.0f);
            vertices[0].color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            vertices[0].texcoord = new Vector2D(0f, 0f);

            vertices[1].position = new Vector4D(0, Mathf.Sqrt(3) * width, 0, 1f);
            vertices[1].normal = new Vector3D(0.0f, 0.0f, 1.0f);
            vertices[1].color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
            vertices[1].texcoord = new Vector2D(0.5f, 1.0f);

            vertices[2].position = new Vector4D(-width, 0, 0, 1f);
            vertices[2].normal = new Vector3D(0.0f, 0.0f, 1.0f);
            vertices[2].color = new Color(0.0f, 0.0f, 1.0f, 1.0f);
            vertices[2].texcoord = new Vector2D(0.5f, 1.0f);

            indices = new int[3];
            indices[0] = 0;
            indices[1] = 1;
            indices[2] = 2;

        }

        public void AsGround()
        {
            float width = 6;

            vertices = new Vertex[4];
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = new Vertex();
            }
            indices = new int[6];
            indices[0] = 0;
            indices[1] = 2;
            indices[2] = 1;
            indices[3] = 1;
            indices[4] = 2;
            indices[5] = 3;

            vertices[0].position = new Vector4D(-2 * width, 0, -width, 1);
            vertices[0].normal = new Vector3D(0, 1, 0);
            vertices[0].color = new Color(1, 0, 0, 1);
            vertices[0].texcoord = new Vector2D(0, 0);

            vertices[1].position = new Vector4D(2 * width, 0, -width, 1);
            vertices[1].normal = new Vector3D(0, 1, 0);
            vertices[1].color = new Color(0, 1, 0, 1);
            vertices[1].texcoord = new Vector2D(1, 0);

            vertices[2].position = new Vector4D(-2 * width, 0, width, 1);
            vertices[2].normal = new Vector3D(0, 1, 0);
            vertices[2].color = new Color(0, 0, 1, 1);
            vertices[2].texcoord = new Vector2D(0, 1);

            vertices[3].position = new Vector4D(2 * width, 0, width, 1);
            vertices[3].normal = new Vector3D(0, 1, 0);
            vertices[3].color = new Color(1, 1, 1, 1);
            vertices[3].texcoord = new Vector2D(1, 1);

        }

        public void AsCube()
        {
            vertices = new Vertex[24];
            for(int i=0;i< vertices.Length;i++)
            {
                vertices[i] = new Vertex();
            }
            indices = new int[36];

            float halfW = 1 * 0.5f;
            float halfH = 1 * 0.5f;
            float halfD = 1 * 0.5f;
            //front
            vertices[0].position = new Vector4D(halfW, halfH, halfD, 1f);
            vertices[0].normal = new Vector3D(0.0f, 0.0f, 1.0f);
            vertices[0].color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            vertices[0].texcoord = new Vector2D(1.0f, 1.0f);

            vertices[1].position = new Vector4D(-halfW, halfH, halfD,1f);
            vertices[1].normal = new Vector3D(0.0f, 0.0f, 1.0f);
            vertices[1].color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
            vertices[1].texcoord = new Vector2D(0.0f, 1.0f);

            vertices[2].position = new Vector4D(-halfW, -halfH, halfD, 1f);
            vertices[2].normal = new Vector3D(0.0f, 0.0f, 1.0f);
            vertices[2].color = new Color(0.0f, 0.0f, 1.0f, 1.0f);
            vertices[2].texcoord = new Vector2D(0.0f, 0.0f);

            vertices[3].position = new Vector4D(halfW, -halfH, halfD, 1f);
            vertices[3].normal = new Vector3D(0.0f, 0.0f, 1.0f);
            vertices[3].color = new Color(0.0f, 1.0f, 1.0f, 1.0f);
            vertices[3].texcoord = new Vector2D(1.0f, 0.0f);
            //left
            vertices[4].position = new Vector4D(-halfW, +halfH, halfD, 1f);
            vertices[4].normal = new Vector3D(-1.0f, 0.0f, 0.0f);
            vertices[4].color = new Color(0.0f, 0.0f, 1.0f, 1.0f);
            vertices[4].texcoord = new Vector2D(1.0f, 1.0f);

            vertices[5].position = new Vector4D(-halfW, +halfH, -halfD, 1f);
            vertices[5].normal = new Vector3D(-1.0f, 0.0f, 0.0f);
            vertices[5].color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
            vertices[5].texcoord = new Vector2D(0.0f, 1.0f);

            vertices[6].position = new Vector4D(-halfW, -halfH, -halfD, 1f);
            vertices[6].normal = new Vector3D(-1.0f, 0.0f, 0.0f);
            vertices[6].color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
            vertices[6].texcoord = new Vector2D(0.0f, 0.0f);

            vertices[7].position = new Vector4D(-halfW, -halfH, halfD, 1f);
            vertices[7].normal = new Vector3D(-1.0f, 0.0f, 0.0f);
            vertices[7].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            vertices[7].texcoord = new Vector2D(1.0f, 0.0f);
            //back
            vertices[8].position = new Vector4D(-halfW, +halfH, -halfD, 1f);
            vertices[8].normal = new Vector3D(0.0f, 0.0f, -1.0f);
            vertices[8].color = new Color(1.0f, 0.0f, 1.0f, 1.0f);
            vertices[8].texcoord = new Vector2D(0.0f, 0.0f);

            vertices[9].position = new Vector4D(+halfW, +halfH, -halfD, 1f);
            vertices[9].normal = new Vector3D(0.0f, 0.0f, -1.0f);
            vertices[9].color = new Color(0.0f, 1.0f, 1.0f, 1.0f);
            vertices[9].texcoord = new Vector2D(1.0f, 0.0f);

            vertices[10].position = new Vector4D(+halfW, -halfH, -halfD, 1f);
            vertices[10].normal = new Vector3D(0.0f, 0.0f, -1.0f);
            vertices[10].color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
            vertices[10].texcoord = new Vector2D(1.0f, 1.0f);

            vertices[11].position = new Vector4D(-halfW, -halfH, -halfD, 1f);
            vertices[11].normal = new Vector3D(0.0f, 0.0f, -1.0f);
            vertices[11].color = new Color(0.0f, 0.0f, 1.0f, 1.0f);
            vertices[11].texcoord = new Vector2D(0.0f, 1.0f);
            //right
            vertices[12].position = new Vector4D(halfW, +halfH, -halfD, 1f);
            vertices[12].normal = new Vector3D(1.0f, 0.0f, 0.0f);
            vertices[12].color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
            vertices[12].texcoord = new Vector2D(0.0f, 0.0f);

            vertices[13].position = new Vector4D(halfW, +halfH, +halfD, 1f);
            vertices[13].normal = new Vector3D(1.0f, 0.0f, 0.0f);
            vertices[13].color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            vertices[13].texcoord = new Vector2D(1.0f, 0.0f);

            vertices[14].position = new Vector4D(halfW, -halfH, +halfD, 1f);
            vertices[14].normal = new Vector3D(1.0f, 0.0f, 0.0f);
            vertices[14].color = new Color(0.0f, 1.0f, 1.0f, 1.0f);
            vertices[14].texcoord = new Vector2D(1.0f, 1.0f);

            vertices[15].position = new Vector4D(halfW, -halfH, -halfD, 1f);
            vertices[15].normal = new Vector3D(1.0f, 0.0f, 0.0f);
            vertices[15].color = new Color(1.0f, 0.0f, 1.0f, 1.0f);
            vertices[15].texcoord = new Vector2D(0.0f, 1.0f);
            //top
            vertices[16].position = new Vector4D(+halfW, halfH, -halfD, 1f);
            vertices[16].normal = new Vector3D(0.0f, 1.0f, 0.0f);
            vertices[16].color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
            vertices[16].texcoord = new Vector2D(0.0f, 0.0f);

            vertices[17].position = new Vector4D(-halfW, halfH, -halfD, 1f);
            vertices[17].normal = new Vector3D(0.0f, 1.0f, 0.0f);
            vertices[17].color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
            vertices[17].texcoord = new Vector2D(1.0f, 0.0f);

            vertices[18].position = new Vector4D(-halfW, halfH, halfD, 1f);
            vertices[18].normal = new Vector3D(0.0f, 1.0f, 0.0f);
            vertices[18].color = new Color(0.0f, 1.0f, 1.0f, 1.0f);
            vertices[18].texcoord = new Vector2D(1.0f, 1.0f);

            vertices[19].position = new Vector4D(+halfW, halfH, halfD, 1f);
            vertices[19].normal = new Vector3D(0.0f, 1.0f, 0.0f);
            vertices[19].color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            vertices[19].texcoord = new Vector2D(0.0f, 1.0f);
            //down
            vertices[20].position = new Vector4D(+halfW, -halfH, -halfD, 1f);
            vertices[20].normal = new Vector3D(0.0f, -1.0f, 0.0f);
            vertices[20].color = new Color(0.0f, 0.0f, 1.0f, 1.0f);
            vertices[20].texcoord = new Vector2D(0.0f, 0.0f);

            vertices[21].position = new Vector4D(+halfW, -halfH, +halfD, 1f);
            vertices[21].normal = new Vector3D(0.0f, -1.0f, 0.0f);
            vertices[21].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            vertices[21].texcoord = new Vector2D(1.0f, 0.0f);

            vertices[22].position = new Vector4D(-halfW, -halfH, +halfD, 1f);
            vertices[22].normal = new Vector3D(0.0f, -1.0f, 0.0f);
            vertices[22].color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
            vertices[22].texcoord = new Vector2D(1.0f, 1.0f);

            vertices[23].position = new Vector4D(-halfW, -halfH, -halfD, 1f);
            vertices[23].normal = new Vector3D(0.0f, -1.0f, 0.0f);
            vertices[23].color = new Color(1.0f, 0.0f, 1.0f, 1.0f);
            vertices[23].texcoord = new Vector2D(0.0f, 1.0f);

            //front
            indices[0] = 0;
            indices[1] = 1;
            indices[2] = 2;
            indices[3] = 0;
            indices[4] = 2;
            indices[5] = 3;
            //left
            indices[6] = 4;
            indices[7] = 5;
            indices[8] = 6;
            indices[9] = 4;
            indices[10] = 6;
            indices[11] = 7;
            //back
            indices[12] = 8;
            indices[13] = 9;
            indices[14] = 10;
            indices[15] = 8;
            indices[16] = 10;
            indices[17] = 11;
            //right
            indices[18] = 12;
            indices[19] = 13;
            indices[20] = 14;
            indices[21] = 12;
            indices[22] = 14;
            indices[23] = 15;
            //top
            indices[24] = 16;
            indices[25] = 17;
            indices[26] = 18;
            indices[27] = 16;
            indices[28] = 18;
            indices[29] = 19;
            //down
            indices[30] = 20;
            indices[31] = 21;
            indices[32] = 22;
            indices[33] = 20;
            indices[34] = 22;
            indices[35] = 23;
        }
    }
}

