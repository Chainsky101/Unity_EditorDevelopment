using Study;
using UnityEditor;
using UnityEngine;

namespace Editor.Study
{
    [CustomEditor(typeof(Player))]
    public class PlayerEditor : UnityEditor.Editor
    {
        private SerializedProperty attack;
        private SerializedProperty defence;
        private SerializedProperty enemy;
        private SerializedProperty items;
        private SerializedProperty weapons;
        private SerializedProperty info;
        private SerializedProperty id;
        private SerializedProperty buff;
        private SerializedProperty keys;
        private SerializedProperty values;
        private int dicCount;
        private int count;
        private bool isPullDown;
        private void OnEnable()
        {
            attack = serializedObject.FindProperty("attack");
            defence = serializedObject.FindProperty("defence");
            enemy = serializedObject.FindProperty("enemy");
            items = serializedObject.FindProperty("items");
            weapons = serializedObject.FindProperty("weapons");
            info = serializedObject.FindProperty("info");
            id = info.FindPropertyRelative("id");
            buff = serializedObject.FindProperty("buff");
            keys = buff.FindPropertyRelative("keys");
            values = buff.FindPropertyRelative("values");
            count = items.arraySize;
            dicCount = keys.arraySize;
        }

        public override void OnInspectorGUI()
        {
            // base.OnInspectorGUI();
            serializedObject.Update();
            isPullDown = EditorGUILayout.BeginFoldoutHeaderGroup(isPullDown, "Basic Info");
            if (isPullDown)
            {
                if (GUILayout.Button("buttonTest"))
                {
                    Debug.Log("click button");
                }
            
                EditorGUILayout.IntSlider(attack, 0, 20, "攻击力");
                defence.floatValue = EditorGUILayout.FloatField("防御力", defence.floatValue);
                EditorGUILayout.ObjectField(enemy, new GUIContent("Enemy"));
                EditorGUILayout.Space(10);
                count = EditorGUILayout.IntField("items",count);
                for (int i = items.arraySize-1; i >= count; i--)
                {
                    items.DeleteArrayElementAtIndex(i);
                }
            
                // while (items.arraySize < count)
                // {
                //     items.InsertArrayElementAtIndex(items.arraySize-1);
                // }
                for (int i = 0; i < count; i++)
                {
                    if (i >= items.arraySize)
                    {
                        items.InsertArrayElementAtIndex(i);
                    }
                    SerializedProperty temp = items.GetArrayElementAtIndex(i);
                    temp.intValue = EditorGUILayout.IntField($"items[{i}]:", temp.intValue);
                    
                    
                }
                EditorGUILayout.Space(10);
                EditorGUILayout.LabelField($"Weapons:{weapons.arraySize} type:{weapons.arrayElementType}");
                for (int i = 0; i < weapons.arraySize; i++)
                {
                    
                    weapons.GetArrayElementAtIndex(i).stringValue = EditorGUILayout.TextField($"weapons[{i}]",
                        weapons.GetArrayElementAtIndex(i).stringValue);
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            // EditorGUILayout.PropertyField(items, new GUIContent("items"));
            // EditorGUILayout.LabelField($"type{info.propertyType.ToString()}");
            EditorGUILayout.PropertyField(info, new GUIContent("info"));
            // EditorGUILayout.PropertyField(buff, new GUIContent("buff"));

            #region Display Dictionary

            dicCount = EditorGUILayout.IntField("pairs", dicCount);
            for (int i = keys.arraySize-1; i >= dicCount; i--)
            {
                keys.DeleteArrayElementAtIndex(i);
                values.DeleteArrayElementAtIndex(i);
            }
            
            for (int i = 0; i < dicCount; i++)
            {
                if(i>= keys.arraySize)
                    keys.InsertArrayElementAtIndex(i);
                if(i>= values.arraySize)
                    values.InsertArrayElementAtIndex(i);
                var key = keys.GetArrayElementAtIndex(i);
                var value = values.GetArrayElementAtIndex(i);
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(key, new GUIContent($"pair[{i}] \tkey:"));
                EditorGUILayout.PropertyField(value, new GUIContent(" \tvalue: "));
                EditorGUILayout.EndHorizontal();
            }
            // EditorGUILayout.PropertyField(keys, new GUIContent("keys"));
            // EditorGUILayout.PropertyField(values, new GUIContent("values"));
            #endregion
            serializedObject.ApplyModifiedProperties();
            
        }
    }
}