using MiniGames;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public Pizza pizza;

    private IngredientType _ingredientType;

    public void Initialize(IngredientType ingredientType) => _ingredientType = ingredientType;

    private void OnTriggerEnter(Collider other)
    {
        var ingredient = other.GetComponent<Ingredient>();
        if (ingredient == null || !ingredient.Type.Equals(_ingredientType))
        {
            Debug.Log(ingredient.Type);
            Debug.Log(_ingredientType);
            Destroy(other.gameObject);
            return;
        }

        if (ingredient.IsInPlace)
            return;

        pizza.AddHit();
        ingredient.IsInPlace = true;
        Wallet.AddMoney(ingredient.Type.Value);
        Debug.Log("nice!");
        Debug.Log(Wallet.GetBalance());
        Destroy(gameObject);
    }
}