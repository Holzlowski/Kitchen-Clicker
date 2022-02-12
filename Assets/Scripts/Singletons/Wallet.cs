using UnityEngine;

namespace Singletons
{
    public class Wallet : Singleton<Wallet>
    {
        private int _money;
        [SerializeField] private int _levelBreakPoint = 1000;

        public static int GetBalance() => Instance._money;

        public static void AddMoney(int amount)
        {
            ChangeMoney(amount);
        }

        public static bool RemoveMoney(int amount)
        {
            if (Instance._money < amount)
                return false;

            ChangeMoney(-amount);
            return true;
        }

        private static void ChangeMoney(int amount)
        {
            Instance._money += amount;
            if (Instance._money > Instance._levelBreakPoint && 
                KitchenManagement.GetLengthAvailableRecipes() == UIManager.GetLengthShopRecipes())
            {
                KitchenManagement.LevelUp();
            }
        }

        public static void ResetWallet()
        {
            Instance._money = 0;
        }
    }
}