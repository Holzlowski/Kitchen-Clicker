using UnityEngine;

namespace Shop
{
    public abstract class BuyableObject : ScriptableObject
    {
        public abstract void Buy();
    }
}