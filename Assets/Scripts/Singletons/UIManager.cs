using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

namespace Singletons
{
    public class UIManager : MonoBehaviour
    {
        private static UIManager instance;

        [SerializeField] private Notification notification;
        [SerializeField] private TMP_Text wallet;
        [SerializeField] private GameObject store;
        [SerializeField] private Button nextIngredient;
        [SerializeField] private Camera mainCam;
        [SerializeField] private Animator camAnim;


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

        private void Start() {
            mainCam = KitchenManagement.GetMainCamera();
            camAnim = mainCam.GetComponent<Animator>();

        }
        private void Update()
        {
            //showIngredient();

            instance.wallet.text = $"₱ {Wallet.GetBalance()}";

            if (Input.GetButtonDown("Cancel"))
                CloseActiveWindow();

            if (Input.GetButtonDown("Store"))
                OpenWindow(store);
        }


        public static void showIngredient() => instance.nextIngredient.image.sprite = KitchenManagement.getCurrentIngredientSprite();

        public void turnCamera(){

            if(!camAnim.GetBool("cameraTurned") && mainCam.transform.eulerAngles.x == 90){
                camAnim.SetBool("cameraTurned", true);
            }
            else if(camAnim.GetBool("cameraTurned") && mainCam.transform.eulerAngles.x == 0){
               camAnim.SetBool("cameraTurned", false);
            }
        }





        //  IEnumerator turnCamera(){

        //      Vector3 rot = new Vector3(90,0,0);

        //     if(cameraTurned){
        //         var fromAngle = mainCam.transform.rotation;
        //         var toAngle = Quaternion.Euler(mainCam.transform.eulerAngles - rot);
        //         transform.rotation = Quaternion.Slerp(fromAngle, toAngle, 20*Time.deltaTime);
  
        //     } 
        //     else if(!cameraTurned){
        //         var fromAngle = mainCam.transform.rotation;
        //         var toAngle = Quaternion.Euler(mainCam.transform.eulerAngles + rot);
        //         transform.rotation = Quaternion.Slerp(fromAngle, toAngle, 20*Time.deltaTime);
        //     }

        //     yield return null;

        //  }


    }
}