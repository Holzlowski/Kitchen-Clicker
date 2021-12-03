using UnityEngine;

namespace MiniGames
{
    public class Ingredient : MonoBehaviour
    {
        [SerializeField] private int cost;

        public bool IsInPlace { get; set; }
        public int Cost => cost; 
    }
}