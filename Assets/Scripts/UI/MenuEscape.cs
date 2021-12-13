using UnityEngine;

namespace UI
{
    public class MenuEscape : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetButtonDown("Cancel"))
                CloseWindow();
        }

        public void CloseWindow() => gameObject.SetActive(false);
    }
}