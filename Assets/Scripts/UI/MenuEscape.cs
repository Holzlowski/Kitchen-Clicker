using UnityEngine;

namespace UI
{
    public class MenuEscape : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetButtonDown("Cancel"))
                gameObject.SetActive(false);
        }
    }
}