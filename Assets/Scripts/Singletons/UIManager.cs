using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;

namespace Singletons
{
    public class UIManager : MonoBehaviour
    {
        private static UIManager instance;

        [SerializeField] private Notification notification;
        [SerializeField] private TMP_Text wallet;
        [SerializeField] private GameObject store;

        private static readonly List<GameObject> ActiveWindows = new List<GameObject>();

        public static void OpenWindow(GameObject window)
        {
            if (window.activeSelf)
                return;
            window.SetActive(true);
            ActiveWindows.Add(window);
        }

        private static void CloseWindowAtIndex(int index)
        {
            ActiveWindows[index].SetActive(false);
            ActiveWindows.RemoveAt(index);
        }

        public static void CloseActiveWindow()
        {
            if (ActiveWindows.Count > 0)
                CloseWindowAtIndex(ActiveWindows.Count - 1);
        }

        public static void CloseWindow(GameObject window)
        {
            window.SetActive(false);
            CloseWindowAtIndex(ActiveWindows.FindLastIndex(w => w == window));
        }

        public static void ShowNotification(string text) => instance.notification.ShowNotification(text);

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("There is more than one instance of UIManager in the current scene.");
                DestroyImmediate(gameObject);
            }

            instance = this;
        }

        private void Update()
        {
            instance.wallet.text = $"₱ {Wallet.GetBalance()}";

            if (Input.GetButtonDown("Cancel"))
                CloseActiveWindow();

            if (Input.GetButtonDown("Store"))
                OpenWindow(store);
        }
    }
}