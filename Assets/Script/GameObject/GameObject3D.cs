using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoftRender
{
    public class GameObject3D
    {
        public Transform3D transform;
        public MeshRenderer meshRenderer;
        public string name = "Gameobject";

        public GameObject3D()
        {
            meshRenderer = new MeshRenderer();
            transform = new Transform3D();
        }
    }
}
