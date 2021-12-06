using UnityEngine;

namespace MiniGames
{
    public class Ingredient : MonoBehaviour
    {
        [SerializeField] private IngredientType type;

        public IngredientType Type => type;

        public bool IsInPlace { get; set; }
    }
}