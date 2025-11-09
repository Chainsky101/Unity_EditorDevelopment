using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.Study
{
    public enum E_Job
    {
        Student,
        Teacher,
        Engineer,
        Programmer
    }
    public class Lesson3 : EditorWindow
    {
        private int layer;
        private string tag;
        private Color color;
        private E_Job job;
        private string[] options = {"one", "two", "three","four"};
        private int[] ints = { 1, 2, 3 };
        private int num;
        private GameObject object_;
        private int i;
        private int i2;
        private float f;
        private double d;
        private long l;
        private Vector2 v2;
        private Vector3 v3;
        private Vector4 v4;
        private Rect r1;
        private Bounds bounds;
        private BoundsInt boundsInt;
        private string str;
        private bool isHide;
        private bool isHideGroup;
        private bool isTog;
        private bool isTogLeft;
        private bool isTogGroup;
        private float fSlider;
        private int iSlider;
        private float leftSlider;
        private float rightSlider;
        private AnimationCurve curve = new AnimationCurve();
        private Vector2 vec2Pos;
        private void OnGUI()
        {
            #region 文本控件、层级标签控件、颜色获取控件

            EditorGUILayout.LabelField("Text Label","test content");
            EditorGUILayout.LabelField("Text Label");
            layer = EditorGUILayout.LayerField("Layer", layer);
            tag = EditorGUILayout.TagField("Tag", tag);
            color = EditorGUILayout.ColorField(new GUIContent("Color:"), color, true, true, true);
            #endregion

            #region 枚举、选择、按下按钮、控件

            job = (E_Job)EditorGUILayout.EnumPopup("Job", job);
            // job = (E_Job)EditorGUILayout.EnumPopup(job);

            num = EditorGUILayout.IntPopup("num", num, options, ints);
            EditorGUILayout.LabelField(num.ToString());
            // Debug.Log(num);
            if (EditorGUILayout.DropdownButton(new GUIContent("button"), FocusType.Passive))
            {
                Debug.Log("button down!");
            }
            
            #endregion

            #region 对象关联、各类型输入

            isHideGroup = EditorGUILayout.BeginFoldoutHeaderGroup(isHideGroup, "Fold Group Component");
            if (isHideGroup)
            {
                object_ = EditorGUILayout.ObjectField("Object related",object_,typeof(GameObject),false) as GameObject;

                i = EditorGUILayout.IntField("int:", i);
                EditorGUILayout.LabelField(i.ToString());
                f = EditorGUILayout.FloatField("Float", f);
                d = EditorGUILayout.DoubleField("Double", d);
                l = EditorGUILayout.LongField("long", l);
                i2 = EditorGUILayout.DelayedIntField("int", i2);
                EditorGUILayout.LabelField(i2.ToString());
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            

            #endregion

            #region 折叠控件、折叠组控件

            isHide = EditorGUILayout.Foldout(isHide, "Fold component",true);
            if (isHide)
            {
                v2 = EditorGUILayout.Vector2Field("vector2:", v2);
                v3 = EditorGUILayout.Vector3Field("vector3:", v3);
                r1 = EditorGUILayout.RectField("Rect:", r1);
                v4 = EditorGUILayout.Vector4Field("vector4", v4);
                str = EditorGUILayout.TextField("text", str);
                bounds = EditorGUILayout.BoundsField("Bounds", bounds);
                boundsInt = EditorGUILayout.BoundsIntField("BoundsInt", boundsInt);
            }

            

            #endregion

            #region 开关控件和开关组控件

            isTogGroup = EditorGUILayout.BeginToggleGroup("Toggle Group", isTogGroup);
            if (isTogGroup)
            {
                isTog = EditorGUILayout.Toggle("Toggle", isTog);
                isTogLeft = EditorGUILayout.ToggleLeft("Toggle Left", isTogLeft);
            }
            EditorGUILayout.EndToggleGroup();
            
            EditorGUILayout.LabelField("test");
            #endregion

            #region 滑动条和双快滑动条

            fSlider = EditorGUILayout.Slider("slider", fSlider, 0f, 1f);
            iSlider = EditorGUILayout.IntSlider("intSlider", iSlider, -5, 5);
            EditorGUILayout.MinMaxSlider("Slider Block",ref leftSlider,ref rightSlider,0f,10f);
            EditorGUILayout.LabelField(leftSlider.ToString());
            EditorGUILayout.LabelField(rightSlider.ToString());
            #endregion

            #region 帮助框、行间隔
            vec2Pos = EditorGUILayout.BeginScrollView(vec2Pos);
            EditorGUILayout.HelpBox("Tip",MessageType.None);
            EditorGUILayout.Space(5);
            EditorGUILayout.HelpBox("Info Tip",MessageType.Info);
            EditorGUILayout.Space(10);
            EditorGUILayout.HelpBox("Warning Tip",MessageType.Warning);
            EditorGUILayout.Space(15);
            EditorGUILayout.HelpBox("Error Tip",MessageType.Error);
            curve = EditorGUILayout.CurveField("animation curve", curve);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("1234321");
            EditorGUILayout.LabelField("1234321");
            EditorGUILayout.LabelField("1234321");
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("1234321");
            EditorGUILayout.LabelField("1234321");
            EditorGUILayout.LabelField("1234321");
            EditorGUILayout.EndVertical();
            
            EditorGUILayout.EndScrollView();
            

            #endregion

            #region 动画曲线、布局API

            

            

            #endregion
            //used to check event sent from other windows
            if (Event.current.type == EventType.ExecuteCommand)
            {
                if(Event.current.commandName == "TestEventBetweenWindows")
                    Debug.Log("event execute");
            }
        }
        
        [MenuItem("EditorExtend/Lesson3/ShowWindows")]
        private static void ShowWindow()
        {
            Lesson3 win = GetWindow<Lesson3>();
            win.Show();
        }

        private void CreateGUI()
        {
            VisualElement root = rootVisualElement;

        }
    }
}