using Shop;
using Singletons;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "ScriptableObjects/Upgrades", order = 0)]
public class Upgrade : BuyableObject
{
    public override void Buy() => KitchenManagement.SetUpgradeFlag(true);
}