using UnityEngine;

namespace PizzaGame
{
    public class Ingredient : MonoBehaviour
    {
        [SerializeField] private IngredientType type;
        [SerializeField] static float constraint = -10;
        public GameObject splashEffect;
        Rigidbody rb;

        public IngredientType Type => type;

        public bool IsInPlace { get; set; }

        private void Start() {
            rb = GetComponent<Rigidbody>();

        }

        private void Update() {
            selfDestruction(constraint);
        }

        private void OnCollisionEnter(Collision other)
        {
            // If ingredient lands on pizza, stick it to pizza
            if (other.gameObject.CompareTag("Pizza"))
            {
                transform.parent = other.transform;
                playSplashEffect();
            }

        }

        private void selfDestruction(float constraint){
            if(transform.position.y < constraint){
                Destroy(gameObject);
            }
        }

        public void playSplashEffect(){
            if(splashEffect != null){
                Instantiate(splashEffect, transform.position, Quaternion.identity);
            }
           
        }
    }
}