using MiniGames;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public Pizza pizza;
    public Wallet wallet;
    [SerializeField] private Ingredient ingredientType;

    public void Initialize(Ingredient ingredientType) => this.ingredientType = ingredientType;

    private void OnTriggerEnter(Collider other)
    {
        var ingredient = other.GetComponent<Ingredient>();
        if (ingredient == null || !ingredient.name.Contains(ingredientType.name))
        {
            Destroy(other.gameObject);
            return;
        }

        if (ingredient.IsInPlace)
        {
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