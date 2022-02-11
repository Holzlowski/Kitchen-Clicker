using Singletons;
using UnityEngine;

namespace Shop
{
    [CreateAssetMenu(fileName = "ShopItem", menuName = "ScriptableObjects/Shop Items/Generic Item", order = 0)]
    public class ShopItem : ScriptableObject
    {
        [SerializeField] private Sprite icon;
        [SerializeField] protected int price;
        [SerializeField] protected BuyableObject buyable;
        [SerializeField] private bool allowMultiBuy;

        public Sprite Icon => icon;
        public virtual int Price => price;
        public virtual bool AllowMultiBuy => allowMultiBuy;

        public virtual bool Buy()
        {
            if (!Wallet.RemoveMoney(Price))
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