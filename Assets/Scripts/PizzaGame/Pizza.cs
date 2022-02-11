using System.Collections.Generic;
using Singletons;
using UnityEngine;
using UnityEngine.EventSystems;
using Util;

namespace PizzaGame
{
    public class Pizza : MonoBehaviour
    {
        [Header("Slot Spawn Options")] [SerializeField]
        private float spawnRadius = 1;

        [SerializeField] private float minDist;
        [SerializeField] private int maxAttempts;

        [Header("Ingredient Spawn Options")] [SerializeField]
        private float fallingDistance = 20f;

        [SerializeField] private List<ParticleSystem> particleEffects = new List<ParticleSystem>();

        [Header("Camera Options")] [SerializeField]
        private float camOffset;

        private readonly List<Slot> _slots = new List<Slot>();
        private int _slotCount;
        private int _slotHits;
        private Recipe _recipe;
        private IngredientType _currentIngredient;
        private IngredientType _nextIngredient;
        private Camera _camera;
        private Transform _camTransform;

        public IngredientType CurrentIngredient => _currentIngredient;

        private AudioSource pizzaAudio;

        private void Start()
        {
            SlotSpawns();

            _currentIngredient = _slots.Random().GetIngredientType();
            _nextIngredient = _slots.Random().GetIngredientType();

            UIManager.ShowIngredient();

            pizzaAudio = GetComponent<AudioSource>();
        }

        private void Update()
        {
            RotatePizza(KitchenManagement.GetPizzaRotationSpeed());

            IngredientSpawnWithClick();

            if (_slotHits != _slotCount)
                return;

            FinishPizza();
            particleEffects.Random().Play();
        }

        public void AddHit() => _slotHits++;

        public void RemoveSlotFromList(Slot slot)
        {
            _slots.Remove(slot);
            if (_slots.Count > 0) _nextIngredient = _slots.Random().GetIngredientType();
        }

        public void Initialize(Recipe currentRecipe, float scale, int slotCount)
        {
            _recipe = currentRecipe;
            _slotCount = slotCount;

            transform.localScale *= scale;
            spawnRadius *= scale;

            if (!Camera.main)
            {
                Debug.LogError("Main camera not found");
                return;
            }

            _camera = Camera.main;
            _camTransform = _camera.transform;
            PositionCamera(scale);
        }

        private void PositionCamera(float pizzaScale)
        {
            float zoomFactor = camOffset * pizzaScale;
            _camTransform.position = Vector3.up * zoomFactor;
        }

        private void SlotSpawns()
        {
            for (int i = 0; i < _slotCount; i++)
            {
                var spawnPos = default(Vector3);
                var attempts = 0;
                while (attempts < maxAttempts)
                {
                    var circlePos = Random.insideUnitCircle * spawnRadius;
                    spawnPos = new Vector3(circlePos.x, 0.175f, circlePos.y);
                    var ok = true;
                    foreach (var slot in _slots)
                    {
                        var dist = (slot.transform.position - spawnPos).magnitude;
                        if (dist < minDist)
                        {
                            ok = false;
                            break;
                        }
                    }

                    if (ok)
                    {
                        break;
                    }

                    attempts++;
                }

                IngredientType randomIngredient = _recipe.GetRandomIngredient();
                // Get slot position on pizza
                Vector3 camPosition = _camTransform.position;
                Vector3 direction = (spawnPos - camPosition).normalized;
                Physics.SyncTransforms();
                Physics.Raycast(camPosition, direction, out RaycastHit hit);
                // Instantiate, position and initialize the slot
                Slot slotInstance = Instantiate(randomIngredient.SlotPrefab, hit.point, Quaternion.identity);
                slotInstance.transform.parent = transform;
                slotInstance.Initialize(this, randomIngredient);
                _slots.Add(slotInstance);
            }
        }

        private void RotatePizza(float degreesPerSecond) =>
            transform.Rotate(new Vector3(0, degreesPerSecond, 0) * Time.deltaTime);

        private void IngredientSpawnWithClick()
        {
            if (!Input.GetMouseButtonDown(0) || EventSystem.current.IsPointerOverGameObject())
                return;
            SpawnIngredient();
        }

        public void SpawnIngredient()
        {
            Ingredient randomIngredientPrefab = _currentIngredient.Prefab;
            _currentIngredient = _nextIngredient;
            UIManager.ShowIngredient();

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction, Color.black, 100);

            if (!Physics.Raycast(ray, out RaycastHit hit))
                return;

            Vector3 fallingPosition = new Vector3(hit.point.x, hit.point.y + fallingDistance, hit.point.z);
            Instantiate(randomIngredientPrefab, fallingPosition,
                Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
        }

        private void OnDrawGizmosSelected() => Gizmos.DrawWireSphere(transform.position, spawnRadius);

        private void FinishPizza()
        {
            Wallet.AddMoney(_recipe.Bonus);
            KitchenManagement.DestroyAllIngredients();
            SoundEffectManager.getrandomCompleteSoundEffect();
            KitchenManagement.DestroyPizza();
            KitchenManagement.GenerateRandomPizza();
        }
    }
}