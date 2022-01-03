using UnityEngine;

namespace PizzaGame
{
    [CreateAssetMenu(fileName = "Ingredient", menuName = "ScriptableObjects/Ingredient", order = 0)]
    public class IngredientType : ScriptableObject
    {
        [SerializeField] private int value;
        [SerializeField] private GameObject prefab;
        [SerializeField] private Slot slotPrefab;

        public int Value => value;
        public GameObject Prefab => prefab;
        public Slot SlotPrefab => slotPrefab;
    }
}