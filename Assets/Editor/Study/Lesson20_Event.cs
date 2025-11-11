using System;
using UnityEditor;
using UnityEngine;

namespace Editor.Study
{
    public class Lesson20_Event : EditorWindow
    {
        [MenuItem("EditorExtend/Lesson20/Event Study Window")]
        private static void OpenWin()
        {
            var win = EditorWindow.GetWindow<Lesson20_Event>();
            win.Show();
        }

        private void OnGUI()
        {
            Event eve = Event.current;
            if(eve.alt)
                Debug.Log("alt key down");
            if(eve.command)
                Debug.Log("win key down");
            if (eve.control)
                Debug.Log("control key down");
            if(eve.numeric)
                Debug.Log("numeric keyboard down");
            if(eve.shift)
                Debug.Log("shift key down");
            if(eve.capsLock)
                Debug.Log("capsLock key down");
            if(eve.functionKey)
                Debug.Log("functionKey key down");
            if (eve.isKey)
            {
                Debug.Log("keyboard event");
                Debug.Log(eve.character);
                Debug.Log(eve.keyCode);
                Debug.Log(eve.modifiers);
            }
            
            if (eve.isMouse)
            {
                Debug.Log("Mouse event");
                Debug.Log(eve.button);
                Debug.Log(eve.delta);
            }

            if (eve.isScrollWheel)
            {
                Debug.Log("scrolling wheel");
            }
            if(eve.commandName == "Copy")
                Debug.Log("copy");
        }
    }
}
