using Singletons;
using UnityEngine;

namespace PizzaGame
{
    public class Ingredient : MonoBehaviour
    {
        [SerializeField] private IngredientType type;
        [SerializeField] private float constraint = -10;
        [SerializeField] private GameObject splashEffect;

        public IngredientType Type => type;
        public bool IsInPlace { get; set; }

        private bool _isVisualizedIngredient = false;

        public void Initialize(bool isVisualizedIngredient)
        {
            _isVisualizedIngredient = isVisualizedIngredient;
            if (_isVisualizedIngredient)
                gameObject.tag = "Untagged";
        }

        private void Update() => SelfDestruction(constraint);

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Pizza"))
                return;

            // If ingredient lands on pizza, stick it to pizza
            transform.parent = other.transform;
            PlaySplashEffect();
        }

        private void SelfDestruction(float constraint)
        {
            if (transform.position.y < constraint)
                Destroy(gameObject);
        }

        private void PlaySplashEffect()
        {
            if (splashEffect != null)
            {
                GameObject effect = Instantiate(splashEffect, transform.position, Quaternion.identity);
                if (!_isVisualizedIngredient)
                    SoundEffectManager.getrandomSplashSoundEffect();
                Destroy(effect, 2);
            }
        }
    }
}