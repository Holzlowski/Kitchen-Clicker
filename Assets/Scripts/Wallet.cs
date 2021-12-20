using UnityEngine;

public class Wallet : MonoBehaviour
{
    private static Wallet Instance
    {
        get
        {
            if (instance != null)
                return instance;
            Debug.LogError("Wallet instance doesn't exist in scene (may cause a NullPointerException)");
            return null;
        }
    }

    private int _money;
    private static Wallet instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("There is more than one wallet instance in this scene");
            Destroy(this);
        }

        instance = this;
    }

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