using Singletons;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(CookManager))]
    public class CookManagerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Add Cook"))
                CookManager.AddCook(1f, 1f);
        }
    }
}