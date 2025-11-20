using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editor.Study
{
    public class Lesson43 : EditorWindow
    {
        [MenuItem("EditorExtend/Lesson43/AssetDatabase Study")]
        private static void OpenWin()
        {
            GetWindow<Lesson43>("AssetDatabase Study").Show();
        }

        private void OnGUI()
        {
            #region Frquently used Methodes in AssetDatabase

            if (GUILayout.Button("Create Resources"))
            {
                Material mat = new Material(Shader.Find("Legacy Shaders/Specular"));
                AssetDatabase.CreateAsset(mat,"Assets/Resources/testRes.mat");
            }

            if (GUILayout.Button("Create Folder"))
            {
                AssetDatabase.CreateFolder("Assets/Resources", "TestFolder");
            }

            if (GUILayout.Button("Copy Resources"))
            {
                AssetDatabase.CopyAsset("Assets/Editor Default Resources/EditorStudy.png", "Assets/Resources/TestFolder/test.png");
            }

            if (GUILayout.Button("Move Resources"))
            {
                AssetDatabase.MoveAsset("Assets/Resources/TestFolder/test.png", "Assets/Resources/test.png");
            }

            if (GUILayout.Button("Delete single Resource"))
            {
                AssetDatabase.DeleteAsset("Assets/Resources/testRes.mat");
            }

            if (GUILayout.Button("Delete multiply Resources"))
            {
                List<string> failedPath = new();
                AssetDatabase.DeleteAssets(new[]
                {
                    "Assets/Resources/TestFolder/test.png",
                    "Assets/Resources/TestFolder/testRes.mat",
                    "Assets/Resources/TestFolder/testRes1.mat"
                }, failedPath);
                foreach (var path in failedPath)
                {
                    Debug.Log(path);
                }
            }

            if (GUILayout.Button("Get Resources Path"))
            {
                Debug.Log(AssetDatabase.GetAssetPath(Selection.activeObject));
            }

            if (GUILayout.Button("Load Resources"))
            {
                Texture texture = AssetDatabase.LoadAssetAtPath<Texture>("Assets/Resources/test.png");
                string guid = AssetDatabase.AssetPathToGUID("Assets/Resources/test.png");
                Debug.Log(texture.name);
                Debug.Log(guid);
            }

            if (GUILayout.Button("Refresh AssetsDatabase"))
            {
                File.WriteAllText("Assets/Resources/testTxt.txt","1234122123");
                AssetDatabase.Refresh();
            }
            
            
            #endregion
            
            
        }
    }
}
