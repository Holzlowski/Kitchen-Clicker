using UnityEngine;

namespace PizzaGame
{
    [CreateAssetMenu(fileName = "Ingredient", menuName = "ScriptableObjects/Ingredient", order = 0)]
    public class IngredientType : ScriptableObject
    {
        [SerializeField] private int value;
        [SerializeField] private Ingredient prefab;
        [SerializeField] private Slot slotPrefab;

        public int Value => value;
        public Ingredient Prefab => prefab;
        public Slot SlotPrefab => slotPrefab;
    }
}