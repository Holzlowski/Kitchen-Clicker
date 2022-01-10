using System.Collections.Generic;
using Singletons;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PizzaGame
{
    public class Pizza : MonoBehaviour
    {
        [Header("Slot Spawn Options")] [SerializeField]
        private float spawnRadius = 1;

        [SerializeField] private int slotCount = 5;
        [SerializeField] private float minDist;
        [SerializeField] private int maxAttempts;
       
        [Header("Ingredient Spawn Options")] [SerializeField]
        private float fallingDistance = 20f;

        [Header("Pizza Rotation")] [SerializeField]
        private float degreesPerSecond = 20f;

        private readonly List<Vector3> _slotCoords = new List<Vector3>();
        private int _slotHits;
        private Recipe _recipe;
        [SerializeField] private IngredientType currentIngredient, nextIngredient;
        public IngredientType getCurrentIngredient => currentIngredient;

        private void Start()
        {
            ObjectToCenter();
            SlotSpawns();

            currentIngredient = _recipe.GetRandomIngredient();
            nextIngredient = _recipe.GetRandomIngredient();
            UIManager.showIngredient();
        }

        private void Update()
        {
            RotatePizza();

            IngredientSpawnWithClick();
            
            if (_slotHits != slotCount)
                return;

            FinishPizza();
        }

        public void AddHit() => _slotHits++;

        public void AddRecipe(Recipe currentRecipe) => _recipe = currentRecipe;

        private void ObjectToCenter() => transform.position = Vector3.zero;

        private void SlotSpawns()
        {
            for (int i = 0; i < slotCount; i++)
            {
                var spawnPos = default(Vector3);
                var attempts = 0;
                while (attempts < maxAttempts)
                {
                    var circlePos = Random.insideUnitCircle * spawnRadius;
                    spawnPos = new Vector3(circlePos.x, 0.175f, circlePos.y);
                    var ok = true;
                    foreach (var slot in _slotCoords)
                    {
                        var dist = (slot - spawnPos).magnitude;
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

                _slotCoords.Add(spawnPos);
                var randomIngredient = _recipe.GetRandomIngredient();
                var slotInstance = Instantiate(randomIngredient.SlotPrefab, spawnPos, Quaternion.identity, transform);
                slotInstance.Initialize(randomIngredient);
                slotInstance.pizza = this;
                slotInstance.gameObject.SetActive(true);
            }
        }

        private void RotatePizza() => transform.Rotate(new Vector3(0, degreesPerSecond, 0) * Time.deltaTime);

        private void IngredientSpawnWithClick()
        {   

            if (!Input.GetMouseButtonDown(0) || EventSystem.current.IsPointerOverGameObject())
                return;

            Ingredient randomIngredientPrefab = currentIngredient.Prefab;
            currentIngredient = nextIngredient;
            UIManager.showIngredient();
            nextIngredient = _recipe.GetRandomIngredient();

            Ray ray = KitchenManagement.GetMainCamera().ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction, Color.black, 100);

            if (!Physics.Raycast(ray, out RaycastHit hit) || !hit.transform.CompareTag("Pizza"))
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
            KitchenManagement.DestroyFinishedPizza();
            KitchenManagement.GenerateRandomPizza();
        }   
    }
}