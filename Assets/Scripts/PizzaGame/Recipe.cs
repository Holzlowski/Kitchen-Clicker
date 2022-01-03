using Shop;
using Singletons;
using UnityEngine;

namespace PizzaGame
{
    [CreateAssetMenu(fileName = "Recipe", menuName = "ScriptableObjects/Recipe", order = 0)]
    public class Recipe : BuyableObject
    {
        [SerializeField] private IngredientType[] ingredients;
        [SerializeField] private int bonus;

        public IngredientType[] Ingredients => ingredients;
        public int Bonus => bonus;

        public IngredientType GetRandomIngredient() => Ingredients[Random.Range(0, Ingredients.Length)];

        public override void Buy() => KitchenManagement.AddRecipe(this);
    }
}