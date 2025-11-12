using System;
using UnityEditor;
using UnityEngine;

namespace Editor.Study
{
    [CustomEditor(typeof(Lesson26))]
    public class Lesson26Editor : UnityEditor.Editor
    {
        private Lesson26 obj;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }

        private void OnEnable()
        {
            obj = target as Lesson26;
        }

        private void OnSceneGUI()
        {
            // Debug.Log("Scene");

            #region Color/Text/Line/Dotted Line
            // Handles.color = Color.cyan;
            // Handles.Label(Vector3.zero, "text test!");
            // Handles.DrawLine(obj.transform.position,obj.transform.position+obj.transform.forward*10);
            // Handles.color = Color.blue;
            // Handles.DrawLine(obj.transform.position,obj.transform.position+obj.transform.right*10);
            #endregion

            #region Arc/Circle/Rectangle/Poly

            Handles.color = Color.white;
            Handles.DrawWireArc(obj.transform.position,obj.transform.up,Quaternion.Euler(0,-15,0)*obj.transform.forward ,30,5);
            Handles.DrawSolidArc(obj.transform.position,obj.transform.up,Quaternion.Euler(0,-15,0)*obj.transform.forward ,30,4);
            Handles.color = Color.blue;
            Handles.DrawSolidDisc(obj.transform.position,obj.transform.up,2f);
            Handles.color = Color.red;
            // Handles.DrawWireCube(obj.transform.position,new Vector3(1,1,1));
            Handles.DrawAAConvexPolygon(Vector3.zero,Vector3.right,Vector3.forward+Vector3.right,Vector3.forward);
            #endregion
        }
    }
}
