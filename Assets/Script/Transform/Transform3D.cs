using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoftRender
{
    public class Transform3D
    {
        private Vector3D scale;
        public Quaternion rotation;
        private Vector3D translation;
        private Matrix4x4 worldM;

        private Vector3D forward = new Vector3D(0, 0, 1);//z
        private Vector3D up = new Vector3D(0, 1, 0);//y
        private Vector3D right = new Vector3D(1, 0, 0);//x

        private bool dirty;

        public Transform3D()
        {
            scale = new Vector3D(1, 1, 1);
            rotation = new Quaternion();
            rotation.Identity();
            translation = new Vector3D(0, 0, 0);
            worldM = new Matrix4x4();
            worldM.LoadIdentity();
            dirty = true;
        }

        public Matrix4x4 ToMatrix()
        {
            if(dirty == true)
            {
                Matrix4x4 transM = new Matrix4x4();
                Matrix4x4 scaleM = new Matrix4x4();
                Matrix4x4 rotationM = rotation.ToMatrix();
                scaleM.SetScale(scale);
                transM.SetTranslation(translation);
                Matrix4x4 tmpM = Matrix4x4.Multiply(transM, worldM);
                worldM = Matrix4x4.Multiply(tmpM, scaleM);
                worldM = Matrix4x4.Multiply(tmpM, rotationM);
                dirty = false;
            }

            return worldM;
        }

        public Vector3D GetPosition()
        {
            return translation;
        }

        public void Translate(Vector3D trans)
        {
            dirty = true;
            translation = Vector3D.Add(translation, trans);

            Matrix4x4 tmpM = new Matrix4x4();
            tmpM.SetTranslation(trans);
            //worldM = Matrix4x4.Multiply(tmpM, worldM);
        }

        public void SetScale(Vector3D scale)
        {
            dirty = true;
            translation = Vector3D.Multipy(this.scale, scale);

            Matrix4x4 tmpM = new Matrix4x4();
            tmpM.SetScale(scale);
            //worldM = Matrix4x4.Multiply(tmpM, worldM);
        }

        public void RotateX(float angle)
        {
            dirty = true;
            Quaternion quaternion = Quaternion.GetRotateX(angle * Mathf.Deg2Rad);
            rotation = Quaternion.Mutlipy(rotation, quaternion);

            //worldM = Matrix4x4.Multiply(quaternion.ToMatrix(), worldM);
        }

        public void RotateY(float angle)
        {
            dirty = true;
            Quaternion quaternion = Quaternion.GetRotateY(angle * Mathf.Deg2Rad);
            rotation = Quaternion.Mutlipy(rotation, quaternion);

            //worldM = Matrix4x4.Multiply(quaternion.ToMatrix(), worldM);
        }

        public void RotateZ(float angle)
        {
            dirty = true;
            Quaternion quaternion = Quaternion.GetRotateZ(angle * Mathf.Deg2Rad);
            rotation = Quaternion.Mutlipy(rotation, quaternion);

            //worldM = Matrix4x4.Multiply(quaternion.ToMatrix(), worldM);
        }

        public Vector3D Up()
        {
            return Quaternion.MutlipyVector3D(rotation, up);
        }

        public Vector3D Forward()
        {
            return Quaternion.MutlipyVector3D(rotation, forward);
        }

        public Vector3D Right()
        {
            return Quaternion.MutlipyVector3D(rotation, right);
        }

    }
}
