using System;
using UnityEngine;

namespace Shop
{
    public class ShopItemList : MonoBehaviour
    {
        [SerializeField] private Transform buyButtonView;
        [SerializeField] private ShopButton buyButtonPrefab;
        [SerializeField] private ShopItem[] items;

        private void Start()
        {
            Array.Sort(items);
            RecreateShopItems();
        }

        public void RecreateShopItems()
        {
            for (int i = buyButtonView.childCount - 1; i >= 0; i--)
                Destroy(buyButtonView.GetChild(i).gameObject);

            foreach (ShopItem item in items)
            {
                ShopButton button = Instantiate(buyButtonPrefab, buyButtonView);
                button.Initialize(item);
            }
        }
    }
}