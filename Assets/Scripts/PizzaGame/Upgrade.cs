using Shop;
using Singletons;
using UnityEngine;
using UnityEngine.Events;

namespace PizzaGame
{
    [CreateAssetMenu(fileName = "Upgrade", menuName = "ScriptableObjects/Upgrade", order = 0)]
    public class Upgrade : BuyableObject
    {
        [SerializeField] private int duration = 5;
        [SerializeField] private UnityEvent<int> upgradeAction;

        public override void Buy()
        {
            KitchenManagement.SetUpgradeFlag(true);
            if (upgradeAction == null)
            {
                Debug.LogWarning("Upgrade Action is not specified! Nothing will happen");
                return;
            }

            upgradeAction?.Invoke(duration);
            KitchenManagement.UpgradeCooldown(duration);
        }
    }
}