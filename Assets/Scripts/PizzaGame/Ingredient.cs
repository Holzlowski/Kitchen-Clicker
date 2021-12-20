using UnityEngine;

namespace PizzaGame
{
    public class Ingredient : MonoBehaviour
    {
        [SerializeField] private IngredientType type;

        public IngredientType Type => type;

        public bool IsInPlace { get; set; }
    }
}