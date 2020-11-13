using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SoftRender
{
    public class DirectionalLight: Light
    {
        private Vector3D direction;
        private float shininess;

        public void SetDirection(Vector3D direction,float shininess)
        {
            this.direction = direction;
            this.shininess = shininess;

            this.direction.Normalize();
        }

        public override Color Lighting(
            Color ambientColor, 
            Color diffuseColor, 
            Color specularColor, 
            Vector3D position, 
            Vector3D normal, 
            Vector3D eyeDir)
        {
            Vector3D directionInverse = new Vector3D(-direction.x,-direction.y,-direction.z);
            eyeDir.Normalize();
            float diff = Mathf.Max(Vector3D.Dot(normal, directionInverse), 0);
            Vector3D halfwarDir = Vector3D.Add(eyeDir, direction);
            halfwarDir.Normalize();
            float spec = Mathf.Pow(Mathf.Max(Vector3D.Dot(eyeDir, halfwarDir), 0), shininess);
            //Color color = specularColor * spec;
            Color color = specularColor * spec * 0.8f + diffuseColor * diff * 0.8f + ambientColor * 0.5f;
            return color;
        }
    }
}


