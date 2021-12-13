using UnityEngine;

namespace Shop
{
    [CreateAssetMenu(fileName = "ShopItem", menuName = "Shop Item", order = 0)]
    public class ShopItem : ScriptableObject
    {
        [SerializeField] private Sprite icon;

        public Sprite Icon => icon;
    }
}