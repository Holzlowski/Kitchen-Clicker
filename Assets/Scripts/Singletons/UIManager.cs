using System;
using System.Collections.Generic;
using Shop;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Singletons
{
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private Notification notification;
        [SerializeField] private TMP_Text wallet;
        [SerializeField] private TMP_Text level;
        [SerializeField] private GameObject store;
        [SerializeField] private Camera mainCam;
        [SerializeField] private Animator camAnim;
        [SerializeField] private Image nextIngredient;
        [SerializeField] private ShopItemList recipeStore;

        private static readonly List<GameObject> ActiveWindows = new List<GameObject>();
        private static readonly int CameraTurned = Animator.StringToHash("cameraTurned");

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

        public static void ShowNotification(string text) => Instance.notification.ShowNotification(text);

        public static bool IsStoreActive()
        {
            return Instance.store.activeSelf;
        }

        private void Start()
        {
            if (!Camera.main)
            {
                Debug.LogError("Main camera not found");
                return;
            }

            camAnim = Camera.main.GetComponent<Animator>();
        }

        private void Update()
        {
            Instance.wallet.text = $"₱ {Wallet.GetBalance()}";
            Instance.level.text = $"Level {KitchenManagement.GetLevel()}";

            if (Input.GetButtonDown("Cancel"))
                CloseActiveWindow();

            if (Input.GetButtonDown("Store"))
                OpenWindow(store);

            //Time.timeScale = store.activeSelf ? 0 : 1;
        }


        public void TurnCamera()
        {
            if (!camAnim.GetBool(CameraTurned) && Math.Abs(mainCam.transform.eulerAngles.x - 90) < 0.001)
                camAnim.SetBool(CameraTurned, true);
            else if (camAnim.GetBool(CameraTurned) && mainCam.transform.eulerAngles.x == 0)
                camAnim.SetBool(CameraTurned, false);
        }

        public static void ShowIngredient() =>
            Instance.nextIngredient.sprite = KitchenManagement.GetCurrentIngredientSprite();

        public static void RecreateShopItemList() => Instance.recipeStore.RecreateShopItems();
        public static int GetLengthShopRecipes() => Instance.recipeStore.GetLengthItems();
    }
}