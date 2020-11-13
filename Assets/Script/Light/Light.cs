using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SoftRender
{
    public class Light
    {
        public virtual Color Lighting(
            Color ambientColor, 
            Color diffuseColor, 
            Color specularColor,
            Vector3D position,
            Vector3D normal,
            Vector3D eyeDir
            )
        {
            return Color.White();
        }
    }
}

