using System;
using UnityEditor;
using UnityEngine;

namespace Editor.Study
{
    public class Lesson26Window : EditorWindow
    {
        [MenuItem("EditorExtend/Lesson26/Scene handler")]
        private static void OpenWin()
        {
            var win = GetWindow<Lesson26Window>();
            win.Show();
        }


        private void OnEnable()
        {
            SceneView.duringSceneGui += SceneChanged;
        }

        private void OnDisable()
        {
            SceneView.duringSceneGui -= SceneChanged;
        }

        private void SceneChanged(SceneView view)
        {
            Debug.Log("Window event");
        }
    }
}
