using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shop
{
    public class ShopButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text label;
        [SerializeField] private Image icon;

        private ShopItem _item;

        public void Initialize(ShopItem item)
        {
            _item = item;

            label.text = item.name + " – " + item.Price + "₱";
            icon.sprite = item.Icon;
            button.onClick.AddListener(ButtonClick);
        }

        private void ButtonClick()
        {
            if (_item.Buy() && !_item.AllowMultiBuy)
                Destroy(gameObject);
        }
    }
}