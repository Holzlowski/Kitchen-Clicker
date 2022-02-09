using Singletons;
using UnityEngine;

namespace Shop
{
    [CreateAssetMenu(fileName = "UpgradeItem", menuName = "ScriptableObjects/Shop Items/Upgrade", order = 1)]
    public class UpgradeShopItem : ShopItem
    {
        public override bool Buy()
        {
            if (KitchenManagement.GetUpgradeFlag())
            {
                UIManager.ShowNotification("There is already an upgrade in use.");
                return false;
            }

            return base.Buy();
        }
    }
}