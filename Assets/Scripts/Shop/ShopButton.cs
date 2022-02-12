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

            UpdateLabel();
            icon.sprite = item.Icon;
            button.onClick.AddListener(ButtonClick);
        }

        public void UpdateLabel() => label.text = _item.name + " – " + _item.Price + "₱";

        private void ButtonClick()
        {
            if (_item.Buy() && !_item.AllowMultiBuy)
                Destroy(gameObject);
            UpdateLabel();
        }
    }
}