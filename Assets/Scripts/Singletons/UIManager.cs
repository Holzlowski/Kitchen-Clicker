using System.Collections.Generic;
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

        public static void ShowNotification(string text) => Instance.notification.ShowNotification(text);

        private void Start()
        {
            mainCam = KitchenManagement.GetMainCamera();
            camAnim = mainCam.GetComponent<Animator>();
        }

        private void Update()
        {
            Instance.wallet.text = $"₱ {Wallet.GetBalance()}";
            Instance.level.text = $"Level {KitchenManagement.GetLevel()}";

            if (Input.GetButtonDown("Cancel"))
                CloseActiveWindow();

            if (Input.GetButtonDown("Store"))
                OpenWindow(store);
        }


        public void TurnCamera()
        {
            if (!camAnim.GetBool("cameraTurned") && mainCam.transform.eulerAngles.x == 90)
            {
                camAnim.SetBool("cameraTurned", true);
            }
            else if (camAnim.GetBool("cameraTurned") && mainCam.transform.eulerAngles.x == 0)
            {
                camAnim.SetBool("cameraTurned", false);
            }
        }

        public static void showIngredient() =>
            Instance.nextIngredient.sprite = KitchenManagement.getCurrentIngredientSprite();
    }
}