using System;
using Unity.Mathematics;
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
            // Handles.color = Color.white;
            // Handles.DrawWireArc(obj.transform.position,obj.transform.up,Quaternion.Euler(0,-15,0)*obj.transform.forward ,30,5);
            // Handles.DrawSolidArc(obj.transform.position,obj.transform.up,Quaternion.Euler(0,-15,0)*obj.transform.forward ,30,4);
            // Handles.color = Color.blue;
            // Handles.DrawSolidDisc(obj.transform.position,obj.transform.up,2f);
            // Handles.color = Color.red;
            // // Handles.DrawWireCube(obj.transform.position,new Vector3(1,1,1));
            // Handles.DrawAAConvexPolygon(Vector3.zero,Vector3.right,Vector3.forward+Vector3.right,Vector3.forward);
            #endregion

            #region Persistant Axis

            // obj.transform.position = Handles.PositionHandle(obj.transform.position, obj.transform.rotation);
            // obj.transform.rotation = Handles.RotationHandle(obj.transform.rotation, obj.transform.position);
            obj.transform.localScale = Handles.ScaleHandle(obj.transform.localScale,obj.transform.position,obj.transform.rotation/*,HandleUtility.GetHandleSize(Vector3.zero)*/);

            #endregion

            #region Free translate / Free rotate

            obj.transform.position = Handles.FreeMoveHandle(obj.transform.position, HandleUtility.GetHandleSize(obj.transform.position),
                Vector3.one*5, Handles.RectangleHandleCap);
            obj.transform.rotation = Handles.FreeRotateHandle(obj.transform.rotation, Vector3.zero,
                HandleUtility.GetHandleSize(Vector3.zero));

            #endregion


            #region Show GUI in the scene

            Handles.BeginGUI();
            float w = SceneView.currentDrawingSceneView.position.width;
            float h = SceneView.currentDrawingSceneView.position.height;
            GUILayout.BeginArea(new Rect(w-100,h-100,100,100));
            GUILayout.Label("GUI in scene test");
            if (GUILayout.Button("skill"))
            {
                GameObject obj = Selection.activeGameObject;
                var player = obj.GetComponent<Lesson26>();
                player.Attack();
            }
            GUILayout.EndArea();
            Handles.EndGUI();
            #endregion
        }
    }
}
