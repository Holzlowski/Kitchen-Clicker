using Singletons;
using UnityEngine;
using System;

namespace Shop
{
    [CreateAssetMenu(fileName = "ShopItem", menuName = "ScriptableObjects/Shop Items/Generic Item", order = 0)]
    public class ShopItem : ScriptableObject, IComparable
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

        public int CompareTo(object obj)
        {
            if (obj is ShopItem item) {
                if (this.price < item.price) return -1;
                if (this.price == item.price) return 0;
                return 1;
            }
            return 1;
        }
    }
}