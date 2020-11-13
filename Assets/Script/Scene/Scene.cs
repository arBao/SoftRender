using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoftRender
{
    public class Scene
    {
        public List<GameObject3D> gameObjects = new List<GameObject3D>();
        //public List<Light> lights = new List<Light>();
        public Camera3D camera = new Camera3D();
        public Light light;


        public Scene()
        {

        }

        public void AddGameObject(GameObject3D gameObject)
        {
            gameObjects.Add(gameObject);
        }

        //public void AddLight(Light light)
        //{
        //    lights.Add(light);
        //}
    }
}
