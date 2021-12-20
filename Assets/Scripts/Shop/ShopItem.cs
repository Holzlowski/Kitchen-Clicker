using UnityEngine;

namespace Shop
{
    [CreateAssetMenu(fileName = "ShopItem", menuName = "ScriptableObjects/Shop Item", order = 0)]
    public class ShopItem : ScriptableObject
    {
        [SerializeField] private Sprite icon;
        [SerializeField] private int price;
        [SerializeField] private BuyableObject buyable;

        public Sprite Icon => icon;
        public int Price => price;

        public bool Buy()
        {
            if (!Wallet.RemoveMoney(price))
                return false;

            buyable.Buy();
            return true;
        }
    }
}