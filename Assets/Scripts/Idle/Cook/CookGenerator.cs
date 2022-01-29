using Shop;
using Singletons;
using UnityEngine;

namespace Idle.Cook
{
    [CreateAssetMenu(fileName = "Cook", menuName = "ScriptableObjects/Cook", order = 0)]
    public class CookGenerator : BuyableObject
    {
        [SerializeField] [Range(0, 1)] private float errorRate = 0.8f;
        [SerializeField] [Range(0, 1)] private float efficiency = 0.1f;

        [SerializeField] private CookVisualisation cookPrefab;

        public override void Buy() => CookManager.AddCook(errorRate, efficiency, cookPrefab);
    }
}