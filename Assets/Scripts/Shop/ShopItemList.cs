using UnityEngine;
using System.Collections.Generic;
using System;

namespace Shop
{
    public class ShopItemList : MonoBehaviour
    {
        [SerializeField] private Transform buyButtonView;
        [SerializeField] private ShopButton buyButtonPrefab;
        [SerializeField] private ShopItem[] items;

        private List<ShopButton> _shopItemButtons = new List<ShopButton>();

        private void Start()
        {
            CreateShopItems();
        }

        public void CreateShopItems()
        {
            if (_shopItemButtons.Count > 0)
            {
                foreach (ShopButton button in _shopItemButtons)
                {
                    if (button.gameObject != null) Destroy(button.gameObject);
                }
            }
            _shopItemButtons = new List<ShopButton>();

            Array.Sort(items);
            foreach (ShopItem item in items)
            {
                ShopButton button = Instantiate(buyButtonPrefab, buyButtonView);
                button.Initialize(item);
                _shopItemButtons.Add(button);
            }
        }
    }
}