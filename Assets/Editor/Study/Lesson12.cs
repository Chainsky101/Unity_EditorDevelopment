using UnityEditor;
using UnityEngine;

namespace Editor.Study
{
    public class Lesson12 : EditorWindow
    {
        private Texture img1;
        private Texture img2;
        private Texture img3;
        private Color color;
        private AnimationCurve curve = new AnimationCurve();
        private void OnGUI()
        {
            #region LoadResources

            if (GUILayout.Button("Load Resources"))
            {
                img1 = EditorGUIUtility.Load("EditorStudy.png") as Texture;
            }
            if(img1 != null)
                GUI.DrawTexture(new Rect(0,100,160,90),img1);
            if (GUILayout.Button("Load Resources otherwise throw error"))
            {
                img2 = EditorGUIUtility.LoadRequired("EditorStudy.png") as Texture;
            }
            if(img2 != null)
                GUI.DrawTexture(new Rect(0,200,160,90),img2);
            #endregion

            #region 搜索框查询、对象选中提示

            if(GUILayout.Button("pick resources"))
                EditorGUIUtility.ShowObjectPicker<Texture>(null,true,"Editor",0);
            if (Event.current.commandName == "ObjectSelectorUpdated")
            {
                img3 = EditorGUIUtility.GetObjectPickerObject() as Texture;
                if(img3 is not null)
                    Debug.Log(img3.name);
            }
            else if (Event.current.commandName == "ObjectSelectorClosed")
            {
                img3 = EditorGUIUtility.GetObjectPickerObject() as Texture;
                if(img3 is not null)
                    Debug.Log("window close: " + img3.name);
            }

            if (GUILayout.Button("Ping selected object"))
            {
                EditorGUIUtility.PingObject(img3);
                
            }

            #endregion

            #region 窗口事件传递、坐标转换

            if (GUILayout.Button("Send event to other windows"))
            {
                Event eventTest = EditorGUIUtility.CommandEvent("TestEventBetweenWindows");
                Lesson3 win = EditorWindow.GetWindow<Lesson3>();
                win.SendEvent(eventTest);
            }
            if (GUILayout.Button("Coordinate Transfer"))
            {
                Vector2 vec = new Vector2(10, 10);
                GUI.BeginGroup(new Rect(10,10,100,100));
                Vector2 vecScreen = EditorGUIUtility.GUIToScreenPoint(vec);
                GUI.EndGroup();
                Debug.Log("GUI vector:"+vec+" Screen vector:"+vecScreen);
            }

            #endregion

            #region 指定区域使用对应鼠标指针
            EditorGUI.DrawRect(new Rect(0,200,100,100),Color.cyan);
            EditorGUIUtility.AddCursorRect(new Rect(0,200,100,100), MouseCursor.Link);

            #endregion

            #region 绘制色板、绘制曲线

            color = EditorGUILayout.ColorField(new GUIContent("Color Selection"), color, true, true, true);
            EditorGUIUtility.DrawColorSwatch(new Rect(180,200,100,50),Color.cyan);

            curve = EditorGUILayout.CurveField("curve",curve);
            EditorGUIUtility.DrawCurveSwatch(new Rect(0,400,100,100),curve,null,Color.red,Color.white);

            #endregion
        }

        [MenuItem("EditorExtend/Lesson12/EditorGUIUtility Study")]
        private static void OpenWin()
        {
            Lesson12 win = EditorWindow.GetWindow<Lesson12>("EditorGUIUtility");
            win.Show();
        }
    }
}