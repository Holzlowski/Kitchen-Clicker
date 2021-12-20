using Shop;
using UnityEngine;

namespace MiniGames
{
    [CreateAssetMenu(fileName = "Recipe", menuName = "ScriptableObjects/Recipe", order = 0)]
    public class Recipe : BuyableObject
    {
        [SerializeField] private IngredientType[] ingredients;
        [SerializeField] private int bonus;

        public IngredientType[] Ingredients => ingredients;
        public int Bonus => bonus;

        public IngredientType GetRandomIngredient() => Ingredients[Random.Range(0, Ingredients.Length)];

        // TODO: Implement unlock logic
        public override void Buy() => Debug.Log("Bought recipe " + name);
    }
}