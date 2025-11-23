using UnityEditor;

namespace Editor.Practise
{
    public class UIExpand
    {
        [MenuItem("GameObject/UI/Generate Binding Script")]
        private static void GenerateUIBindingScripts()
        {
            EditorWindow.GetWindow<ScriptWin>("Code Detail").Show();
        }
    }
}
