using Singletons;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(KitchenManagement))]
    public class KitchenManagementEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Level Up"))
                KitchenManagement.LevelUp();
        }
    }
}