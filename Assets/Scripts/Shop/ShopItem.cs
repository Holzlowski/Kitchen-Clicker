using Singletons;
using UnityEngine;

namespace Shop
{
    [CreateAssetMenu(fileName = "ShopItem", menuName = "ScriptableObjects/Shop Item", order = 0)]
    public class ShopItem : ScriptableObject
    {
        [SerializeField] private Sprite icon;
        [SerializeField] private int price;
        [SerializeField] private BuyableObject buyable;
        [SerializeField] private bool allowMultiBuy;

        public Sprite Icon => icon;
        public int Price => price;
        public bool AllowMultiBuy => allowMultiBuy;

        public bool Buy()
        {
            if (!Wallet.RemoveMoney(price))
            {
                UIManager.ShowNotification("Not enough money");
                return false;
            }

            if (buyable == null)
            {
                Debug.LogError($"Shop item \"{name}\" doesn't have a reference to a buyable object");
                return false;
            }

            buyable.Buy();
            return true;
        }
    }
}