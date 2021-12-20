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
            foreach (ShopItem item in items)
            {
                ShopButton button = Instantiate(buyButtonPrefab, buyButtonView);
                button.Initialize(item);
            }
        }
    }
}