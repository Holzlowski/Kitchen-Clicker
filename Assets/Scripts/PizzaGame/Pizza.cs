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
        private int slotCount;
        [SerializeField] private float minDist;
        [SerializeField] private int maxAttempts;
       
        [Header("Ingredient Spawn Options")] [SerializeField]
        private float fallingDistance = 20f;

        [Header("Pizza Rotation")] [SerializeField]
        private float degreesPerSecond = 20f;

        private readonly List<Slot> _slots = new List<Slot>();
        private int _slotHits;
        private int randomIndex;
        private Recipe _recipe;
        [SerializeField] private IngredientType currentIngredient, nextIngredient;
        public IngredientType getCurrentIngredient => currentIngredient;

        private void Start()
        {
            ObjectToCenter();
            SlotSpawns();

            currentIngredient = _slots[randomIndex].GetIngredientType();
            nextIngredient = _slots[randomIndex].GetIngredientType();
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

        public void RemoveSlotFromList(Slot slot) {
            _slots.Remove(slot);
            randomIndex = Random.Range(0, _slots.Count);
            if (_slots.Count > 0) nextIngredient = _slots[randomIndex].GetIngredientType();
        }

        public void Initialize(Recipe currentRecipe, Vector3 scale, int slotCount) {
            _recipe = currentRecipe;
            this.slotCount = slotCount;
            // TODO: Scale richtig draufaddieren (nicht jedes mal wenn neue Pizza generiert wird)
            // this.transform.localScale += scale;
        }

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

                var randomIngredient = _recipe.GetRandomIngredient();
                var slotInstance = Instantiate(randomIngredient.SlotPrefab, spawnPos, Quaternion.identity, transform);
                slotInstance.Initialize(randomIngredient);
                slotInstance.pizza = this;
                slotInstance.gameObject.SetActive(true);
                _slots.Add(slotInstance);
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

            Ray ray = KitchenManagement.GetMainCamera().ScreenPointToRay(Input.mousePosition);
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
            KitchenManagement.DestroyFinishedPizza();
            KitchenManagement.GenerateRandomPizza();
        }   
    }
}