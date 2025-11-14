using System;
using UnityEngine;

namespace Study
{
    public class GizmosStudy : MonoBehaviour
    {
        public Texture texture;
        public Mesh mesh;
        private void OnDrawGizmosSelected()
        {
            // Gizmos.matrix = Matrix4x4.identity;
            // Gizmos.color = Color.cyan;
            // Gizmos.DrawCube(this.transform.position,new Vector3(1,1,1));
            // Gizmos.DrawWireCube(this.transform.position,new Vector3(2,2,2));
            // // Gizmos.matrix = this.transform.localToWorldMatrix;
            // Gizmos.matrix = Matrix4x4.TRS(transform.position,transform.rotation,Vector3.one);
            // Gizmos.DrawFrustum(this.transform.position,20,20,100,16f/9f);

            #region Draw Texture/Icon

            // Gizmos.DrawGUITexture(new Rect(this.transform.position,new Vector2(10,20)),texture);
            Gizmos.DrawIcon(Vector3.zero, "Circle");
            #endregion

            #region Draw line/grid/ray

            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position,transform.position + transform.forward*10);
            Gizmos.DrawRay(Vector3.zero, transform.up);
            Gizmos.color = Color.yellow;
            // if (mesh is not null)
            // {
            //     Gizmos.DrawMesh(mesh,-1,transform.position);
            //     Gizmos.DrawWireMesh(mesh,-1,Vector3.zero);
            // }
            #endregion
        }
    }
}