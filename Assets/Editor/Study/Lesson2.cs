using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.Study
{
    public class Lesson2 : EditorWindow
    {
        [MenuItem("EditorExtend/Lesson2/ShowWindow")]
        private static void ShowWindow()
        {
            Lesson2 win = EditorWindow.GetWindow<Lesson2>();
            win.Show();
        }

        private void CreateGUI()
        {
            VisualElement root = rootVisualElement;
            Label label = new Label("hello world!");
            root.Add(label);
            Button button = new Button();
            button.name = "button";
            button.text = "Close";
            button.clicked += () => Close();
            root.Add(button);
            Toggle toggle = new Toggle("Toggle");
            toggle.name = "toggle";
            root.Add(toggle);
        }

        //execute every frame
        private void OnGUI()
        {
            EditorGUI.LabelField(new Rect(0,100,50,30), "test");
        }

        private void OnEnable()
        {
            Debug.Log("OnEnable");
        }

        private void OnProjectChange()
        {
            Debug.Log("Project change");
        }

        private void OnHierarchyChange()
        {
            Debug.Log("Hierarchy change");
        }

        private void OnBecameInvisible()
        {
            Debug.Log("Invisible");
        }
    }
}