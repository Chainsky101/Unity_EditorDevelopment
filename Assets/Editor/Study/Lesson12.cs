using System;
using UnityEditor;
using UnityEngine;

namespace Editor.Study
{
    public class Lesson12 : EditorWindow
    {
        private void OnGUI()
        {
            
        }

        [MenuItem("EditorExtend/Lesson12/EditorGUIUtility Study")]
        private static void OpenWin()
        {
            Lesson12 win = EditorWindow.GetWindow<Lesson12>("EditorGUIUtility");
            win.Show();
        }
    }
}