using System;
using UnityEditor;
using UnityEngine;

namespace Editor.Study
{
    public class Lesson1
    {
        [MenuItem("EditorExtend/Lesson1/Hello _LEFT")]
        private static void Test()
        {
            Debug.Log("Hello Editor!");
        }

        [MenuItem("GameObject/Lesson1/TestFunc1")]
        private static void TestFunc1()
        {
            Debug.Log("Hierarchy Test");
        }
        
        [MenuItem("Assets/Lesson1/TestFunc2")]
        private static void TestFunc2()
        {
            Debug.Log("Assets Test");
        }
        
        [MenuItem("CONTEXT/Test_lesson1/TestFunc3")]
        private static void TestFunc3()
        {
            Debug.Log("Inspector Test");
        }
    }
}
