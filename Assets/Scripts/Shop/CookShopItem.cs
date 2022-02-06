using System;
using Idle.Cook;
using Singletons;
using UnityEngine;

namespace Shop
{
    [CreateAssetMenu(fileName = "ShopItem", menuName = "ScriptableObjects/Cook Shop Item", order = 0)]
    public class CookShopItem : ShopItem
    {
        [SerializeField] private float growthRate = 1;

        public override int Price =>
            (int)Math.Ceiling(price * Math.Pow(growthRate, CookManager.GetCookCountForType(buyable as CookGenerator)));

        public override bool AllowMultiBuy => true;
    }
}