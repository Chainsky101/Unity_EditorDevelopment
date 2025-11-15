using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editor.Study
{
    public class Lesson33EditorUtility : EditorWindow
    {
        private GameObject obj;
        private float barValue;
        [MenuItem("EditorExtend/Lesson33/EditorUtility Study")]
        private static void OpenWin()
        {
            EditorWindow.GetWindow<Lesson33EditorUtility>("EditorUtility Study").Show();
        }


        private void OnGUI()
        {
            #region Show Dialog/Complex Dialog/Progress Bar
            
            //阻塞的
            if (GUILayout.Button("Show Dialog"))
            {
                EditorUtility.DisplayDialog("Dialog Test", "Are you sure to execute this?", "Sure","Cancel");
            }

            //阻塞的
            if (GUILayout.Button("Show Dialog Complex"))
            {
                int action = EditorUtility.DisplayDialogComplex("Complex Dialog Test", "Which kind of action you want to execute?",
                    "Action1", "Action3", "Action2");
                switch (action)
                {
                    case 0:
                        Debug.Log("execute action1");
                        break;
                    case 1:
                        Debug.Log("execute action3");
                        break;
                    case 2:
                        Debug.Log("execute action2");
                        break;
                }
            }

            // 非阻塞的
            if (GUILayout.Button("Show Progress Bar"))
            {
                barValue += 0.1f;
                EditorUtility.DisplayProgressBar("Progress bar", "Loading",barValue);
                if (barValue >= 1f)
                {
                    barValue = 0;
                    EditorUtility.ClearProgressBar();
                }
            }

            #endregion

            #region File Panel Related

            if (GUILayout.Button("Save File Panel"))
            {
                string str = EditorUtility.SaveFilePanel("Save File to", Application.dataPath, "file", "txt");
                Debug.Log(str);
                if(str != "")
                    File.WriteAllText(str,"12332132122");
            }

            if (GUILayout.Button("Save Folder Panel"))
            {
                string str1 = EditorUtility.SaveFolderPanel("Save Folder to ", Application.dataPath, "Folder");
                Debug.Log(str1);
            }

            if (GUILayout.Button("Save File in Project"))
            {
                string str2 = EditorUtility.SaveFilePanelInProject("Save File in Project", "file", "txt", "Save file where is it");
                Debug.Log(str2);
                if(str2 != "")
                    File.WriteAllText(str2,"11111111111111111111155555555555555555555");
            }

            if (GUILayout.Button("Open File"))
            {
                string str3 = EditorUtility.OpenFilePanel("Open File", Application.dataPath, "txt");
                Debug.Log(str3);
                if (str3 != "")
                {
                    string str4 = File.ReadAllText(str3);
                    Debug.Log(str4);
                }
            }

            if (GUILayout.Button("Open Folder"))
            {
                string str5 = EditorUtility.OpenFolderPanel("Open Folder", Application.dataPath, "Folder");
                Debug.Log(str5);
            }
            #endregion

            #region Other Contents

            obj = EditorGUILayout.ObjectField("selected obj",obj,typeof(GameObject),true) as GameObject;
            if (GUILayout.Button("Show Dependencies"))
            {
                if ( obj != null)
                {
                    Object[] objects = EditorUtility.CollectDependencies(new []{obj});
                    Selection.objects = objects;
                }
            }
            

            #endregion
        }
    }
}