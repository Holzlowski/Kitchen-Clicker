namespace Singletons
{
    public class Wallet : Singleton<Wallet>
    {
        private int _money;

        public static int GetBalance() => Instance._money;

        public static void AddMoney(int amount) => Instance._money += amount;

        public static bool RemoveMoney(int amount)
        {
            if (Instance._money < amount)
                return false;

            Instance._money -= amount;
            return true;
        }
    }
}