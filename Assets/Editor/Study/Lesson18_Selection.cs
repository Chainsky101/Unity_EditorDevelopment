using System;
using System.Text;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Editor.Study
{
    public class Lesson18_Selection : EditorWindow
    {
        // MenuItem Button used to open this window

        [MenuItem("EditorExtend/Lesson18/Show Selection Part Window")]
        private static void OpenWin()
        {
            Lesson18_Selection win = EditorWindow.GetWindow<Lesson18_Selection>();
            win.Show();
        }

        private StringBuilder sb1 = new("No Selection");
        private StringBuilder sb2 = new("No Selection");
        private StringBuilder sb3 = new("No Selection");
        private StringBuilder sb4 = new("No Selection");
        private StringBuilder sb5 = new("No Selection");
        private StringBuilder sb6 = new("No Selection");
        private Object[] objects;
        private GameObject obj;
        private void OnGUI()
        {
            #region Frequent Usage Static Attributes In Selection
            // Object Test —— if select Object more than one, it gets the last Object
            if (GUILayout.Button("Get selected Object"))
            {
                sb1.Clear();
                if (Selection.activeObject is not null)
                {
                    sb1.Append(Selection.activeObject.name);
                }
                else
                {
                    sb1.Append("No Selection");
                }    
            }
            EditorGUILayout.LabelField("Selected Object", sb1.ToString());
            
            //GameObject test
            if (GUILayout.Button("Get selected GameObject"))
            {
                sb2.Clear();
                if (Selection.activeGameObject is not null)
                {
                    sb2.Append(Selection.activeGameObject.name);
                }
                else
                {
                    sb2.Append("No Selection");
                }    
            }
            EditorGUILayout.LabelField("Selected GameObject", sb2.ToString());

            //Transform test
            if (GUILayout.Button("Get selected Transform"))
            {
                sb3.Clear();
                if (Selection.activeTransform is not null)
                {
                    sb3.Append(Selection.activeTransform.name);
                }
                else
                {
                    sb3.Append("No Selection");
                }    
            }
            EditorGUILayout.LabelField("Selected Transform", sb3.ToString());
            
            //Objects test
            if (GUILayout.Button("Get selected Objects"))
            {
                sb4.Clear();
                if (Selection.objects is not null)
                {
                    for (int i = 0; i < Selection.objects.Length; i++)
                    {
                        sb4.Append(Selection.objects[i].name);
                        if (i < Selection.objects.Length-1)
                        {
                            sb4.Append(" | ");
                        }
                    }
                }
                else
                {
                    sb4.Append("No Selection");
                }    
            }
            EditorGUILayout.LabelField("Selected Objects", sb4.ToString());
            
            //GameObjects test
            if (GUILayout.Button("Get selected GameObjects"))
            {
                sb5.Clear();
                if (Selection.gameObjects is not null)
                {
                    for (int i = 0; i < Selection.gameObjects.Length; i++)
                    {
                        sb5.Append(Selection.gameObjects[i].name);
                        if (i < Selection.gameObjects.Length-1)
                        {
                            sb5.Append(" | ");
                        }
                    }
                }
                else
                {
                    sb5.Append("No Selection");
                }    
            }
            EditorGUILayout.LabelField("Selected gameObjects", sb5.ToString());
            
            //Transform test
            if (GUILayout.Button("Get selected Transforms"))
            {
                sb6.Clear();
                if (Selection.transforms is not null)
                {
                    for (int i = 0; i < Selection.transforms.Length; i++)
                    {
                        sb6.Append(Selection.transforms[i].name);
                        if (i < Selection.transforms.Length-1)
                        {
                            sb6.Append(" | ");
                        }
                    }
                }
                else
                {
                    sb6.Append("No Selection");
                }    
            }
            EditorGUILayout.LabelField("Selected Transforms", sb6.ToString());
            #endregion

            #region Frequent Usage Static Methods In Selection

            obj = EditorGUILayout.ObjectField("Selected Object:",obj,typeof(GameObject),true) as GameObject;
            if (GUILayout.Button("Check gameObject"))
            {
                if (Selection.Contains(obj))
                {
                    Debug.Log("Contains");
                }
                else
                {
                    Debug.Log("Not Contain");
                }
            }

            if (GUILayout.Button("filter gameObject"))
            {
                 objects = Selection.GetFiltered<Object>(SelectionMode.DeepAssets | SelectionMode.Assets);
                 if (objects is not null)
                 {
                     for (int i = 0; i < objects.Length; i++)
                     {
                         Debug.Log(objects[i].GetType());
                     }
                 }
            }
            
            

            #endregion
        }

        private void OnEnable()
        {
            Selection.selectionChanged += SelectionChanged;
        }

        private void OnDisable()
        {
            Selection.selectionChanged -= SelectionChanged;
        }

        private void SelectionChanged()
        {
            Debug.Log("Selection Changed");
        }
    }
}