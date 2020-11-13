using UnityEngine;


namespace SoftRender
{
    public enum PolygonMode
    {
        Wire,//线框模式
        Fill,//填充模式
    }

    public class Pipeline
    {
        public Scene scene;
        public FrameBuffer frontBuffer;
        public FrameBuffer backBuffer;
        private PolygonMode polygonMode = PolygonMode.Wire;
        private Matrix4x4 projectMatrix;

        //视图空间矩阵
        private Matrix4x4 viewMatrix;

        private float deltaTime = 1.0f;//多久渲染一次
        private float calTime = 0;
        private Matrix4x4 viewPortMatrix;
        private Vector3D cameraPosition;

        public Pipeline()
        {
            frontBuffer = new FrameBuffer();
            frontBuffer.SetWidthAndHeight(Global.screenWidth, Global.screenHeight);
            backBuffer = new FrameBuffer();
            backBuffer.SetWidthAndHeight(Global.screenWidth, Global.screenHeight);
            viewPortMatrix = new Matrix4x4();
            viewPortMatrix.SetViewPort(0, 0, Global.screenWidth, Global.screenHeight);
            viewMatrix = new Matrix4x4();
        }

        public void SetProjectMatrix(Matrix4x4 matrix)
        {
            projectMatrix = matrix;
        }

        public void SetPolygonMode(PolygonMode polygonMode)
        {
            this.polygonMode = polygonMode;
        }

        public void Render()
        {

            ////测试画线函数------>>>>>>
            //VertexOut pA = new VertexOut();
            //pA.posProjective = new Vector4D(100,0,0,1);

            //VertexOut pB = new VertexOut();
            //pB.posProjective = new Vector4D(100, 100, 0, 1);

            //BresenhamLineRasterization(pA, pB);

            //return;
            ////测试画线函数<<<<<<<--------
            /// 
            Vector3D tmp = scene.camera.transform.GetPosition();
            cameraPosition = new Vector3D(tmp.x, tmp.y, tmp.z);
            //cameraPosition.x = -cameraPosition.x;
            //cameraPosition.y = -cameraPosition.y;
            //cameraPosition.z = -cameraPosition.z;

            Vector3D up = new Vector3D(0, 1, 0);
            //up.x = -up.x;
            //up.y = -up.y;
            //up.z = -up.z;
            viewMatrix.SetLookAt(cameraPosition, new Vector3D(0,0,0), up);

            backBuffer.Clear();
            //scene.camera
            for (int i=0;i<scene.gameObjects.Count;i++)
            {
                GameObject3D gameObject = scene.gameObjects[i];
                BaseShader shader = gameObject.meshRenderer.material.shader;
                shader.SetModelMatrix(gameObject.transform.ToMatrix());

                shader.SetViewMatrix(viewMatrix);
                shader.SetEyePos(cameraPosition);
                shader.SetProjectMatrix(projectMatrix);
                shader.SetLight(scene.light);

                //图元装配
                Mesh mesh = gameObject.meshRenderer.mesh;

                //Debug.LogError("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");

                for(int j=0;j<mesh.indices.Length;j+=3)
                {

                    Vertex p1, p2, p3;
                    p1 = mesh.vertices[mesh.indices[j]];
                    p2 = mesh.vertices[mesh.indices[j + 1]];
                    p3 = mesh.vertices[mesh.indices[j + 2]];

                    //顶点着色(遍历三角形)
                    VertexOut vertexOut1, vertexOut2, vertexOut3;
                    vertexOut1 = shader.VertexShader(p1);
                    vertexOut2 = shader.VertexShader(p2);
                    vertexOut3 = shader.VertexShader(p3);

                    shader.ProjectedMap(vertexOut1);
                    shader.ProjectedMap(vertexOut2);
                    shader.ProjectedMap(vertexOut3);
                    //背面裁剪
                    if(shader.backFaceCulling)
                    {
                        if(polygonMode != PolygonMode.Wire && !BackFaceCulling(vertexOut1.posWorld, vertexOut2.posWorld, vertexOut3.posWorld))
                        {
                            continue;
                        }
                    }

                    //透视处理
                    PerspectiveDivision(vertexOut1);
                    PerspectiveDivision(vertexOut2);
                    PerspectiveDivision(vertexOut3);

                    //几何裁剪

                    //屏幕空间转换
                    vertexOut1.posProjective = Matrix4x4.MultiplyVector4D(viewPortMatrix, vertexOut1.posProjective);
                    vertexOut2.posProjective = Matrix4x4.MultiplyVector4D(viewPortMatrix, vertexOut2.posProjective);
                    vertexOut3.posProjective = Matrix4x4.MultiplyVector4D(viewPortMatrix, vertexOut3.posProjective);

                    //Debug.LogError("vertexOut1.posProjective " + vertexOut1.posProjective.x 
                    //    + " " + vertexOut1.posProjective.y + " " + vertexOut1.posProjective.z );
                    //Debug.LogError("vertexOut2.posProjective " + vertexOut2.posProjective.x
                    //    + " " + vertexOut2.posProjective.y + " " + vertexOut2.posProjective.z );
                    //Debug.LogError("vertexOut3.posProjective " + vertexOut3.posProjective.x
                    //    + " " + vertexOut3.posProjective.y + " " + vertexOut3.posProjective.z );
                    //Debug.LogError("-----------------");

                    //片段着色
                    if (polygonMode == PolygonMode.Wire)
                    {
                        //线框模式
                        BresenhamLineRasterization(vertexOut1, vertexOut2);
                        BresenhamLineRasterization(vertexOut1, vertexOut3);
                        BresenhamLineRasterization(vertexOut2, vertexOut3);
                    }
                    else
                    {
                        //三角形填充模式
                        EdgeWalkingFillRasterization(vertexOut1, vertexOut2, vertexOut3,shader);
                    }
                }
            }
        }

        private bool BackFaceCulling(Vector4D v1, Vector4D v2, Vector4D v3)
        {
            Vector4D tmp1 = Vector4D.Minus(v2, v1);
            Vector4D tmp2 = Vector4D.Minus(v3, v1);

            Vector3D edge1 = new Vector3D(tmp1.x, tmp1.y, tmp1.z);
            Vector3D edge2 = new Vector3D(tmp2.x, tmp2.y, tmp2.z);
            Vector3D viewRay = new Vector3D(cameraPosition.x - v1.x, cameraPosition.y - v2.y, cameraPosition.z - v3.z);
            Vector3D normal = Vector3D.Cross(edge1, edge2);

            return Vector3D.Dot(normal, viewRay) > 0;
        }

        private VertexOut LerpVertexOut(VertexOut v1, VertexOut v2,float weight)
        {
            VertexOut vertexOut = new VertexOut();
            vertexOut.posProjective = Vector4D.Lerp(v1.posProjective, v2.posProjective,weight);
            vertexOut.posWorld = Vector4D.Lerp(v1.posWorld, v2.posWorld, weight);
            vertexOut.color = Color.Lefp(v1.color, v2.color, weight);
            vertexOut.normal = Vector3D.Lerp(v1.normal, v2.normal, weight);
            vertexOut.texcoord = Vector2D.Lerp(v1.texcoord, v2.texcoord,weight);
            vertexOut.oneDivZ = (1 - weight) * v1.oneDivZ + weight * v2.oneDivZ;
            return vertexOut;
        }

        //按列逐行扫描
        private void ScanLinePerRow(VertexOut left, VertexOut right,BaseShader shader)
        {
            VertexOut current;
            int length = (int)right.posProjective.x - (int)left.posProjective.x;
            if(length == 0)
            {
                return;
            }
            for(int i=0;i<=length;i++)
            {
                float weight = (float)i / length;
                current = LerpVertexOut(left, right,weight);

                if(shader.ZTest == true)
                {
                    if(current.posProjective.z >= 0.5f 
                        && current.posProjective.z > backBuffer.GetDepth((int)current.posProjective.x, (int)current.posProjective.y))
                    {
                        continue;
                    }
                }

                if(shader.ZWrite == true)
                {
                    backBuffer.DrawDepth((int)current.posProjective.x, (int)current.posProjective.y, current.posProjective.z);
                }

                current.posProjective.x = left.posProjective.x + i;
                current.posProjective.y = left.posProjective.y;

                //透视纹理处理
                shader.ProjectedMapRestore(current);

                //片段着色
                backBuffer.DrawPixel((int)current.posProjective.x, (int)current.posProjective.y, shader.FragmentShader(current));

            }
        }

        //从小到大输入
        private void RasterTopTriangle(VertexOut v1, VertexOut v2, VertexOut v3,BaseShader shader)
        {
            VertexOut left = v2;
            VertexOut right = v3;
            VertexOut dest = v1;
            float weight = 0;
            VertexOut newLeft;
            VertexOut newRight;

            if (left.posProjective.x > right.posProjective.x)
            {
                VertexOut tmp = left;
                left = right;
                right = tmp;
            }

            int dy = (int)left.posProjective.y - (int)dest.posProjective.y + 1;
            for(int i=0;i<dy;i++)
            {
                if(dy != 0)
                {
                    weight = (float)i / dy;
                }

                newLeft = LerpVertexOut(left, dest, weight);
                newRight = LerpVertexOut(right, dest, weight);
                newLeft.posProjective.y = left.posProjective.y - i;
                newRight.posProjective.y = left.posProjective.y - i;
                ScanLinePerRow(newLeft, newRight, shader);
            }
        }

        //从小到大输入
        private void RasterBottomTriangle(VertexOut v1, VertexOut v2, VertexOut v3,BaseShader shader)
        {
            VertexOut left = v1;
            VertexOut right = v2;
            VertexOut dest = v3;
            float weight = 0;
            VertexOut newLeft;
            VertexOut newRight;

            if (left.posProjective.x > right.posProjective.x)
            {
                VertexOut tmp = left;
                left = right;
                right = tmp;
            }

            int dy = (int)dest.posProjective.y - (int)left.posProjective.y + 1;
            for (int i = 0; i < dy; i++)
            {
                if (dy != 0)
                {
                    weight = (float)i / dy;
                }
                newLeft = LerpVertexOut(left, dest, weight);
                newRight = LerpVertexOut(right, dest, weight);
                newLeft.posProjective.y = left.posProjective.y + i;
                newRight.posProjective.y = left.posProjective.y + i;
                ScanLinePerRow(newLeft, newRight, shader);
            }
        }


        private void EdgeWalkingFillRasterization(VertexOut v1, VertexOut v2, VertexOut v3, BaseShader shader)
        {
            //先把三个点按y从小到大排序
            VertexOut[] targets = { v1,v2,v3};
            VertexOut tmp;
            if (targets[0].posProjective.y > targets[1].posProjective.y)
            {
                tmp = targets[0];
                targets[0] = targets[1];
                targets[1] = tmp;
            }
            if (targets[0].posProjective.y > targets[2].posProjective.y)
            {
                tmp = targets[0];
                targets[0] = targets[2];
                targets[2] = tmp;
            }
            if (targets[1].posProjective.y > targets[2].posProjective.y)
            {
                tmp = targets[1];
                targets[1] = targets[2];
                targets[2] = tmp;
            }

            if((int)targets[0].posProjective.y == (int)targets[1].posProjective.y)
            {
                //下三角形
                RasterBottomTriangle(targets[0], targets[1], targets[2], shader);
            }
            else if((int)targets[1].posProjective.y == (int)targets[2].posProjective.y)
            {
                //上三角形
                RasterTopTriangle(targets[0], targets[1], targets[2], shader);
            }
            else
            {
                //需要分割为上三角形下三角形
                float weight = (targets[1].posProjective.y - targets[0].posProjective.y) 
                    / (targets[2].posProjective.y - targets[0].posProjective.y);
                    
                VertexOut vertexOutNew = LerpVertexOut(targets[0], targets[2],weight);
                vertexOutNew.posProjective.y = targets[1].posProjective.y;

                RasterTopTriangle(targets[0], vertexOutNew, targets[1], shader);
                RasterBottomTriangle(vertexOutNew, targets[1], targets[2], shader);
            }
        }


        //Bresenham画线法 用整数加减法代替浮点数加减法
        private void BresenhamLineRasterization(VertexOut from, VertexOut to)
        {
            int x = (int)from.posProjective.x;
            int y = (int)from.posProjective.y;

            int toX = (int)to.posProjective.x;
            int toY = (int)to.posProjective.y;

            int deltax = toX - x;
            int deltay = toY - y;

            int stepx = 1;
            int stepy = 1;
            if(deltax < 0)
            {
                stepx = -1;
            }
            if(deltay < 0)
            {
                stepy = -1;
            }

            int eps = 0;//误差累计

            deltax = Mathf.Abs(deltax);
            deltay = Mathf.Abs(deltay);

            if (deltax > deltay)
            {
                for (; x != toX; x += stepx)
                {
                    eps += deltay;
                    if ((eps << 1) >= deltax)
                    {
                        y += stepy;
                        eps -= deltax;
                    }
                    backBuffer.DrawPixel(x, y, Color.White());
                }
            }
            else
            {
                for (; y != toY; y += stepy)
                {
                    eps += deltax;
                    if ((eps << 1) >= deltay)
                    {
                        x += stepx;
                        eps -= deltay;
                    }
                    backBuffer.DrawPixel(x, y, Color.White());
                }
            }

        }

        private void PerspectiveDivision(VertexOut vertexOut)
        {
            vertexOut.posProjective.x /= vertexOut.posProjective.w;
            vertexOut.posProjective.y /= vertexOut.posProjective.w;
            vertexOut.posProjective.z /= vertexOut.posProjective.w;
            vertexOut.posProjective.w = 1;
            //[-1,1] to [0,1]
            vertexOut.posProjective.z = (vertexOut.posProjective.z + 1.0f) * 0.5f;
        }

        public void SwapBuffer()
        {
            //frontBuffer = backBuffer;
        }

        public void Update()
        {
            calTime += Time.deltaTime;
            if (calTime > deltaTime)
            {
                calTime = 0;
            }
        }
    }
}

