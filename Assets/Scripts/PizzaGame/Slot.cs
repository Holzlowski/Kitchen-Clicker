using Singletons;
using UnityEngine;

namespace PizzaGame
{
    public class Slot : MonoBehaviour
    {
        private Pizza _pizza;
        private IngredientType _ingredientType;

        public void Initialize(Pizza pizza, IngredientType ingredientType)
        {
            _pizza = pizza;
            _ingredientType = ingredientType;
        }

        public IngredientType GetIngredientType() => _ingredientType;

        private void OnTriggerEnter(Collider other)
        {
            var ingredient = other.GetComponent<Ingredient>();
            if (ingredient == null || !ingredient.Type.Equals(_ingredientType) && !ingredient.IsInPlace)
            {
                Destroy(other.gameObject);
                return;
            }

            if (ingredient.IsInPlace)
                return;

            _pizza.AddHit();
            _pizza.RemoveSlotFromList(this);
            ingredient.IsInPlace = true;
            Wallet.AddMoney(ingredient.Type.Value);
            Destroy(gameObject);
        }
    }
}