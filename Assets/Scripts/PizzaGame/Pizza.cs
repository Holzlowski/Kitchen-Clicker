using System.Collections.Generic;
using Singletons;
using UnityEngine;

namespace PizzaGame
{
    public class Pizza : MonoBehaviour
    {
        [SerializeField] private float spawnRadius = 1;
        [SerializeField] private int slotCount = 5;
        [SerializeField] private float minDist;
        [SerializeField] private int maxAttempts;
        [SerializeField] private Slot slotPrefab;
        [SerializeField] private List<Vector3> slotCoords = new List<Vector3>();
        [SerializeField] private List<Slot> slots = new List<Slot>();
        [SerializeField] private Recipe recipe;
        [SerializeField] private float fallingDistance = 20f;
        [SerializeField] private float degreesPerSecond = 20f;
        private GameObject ingredientPrefab;
        private int _slotHits;

        private void Start()
        {
            ObjectToCenter();
            SlotSpawns();
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

        public void AddRecipe(Recipe currentRecipe) => recipe = currentRecipe;

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
                    foreach (var slot in slotCoords)
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

                slotCoords.Add(spawnPos);
                var randomIngredient = recipe.GetRandomIngredient();
                var slotInstance = Instantiate(randomIngredient.SlotPrefab, spawnPos, Quaternion.identity, transform);
                slotInstance.Initialize(randomIngredient);
                slotInstance.pizza = this;
                slotInstance.gameObject.SetActive(true);
                slots.Add(slotInstance);
            }
        }

        private void RotatePizza() => transform.Rotate(new Vector3(0, degreesPerSecond, 0) * Time.deltaTime);

        private void IngredientSpawnWithClick()
        {
            if (!Input.GetMouseButtonDown(0)) return;

            Ingredient randomIngredientPrefab = recipe.GetRandomIngredient().Prefab;
            Ray ray = KitchenManagement.GetMainCamera().ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction, Color.black, 100);

            if (!Physics.Raycast(ray, out RaycastHit hit) || !hit.transform.CompareTag("Pizza"))
                return;

            Vector3 fallingPosition = new Vector3(hit.point.x, hit.point.y + fallingDistance, hit.point.z);
            var ingredientInstance = Instantiate(randomIngredientPrefab, fallingPosition,
                Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
        }

        private void OnDrawGizmosSelected() => Gizmos.DrawWireSphere(transform.position, spawnRadius);

        private void FinishPizza()
        {
            Wallet.AddMoney(recipe.Bonus);
            KitchenManagement.DestroyAllIngredients();
            KitchenManagement.DestroyFinishedPizza();
            KitchenManagement.GenerateRandomPizza();
        }
    }
}