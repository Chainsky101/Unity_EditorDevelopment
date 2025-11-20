using System;
using UnityEditor;
using UnityEngine;

namespace Editor.Study
{
    public class Lesson44 : EditorWindow
    {
        [MenuItem("EditorExtend/Lesson44/PrefabUtility Study")]
        private static void OpenWin()
        {
            GetWindow<Lesson44>("PrefabUtility").Show();
        }

        private void OnGUI()
        {
            #region PrefabUtility

            if (GUILayout.Button("Create Prefab"))
            {
                GameObject obj = new GameObject("objTest");
                obj.AddComponent<Rigidbody>();
                obj.AddComponent<BoxCollider>();
                PrefabUtility.SaveAsPrefabAsset(obj, "Assets/Resources/obj.prefab");
                DestroyImmediate(obj);
            }

            if (GUILayout.Button("Load Prefab"))
            {
                GameObject obj = PrefabUtility.LoadPrefabContents("Assets/Resources/obj.prefab");
                obj.AddComponent<MeshRenderer>();
                PrefabUtility.SaveAsPrefabAsset(obj,"Assets/Resources/obj.prefab");
                PrefabUtility.UnloadPrefabContents(obj);
            }

            if (GUILayout.Button("Edit Prefab"))
            {
                GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/obj.prefab");
                obj.AddComponent<BoxCollider>();
                PrefabUtility.SavePrefabAsset(obj);
            }

            if (GUILayout.Button("Instantiate prefab"))
            {
                GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/obj.prefab");
                PrefabUtility.InstantiatePrefab(obj);
            }
            
            #endregion
        }
    }
}
