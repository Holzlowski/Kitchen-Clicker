using UnityEngine;

namespace PizzaGame
{
    [CreateAssetMenu(fileName = "Ingredient", menuName = "ScriptableObjects/Ingredient", order = 0)]
    public class IngredientType : ScriptableObject
    {
        [SerializeField] private int value;

        public int Value => value;
    }
}