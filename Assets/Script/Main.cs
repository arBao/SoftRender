using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using SoftRender;
namespace SoftRender
{
    public class Main : MonoBehaviour
    {

        public RawImage rawImage;
        public UnityEngine.Texture2D texture;
        private Pipeline pipeline;
        private float perTime = 100f;//多久渲染一次
        private float calTime = 100f;

        // Use this for initialization
        void Start()
        {
			//for (int w = 0; w < Global.screenWidth; w++)
			//{
			//    for (int h = 0; h < Global.screenHeight; h++)
			//    {
			//        if (h > Global.screenHeight / 2 && h < Global.screenHeight / 2 + 50)
			//            texture.SetPixel(w, h,UnityEngine.Color.yellow);
			//        else
			//            texture.SetPixel(w, h, UnityEngine.Color.green);
			//    }
			//}

			////Debug.LogError(UnityEngine.Color.green.r + " " + UnityEngine.Color.green.g + " " + UnityEngine.Color.green.b + " " + UnityEngine.Color.green.a);

			//texture.Apply();

			/*
            //四元数转欧拉角
            Quaternion quaternion = new Quaternion();
            //(w,x,y,z)=(0.653,-0.271,0.653,0.271)
            quaternion.w = 0.653f;
            quaternion.x = -0.271f;
            quaternion.y = 0.653f;
            quaternion.z = 0.271f;
            Vector3D vector = quaternion.ToEular();
            Debug.LogError("111 从四元数转到欧拉角 vector.x " + Mathf.Rad2Deg * vector.x + " vector.y " + Mathf.Rad2Deg * vector.y + " vector.z " + Mathf.Rad2Deg * vector.z);

            //欧拉角转四元数
            quaternion = new Quaternion();
            quaternion.FromEular(vector);
            Debug.LogError("111 从欧拉角转回四元数 w " + quaternion.w + " x " + quaternion.x + " y " + quaternion.y + " z " + quaternion.z);

            //(10,20,30)
            vector.x = 10f * Mathf.Deg2Rad;
            vector.y = 10f * Mathf.Deg2Rad;
            vector.z = 10f * Mathf.Deg2Rad;

            quaternion = new Quaternion();
            quaternion.FromEular(vector);
            Debug.LogError("222 自定义欧拉角转四元数 w " + quaternion.w + " x " + quaternion.x + " y " + quaternion.y + " z " + quaternion.z);

            vector = quaternion.ToEular();
            Debug.LogError("222 自定义的欧拉角生成的四元数打印 vector.x " + Mathf.Rad2Deg * vector.x + " vector.y " + Mathf.Rad2Deg * vector.y + " vector.z " + Mathf.Rad2Deg * vector.z);

            Matrix4x4 matrix = quaternion.ToMatrix();
            quaternion = new Quaternion();
            quaternion.FromMatrix(matrix);

            vector = quaternion.ToEular();
            Debug.LogError("333 四元数转矩阵，在用矩阵转四元数 vector.x " + Mathf.Rad2Deg * vector.x + " vector.y " + Mathf.Rad2Deg * vector.y + " vector.z " + Mathf.Rad2Deg * vector.z);

            quaternion = new Quaternion();
            quaternion.FromEular(new Vector3D(0, 0, 10 * Mathf.Deg2Rad));
            quaternion = Quaternion.Mutlipy(quaternion, quaternion);
            quaternion = Quaternion.Mutlipy(quaternion, quaternion);
            quaternion = Quaternion.Mutlipy(quaternion, quaternion);
            vector = quaternion.ToEular();
            Debug.LogError("444 四元数相乘 vector.x " + Mathf.Rad2Deg * vector.x + " vector.y " + Mathf.Rad2Deg * vector.y + " vector.z " + Mathf.Rad2Deg * vector.z);

            Quaternion quaternion1 = new Quaternion();
            quaternion1.FromEular(new Vector3D(0, 0, 10 * Mathf.Deg2Rad));
            quaternion1.Conjugate();
            quaternion = Quaternion.Mutlipy(quaternion, quaternion1);
            vector = quaternion.ToEular();
            Debug.LogError("555 取共轭四元数求反 vector.x " + Mathf.Rad2Deg * vector.x + " vector.y " + Mathf.Rad2Deg * vector.y + " vector.z " + Mathf.Rad2Deg * vector.z);

            quaternion = Quaternion.Delta(quaternion, quaternion1);
            vector = quaternion.ToEular();
            Debug.LogError("666 两个四元数间的差距为 vector.x " + Mathf.Rad2Deg * vector.x + " vector.y " + Mathf.Rad2Deg * vector.y + " vector.z " + Mathf.Rad2Deg * vector.z);

            quaternion1 = Quaternion.Pow(quaternion1, 0.2f);
            quaternion = Quaternion.Mutlipy(quaternion, quaternion1);
            vector = quaternion.ToEular();
            Debug.LogError("777 四元数求幂后 vector.x " + Mathf.Rad2Deg * vector.x + " vector.y " + Mathf.Rad2Deg * vector.y + " vector.z " + Mathf.Rad2Deg * vector.z);

            quaternion = new Quaternion();
            quaternion.FromEular(new Vector3D(0, 0, 10 * Mathf.Deg2Rad));

            quaternion1 = new Quaternion();
            quaternion1.FromEular(new Vector3D(0, 0, 50 * Mathf.Deg2Rad));

            quaternion = Quaternion.Slerp(quaternion, quaternion1, 0.5f);
            vector = quaternion.ToEular();
            Debug.LogError("888 求插值后 vector.x " + Mathf.Rad2Deg * vector.x + " vector.y " + Mathf.Rad2Deg * vector.y + " vector.z " + Mathf.Rad2Deg * vector.z);

            quaternion = new Quaternion();
            quaternion.FromEular(new Vector3D(0, 90 * Mathf.Deg2Rad, 0));
            vector = new Vector3D(0, 0, 10);//z轴的向量绕y轴旋转90度
            vector = Quaternion.MutlipyVector3D(quaternion, vector);

            Debug.LogError("999 向量和四元数相乘 vector.x " + vector.x + " vector.y " +  vector.y + " vector.z " + vector.z);

	*/


			texture = new UnityEngine.Texture2D(Global.screenWidth, Global.screenHeight);
			rawImage.texture = texture;

			//Texture2D texture2D = new Texture2D();
			//         texture2D.Load("wolf/STANDARD_WOLF_A");

			//         System.Diagnostics.Stopwatch getTime = new System.Diagnostics.Stopwatch();
			//         getTime.Start();
			//         UnityEngine.Color color = UnityEngine.Color.white;

			//         for (int h = 0; h < Global.screenHeight; h++)
			//         {
			//             for (int w = 0; w < Global.screenWidth; w++)
			//             {
			//                 float u = ((float)w) / Global.screenWidth;
			//                 float v = ((float)h) / Global.screenHeight;
			//                 Color colorData = texture2D.Sample(u, v);
			//                 color.r = colorData.r;
			//                 color.g = colorData.g;
			//                 color.b = colorData.b;
			//                 color.a = colorData.a;

			//                 texture.SetPixel(w, h, color);
			//             }
			//         }

			//         getTime.Stop();

			//         Debug.LogError("getTime:" + getTime.ElapsedMilliseconds.ToString());

			//texture.Apply();


			pipeline = new Pipeline();
            Scene scene = new Scene();
            pipeline.scene = scene;

            ////正方体测试---->>>>>
            //GameObject3D gameObject3D = new GameObject3D();
            //MeshRenderer meshRenderer = new MeshRenderer();
            //meshRenderer.material = new Material();
            //meshRenderer.material.shader = new SimpleShader();
            //meshRenderer.material.texture = new Texture2D();
            //meshRenderer.material.texture.Load("box");
            //meshRenderer.material.shader.SetMainTexture(meshRenderer.material.texture);
            //meshRenderer.mesh = new Mesh();
            ////meshRenderer.mesh.AsCube();
            //meshRenderer.mesh.AsTriangle();
            //gameObject3D.meshRenderer = meshRenderer;
            //scene.AddGameObject(gameObject3D);
            ////<<<<<------正方体测试

            //模型测试1----->>>>>>
            GameObject3D gameObject3D = new GameObject3D();
            MeshRenderer meshRenderer = new MeshRenderer();
            meshRenderer.material = new Material();
            meshRenderer.material.shader = new SimpleShader();
            meshRenderer.material.texture = new Texture2D();
            meshRenderer.material.texture.Load("wolf/STANDARD_WOLF_A");
            meshRenderer.material.shader.SetMainTexture(meshRenderer.material.texture);
            meshRenderer.mesh = new Mesh();
            meshRenderer.mesh.LoadFile("wolf/wolfFBX");
            gameObject3D.meshRenderer = meshRenderer;
            scene.AddGameObject(gameObject3D);

            gameObject3D.transform.Translate(new Vector3D(0, 0, 0));
            gameObject3D.transform.RotateY(180);
            gameObject3D.transform.RotateX(-90);

            //模型测试<<<<<<<<-----

            //模型测试2----->>>>>>
            gameObject3D = new GameObject3D();
            meshRenderer = new MeshRenderer();
            meshRenderer.material = new Material();
            meshRenderer.material.shader = new SimpleShader();
            meshRenderer.material.texture = new Texture2D();
            meshRenderer.material.texture.Load("wolf/STANDARD_WOLF_A");
            meshRenderer.material.shader.SetMainTexture(meshRenderer.material.texture);
            meshRenderer.mesh = new Mesh();
            meshRenderer.mesh.LoadFile("wolf/wolfFBX");
            gameObject3D.meshRenderer = meshRenderer;
            scene.AddGameObject(gameObject3D);
            gameObject3D.transform.Translate(new Vector3D(2, 0, 0));
            gameObject3D.transform.RotateY(180);
            gameObject3D.transform.RotateX(-90);

            //模型测试<<<<<<<<-----

            //模型测试3----->>>>>>
            gameObject3D = new GameObject3D();
            meshRenderer = new MeshRenderer();
            meshRenderer.material = new Material();
            meshRenderer.material.shader = new SimpleShader();
            meshRenderer.material.texture = new Texture2D();
            meshRenderer.material.texture.Load("wolf/STANDARD_WOLF_A");
            meshRenderer.material.shader.SetMainTexture(meshRenderer.material.texture);
            meshRenderer.mesh = new Mesh();
            meshRenderer.mesh.LoadFile("wolf/wolfFBX");
            gameObject3D.meshRenderer = meshRenderer;
            scene.AddGameObject(gameObject3D);
            gameObject3D.transform.Translate(new Vector3D(-2, 0, 0));
            gameObject3D.transform.RotateY(180);
            gameObject3D.transform.RotateX(-90);

            //模型测试<<<<<<<<-----

            //地面----->>>>>>
            gameObject3D = new GameObject3D();
            meshRenderer = new MeshRenderer();
            meshRenderer.material = new Material();
            meshRenderer.material.shader = new SimpleShader();
            meshRenderer.material.texture = new Texture2D();
            meshRenderer.material.texture.Load("box");
            meshRenderer.material.shader.SetMainTexture(meshRenderer.material.texture);
            meshRenderer.mesh = new Mesh();
            meshRenderer.mesh.AsGround();
            gameObject3D.meshRenderer = meshRenderer;
            scene.AddGameObject(gameObject3D);
            gameObject3D.transform.Translate(new Vector3D(0, -4, 3));
            //gameObject3D.transform.RotateY(30);
            //模型测试<<<<<<<<-----

            //旋转角度试试
            //gameObject3D.transform.RotateX(-90);

            DirectionalLight directionalLight = new DirectionalLight();
            directionalLight.SetDirection(new Vector3D(-1, -1, 1), 0.3f);
            scene.light = directionalLight;

            scene.camera.transform.Translate(new Vector3D(0, 3, -5));
            //scene.camera.transform.Translate(new Vector3D(0,5,-10));
            //scene.camera.transform.RotateX(10);

            Matrix4x4 projectMatrix = new Matrix4x4();
            projectMatrix.SetPerspective(45f, ((float)Global.screenWidth) / Global.screenHeight, 0.01f, 40f);
            pipeline.SetProjectMatrix(projectMatrix);
            pipeline.SetPolygonMode(PolygonMode.Fill);
            //pipeline.SetPolygonMode(PolygonMode.Wire);

            //TimerManager.Instance.CallActionDelay((obj) => {
            //    gameObject3D.transform.RotateY(30);
            //}, 1, null, -1, 1, true);


            //测试FrameBuffer-------------》》》》》》
            //FrameBuffer frameBuffer = new FrameBuffer();
            //frameBuffer.SetWidthAndHeight(Global.screenWidth, Global.screenHeight);
            //for (int h = 0; h < Global.screenHeight; h++)
            //{
            //    for (int w = 0; w < Global.screenWidth; w++)
            //    {
            //        float u = ((float)w) / Global.screenWidth;
            //        float v = ((float)h) / Global.screenHeight;
            //        Color colorData = texture2D.Sample(u, v);
            //        frameBuffer.DrawPixel(w, h, colorData);
            //    }
            //}

            //for (int h = 0; h < Global.screenHeight; h++)
            //{
            //    for (int w = 0; w < Global.screenWidth; w++)
            //    {
            //        Color colorData = frameBuffer.GetColor(w, h);
            //        color.r = colorData.r;
            //        color.g = colorData.g;
            //        color.b = colorData.b;
            //        color.a = colorData.a;

            //        texture.SetPixel(w, h, color);
            //    }
            //}

            //texture.Apply();

            //测试FrameBuffer《《《《《《-------------

            //测试矩阵乘以向量------>>>>
            //matrix = new Matrix4x4();
            //Vector4D vector4D = new Vector4D(1, 2, 3, 4);
            //vector4D = Matrix4x4.MultiplyVector4D(matrix, vector4D);
            //Debug.LogError("x " + vector4D.x + " y " + vector4D.y + " z " + vector4D.z + " w " + vector4D.w);

            //测试矩阵乘以向量<<<<<<-------
            //UnityEngine.UI.InputField inputField;
        }



        void Update()
        {
            TimerManager.Instance.UpdateFunc(Time.deltaTime);

            calTime += Time.deltaTime;
            if (calTime > perTime)
            {
                calTime = 0;

                UnityEngine.Color color = UnityEngine.Color.white;
                pipeline.Render();

                for (int h = 0; h < Global.screenHeight; h++)
                {
                    for (int w = 0; w < Global.screenWidth; w++)
                    {
                        Color colorData = pipeline.backBuffer.GetColor(w, h);
                        color.r = colorData.r;
                        color.g = colorData.g;
                        color.b = colorData.b;
                        color.a = colorData.a;

                        texture.SetPixel(w, h, color);
                    }
                }
                texture.Apply();
            }


        }
    }
}

