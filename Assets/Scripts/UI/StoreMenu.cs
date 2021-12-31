using Singletons;
using UnityEngine;

namespace UI
{
    public class StoreMenu : MonoBehaviour
    {
        [SerializeField] private ButtonMapping[] buttonMappings;

        private void Awake()
        {
            foreach (ButtonMapping buttonMap in buttonMappings)
                buttonMap.button.onClick.AddListener(() => UIManager.OpenWindow(buttonMap.gameObject));
        }
    }
}