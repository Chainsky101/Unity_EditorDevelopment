using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Editor.Practise
{
    public class ScriptWin : EditorWindow
    {
        //会跳过控件绑定的控件名
        private List<string> defaultName = new List<string>()
            { "Image", "Text (TMP)", "Background", "Checkmark", 
                "Label","Fill","Handle","Arrow","Text","Placeholder",
                "Text (Legacy)","Viewport","Scrollbar Vertical"};
        //record binding object name and type
        private Dictionary<string, Type> notebook = new Dictionary<string, Type>();
        private string templatePath = Application.dataPath + "/Editor/Practise/";
        private string path = "Assets/Scripts";
        private GameObject obj;
        private string baseCode;
        private string deprivedCode;
        private bool isFirst = true;
        private StringBuilder sbBlock1;
        private StringBuilder sbBlock2;
        private StringBuilder sbBlock3;
        private StringBuilder sbBlock4;
        
        private Vector2 loc = Vector2.zero;
        private void OnGUI()
        {
            if(obj is null)
                return;
            
            loc = EditorGUILayout.BeginScrollView(loc);
            if (isFirst)
            {
                isFirst = false;
                var codeList = GenerateCode();
                baseCode = EditorGUILayout.TextArea(codeList[0]);
                deprivedCode = EditorGUILayout.TextArea(codeList[1]);
            }
            else
            {
                baseCode = EditorGUILayout.TextArea(baseCode);
                deprivedCode = EditorGUILayout.TextArea(deprivedCode);
            }

            if (GUILayout.Button("Save"))
            {
                string savePath = EditorUtility.SaveFolderPanel("Save Script", path, "UI");
                if (savePath != "")
                {
                    File.WriteAllText(savePath + "/" + obj.name + ".cs", deprivedCode);
                    File.WriteAllText(savePath + "/" + obj.name + "Base.cs", baseCode);
                    AssetDatabase.Refresh();
                }
            }
            EditorGUILayout.EndScrollView();
        }

        private void OnEnable()
        {
            obj = Selection.activeGameObject;
            CompilationPipeline.compilationFinished += AddScriptToGameObject;
        }

        private void OnDisable()
        {
            CompilationPipeline.compilationFinished -= AddScriptToGameObject;
        }

        //add the generated script upon the selected GameObject
        private void AddScriptToGameObject(object gameObject)
        {

        }

        private List<string> GenerateCode()
        {
            string scriptName = obj.name;
            string baseCode = File.ReadAllText(templatePath + "UIConfigBase.txt");
            string deprivedCode = File.ReadAllText(templatePath + "UIConfig.txt");
            StringBuilder sbBaseCode = new StringBuilder(baseCode);
            StringBuilder sbDeprivedCode = new StringBuilder(deprivedCode);
            //1. handle deprivedCode
            //deprivedCode = deprivedCode.Replace("0", scriptName);
            sbDeprivedCode.Replace("*0*", scriptName);

            //2. handle baseCode
            // a. handle block[0]
            sbBaseCode.Replace("*0*", scriptName);
            // b. handle block[1]
            sbBlock1 = new StringBuilder("");
            // c. handle block[2]
            sbBlock2 = new StringBuilder("");
            // d. handle block[3]
            sbBlock3 = new StringBuilder("");
            // e. handle block[4]
            sbBlock4 = new StringBuilder("");
            // solution 1: find all UIBehaviour
            // cons: cannot skip the multiply controls in one object
            #region Solution 1
            // UIBehaviour[] controls = obj.transform.GetComponentsInChildren<UIBehaviour>();
            // foreach (var control in controls)
            // {
            //     string controlName = control.name;
            //     //skip the default name object
            //     if(defaultName.Contains(controlName)||controlName == obj.name)
            //         continue;
            //     
            //     switch (control)
            //     {
            //         case Text:
            //             AddControlWithoutListener("Text",controlName);
            //             break;
            //         case Button:
            //             AddControlWithListener("Button",controlName);
            //             break;
            //         case Slider:
            //             AddControlWithListener("Slider",controlName);
            //             break;
            //         case Toggle:
            //             AddControlWithListener("Toggle",controlName);
            //             break;
            //         case Image:
            //             AddControlWithoutListener("Image",controlName);
            //             break;
            //         case TMP_Dropdown:
            //             AddControlWithListener("TMP_Dropdown",controlName);
            //             break;
            //         case TMP_InputField:
            //             AddControlWithListener("TMP_InputField",controlName);
            //             break;
            //         case Dropdown:
            //             AddControlWithListener("Dropdown",controlName);
            //             break;
            //         case InputField:
            //             AddControlWithListener("InputField",controlName);
            //             break;
            //         default:
            //             Debug.Log($"there is non-interpreted type on {controlName}");
            //             break;
            //     }
            // }
            #endregion
            
            // solution 2: binding controls according to type
            GenerateControl<Button>();
            GenerateControl<Slider>();
            GenerateControl<Toggle>();
            GenerateControl<InputField>();
            GenerateControl<Dropdown>();
            GenerateControl<TMP_Dropdown>();
            GenerateControl<TMP_InputField>();
            GenerateControl<ScrollRect>();
            GenerateControl<Text>();
            GenerateControl<Image>();
            GenerateControl<TMP_Text>();
            sbBaseCode.Replace("*1*", sbBlock1.ToString());
            sbBaseCode.Replace("*2*", sbBlock2.ToString());
            sbBaseCode.Replace("*3*", sbBlock3.ToString());
            sbBaseCode.Replace("*4*", sbBlock4.ToString());

            return new List<string> { sbBaseCode.ToString(), sbDeprivedCode.ToString() };
        }

        private void AddControlWithoutListener(string typeName, string controlName,Transform control)
        {
            sbBlock1.AppendLine($"\t\tpublic {typeName} {controlName};");
            sbBlock2.AppendLine($"\t\t\t{controlName} = transform.Find(\"{GetControlPath(control)}\").GetComponent<{typeName}>();");
        }

        private void AddControlWithListener(string typeName, string controlName,Transform control)
        {
            AddControlWithoutListener(typeName, controlName,control);
            if (typeName == "Button")
            {
                sbBlock3.AppendLine($"\t\t\t{controlName}.onClick.AddListener(On{controlName}Listener);");
                sbBlock4.AppendLine($"\t\tprotected virtual void On{controlName}Listener()");
            }
            else if (typeName == "Toggle")
            {
                sbBlock3.AppendLine($"\t\t\t{controlName}.onValueChanged.AddListener(On{controlName}Listener);");
                sbBlock4.AppendLine($"\t\tprotected virtual void On{controlName}Listener(bool isOn)");
            }
            else if (typeName == "Slider")
            {
                sbBlock3.AppendLine($"\t\t\t{controlName}.onValueChanged.AddListener(On{controlName}Listener);");
                sbBlock4.AppendLine($"\t\tprotected virtual void On{controlName}Listener(float val)");
            }
            else if (typeName == "TMP_Dropdown")
            {
                sbBlock3.AppendLine($"\t\t\t{controlName}.onValueChanged.AddListener(On{controlName}Listener);");
                sbBlock4.AppendLine($"\t\tprotected virtual void On{controlName}Listener(int val)");
            }
            else if (typeName == "TMP_InputField")
            {
                sbBlock3.AppendLine($"\t\t\t{controlName}.onSubmit.AddListener(On{controlName}Listener);");
                sbBlock4.AppendLine($"\t\tprotected virtual void On{controlName}Listener(string content)");
            }
            else if (typeName == "Dropdown")
            {
                sbBlock3.AppendLine($"\t\t\t{controlName}.onValueChanged.AddListener(On{controlName}Listener);");
                sbBlock4.AppendLine($"\t\tprotected virtual void On{controlName}Listener(int val)");
            }
            else if (typeName == "InputField")
            {
                sbBlock3.AppendLine($"\t\t\t{controlName}.onSubmit.AddListener(On{controlName}Listener);");
                sbBlock4.AppendLine($"\t\tprotected virtual void On{controlName}Listener(string content)");
            }
            else if (typeName == "ScrollRect")
            {
                sbBlock3.AppendLine($"\t\t\t{controlName}.onValueChanged.AddListener(On{controlName}Listener);");
                sbBlock4.AppendLine($"\t\tprotected virtual void On{controlName}Listener(Vector2 loc)");
            }
            sbBlock4.AppendLine("\t\t{}");
            sbBlock4.AppendLine();
        }
        
        
        private void GenerateControl<T>() where T : UIBehaviour
        {
            string typeName = typeof(T).Name;
            T[] controls = obj.GetComponentsInChildren<T>();
            foreach (var control in controls)
            {
                string controlName = control.name;
                //1. skip the default name object
                if (defaultName.Contains(controlName)||controlName == obj.name)
                    continue;
                
                if (notebook.ContainsKey(controlName))
                {
                    //2. skip the object both same name and type
                    if (notebook[controlName] == typeof(T))
                    {
                        EditorUtility.DisplayDialog("Contradict Objects",
                            $"There are two controls with both same name and type:{controlName}", "sure");
                        this.Close();
                    }
                    //3.skip the same name Object(with multiply controls)
                    continue;
                }
                notebook.Add(controlName,typeof(T));
                
                switch (typeName)
                {
                    case "Text":
                        AddControlWithoutListener("Text",controlName,control.transform);
                        break;
                    case "TMP_Text":
                        AddControlWithoutListener("TMP_Text",controlName,control.transform);
                        break;
                    case "Button":
                        AddControlWithListener("Button",controlName,control.transform);
                        break;
                    case "Slider":
                        AddControlWithListener("Slider",controlName,control.transform);
                        break;
                    case "Toggle":
                        AddControlWithListener("Toggle",controlName,control.transform);
                        break;
                    case "Image":
                        AddControlWithoutListener("Image",controlName,control.transform);
                        break;
                    case "TMP_Dropdown":
                        AddControlWithListener("TMP_Dropdown",controlName,control.transform);
                        break;
                    case "TMP_InputField":
                        AddControlWithListener("TMP_InputField",controlName,control.transform);
                        break;
                    case "Dropdown":
                        AddControlWithListener("Dropdown",controlName,control.transform);
                        break;
                    case "InputField":
                        AddControlWithListener("InputField",controlName,control.transform);
                        break;
                    case "ScrollRect":
                        AddControlWithListener("ScrollRect",controlName,control.transform);
                        break;
                    default:
                        Debug.Log($"there is non-interpreted type on {controlName}");
                        break;
                }
            }
        }

        //fix the nested problem(find can only apply on one nested layer)
        private string GetControlPath(Transform control)
        {
            StringBuilder path = new StringBuilder(control.name);
            while (control.parent != obj.transform)
            {
                path.Insert(0, control.parent.name + "/");
                control = control.parent;
            }
            return path.ToString();
            // string path = control.name;
            // while (control.parent != obj.transform)
            // {
            //     path = control.parent.name + "/" + path;
            // }
            //
            // return path;
        }
    }
}