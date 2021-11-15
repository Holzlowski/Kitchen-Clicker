using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shop
{
    public class ShopButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text label;
        [SerializeField] private Image icon;

        public void Initialize(ShopItem item)
        {
            label.text = item.name;
            icon.sprite = item.Icon;
        }
    }
}