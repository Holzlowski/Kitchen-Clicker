using UnityEngine;

namespace MiniGames
{
    [CreateAssetMenu(fileName = "Recipe", menuName = "ScriptableObjects/Recipe", order = 0)]
    public class Recipe : ScriptableObject
    {
        [SerializeField] private Ingredient[] ingredients;
        [SerializeField] private int bonus;

        public Ingredient[] Ingredients => ingredients;
        public int Bonus => bonus;

        public Ingredient GetRandomIngredient() => Ingredients[Random.Range(0, Ingredients.Length)];
    }
}