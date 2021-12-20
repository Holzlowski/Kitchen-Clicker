using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(Wallet))]
    public class WalletEditor : UnityEditor.Editor
    {
        private int _customAmount;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Add 10₱"))
                    Wallet.AddMoney(10);
                if (GUILayout.Button("Add 100₱"))
                    Wallet.AddMoney(100);
                if (GUILayout.Button("Add 1000₱"))
                    Wallet.AddMoney(1000);
            }

            using (new EditorGUILayout.HorizontalScope())
            {
                _customAmount = EditorGUILayout.IntField("Custom amount", _customAmount);
                if (GUILayout.Button("Add", GUILayout.Width(40)))
                {
                    Wallet.AddMoney(_customAmount);
                    _customAmount = 0;
                }
            }
        }
    }
}