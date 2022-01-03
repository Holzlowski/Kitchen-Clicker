using System.Collections;
using TMPro;
using UnityEngine;

namespace UI
{
    public class Notification : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private Animator animator;
        [SerializeField] private float displayTime = 1;

        private static readonly int Show = Animator.StringToHash("Show");
        private static readonly int Hide = Animator.StringToHash("Hide");

        public void ShowNotification(string text)
        {
            this.text.text = text;
            animator.SetTrigger(Show);

            StartCoroutine(HideNotification());
        }

        private IEnumerator HideNotification()
        {
            yield return new WaitForSeconds(displayTime);
            animator.SetTrigger(Hide);
        }
    }
}