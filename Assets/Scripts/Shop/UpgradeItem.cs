using Singletons;
using UnityEngine;

namespace Shop
{
    [CreateAssetMenu(fileName = "UpgradeItem", menuName = "ScriptableObjects/Upgrade Item", order = 0)]
    public class UpgradeItem : ShopItem
    {
        public override bool Buy() 
        {
            if (KitchenManagement.GetUpgradeFlag()) {
                UIManager.ShowNotification("There is already an upgrade in use.");
                return false;
            }
            return base.Buy();
        }
    }
}