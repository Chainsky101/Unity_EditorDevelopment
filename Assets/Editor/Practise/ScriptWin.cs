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
            UIBehaviour[] controls = obj.transform.GetComponentsInChildren<UIBehaviour>();
            foreach (var control in controls)
            {
                string controlName = control.gameObject.name;
                switch (control)
                {
                    case Text:
                        AddControlWithoutListener("Text",controlName);
                        break;
                    case Button:
                        AddControlWithListener("Button",controlName);
                        break;
                    case Slider:
                        AddControlWithListener("Slider",controlName);
                        break;
                    case Toggle:
                        AddControlWithListener("Toggle",controlName);
                        break;
                    case Image:
                        AddControlWithoutListener("Image",controlName);
                        break;
                    case Dropdown:
                        AddControlWithListener("Dropdown",controlName);
                        break;
                    case InputField:
                        AddControlWithListener("InputField",controlName);
                        break;
                    default:
                        Debug.Log($"there is non-interpreted type on {controlName}");
                        break;
                }
            }

            sbBaseCode.Replace("*1*", sbBlock1.ToString());
            sbBaseCode.Replace("*2*", sbBlock2.ToString());
            sbBaseCode.Replace("*3*", sbBlock3.ToString());
            sbBaseCode.Replace("*4*", sbBlock4.ToString());

            return new List<string> { sbBaseCode.ToString(), sbDeprivedCode.ToString() };
        }

        private void AddControlWithoutListener(string typeName, string controlNamePre)
        {
            string controlName = controlNamePre + typeName;
            sbBlock1.AppendLine($"\t\tpublic {typeName} {controlName};");
            sbBlock2.AppendLine($"\t\t\t{controlName} = transform.Find(\"{controlName}\").GetComponent<{typeName}>();");
        }

        private void AddControlWithListener(string typeName, string controlNamePre)
        {
            string controlName = controlNamePre + typeName;
            AddControlWithoutListener(typeName, controlNamePre);
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
            sbBlock4.AppendLine("\t\t{}");
            sbBlock4.AppendLine();

        }
    }
}