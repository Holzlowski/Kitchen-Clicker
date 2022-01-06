using UnityEngine;
using UnityEngine.UI;

namespace PizzaGame
{
    [CreateAssetMenu(fileName = "Ingredient", menuName = "ScriptableObjects/Ingredient", order = 0)]
    public class IngredientType : ScriptableObject
    {
        [SerializeField] private int value;
        [SerializeField] private Ingredient prefab;
        [SerializeField] private Slot slotPrefab;
        [SerializeField] private Sprite sprite;

        public int Value => value;
        public Ingredient Prefab => prefab;
        public Slot SlotPrefab => slotPrefab;

        public Sprite ingredientImage => sprite;
    }
}