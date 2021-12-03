using MiniGames;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public Pizza pizza;
    public Wallet wallet;

    private void OnTriggerEnter(Collider other)
    {
        var ingredient = other.GetComponent<Ingredient>();
        if (ingredient == null)
        {
            Destroy(other.gameObject);
            return;
        }

        if (ingredient.IsInPlace) {
            return;
        }

        pizza.AddHit();
        ingredient.IsInPlace = true;
        wallet.AddMoney(ingredient.Cost);
        Debug.Log("nice!");
        Debug.Log(wallet.money);
        Destroy(gameObject);
    }
}