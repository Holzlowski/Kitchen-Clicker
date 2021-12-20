using System.Collections.Generic;
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
        [SerializeField] private Wallet wallet; // TODO: Refactor wallet access here and in Slot.cs

        [SerializeField] private GameObject ingredientPrefab;

        [SerializeField] private float distanceToCamera = 0.85f;
        [SerializeField] private float degreesPerSecond = 20f;
        [SerializeField] private float fallingDistance = 0.5f;

        [SerializeField] private Camera mainCamera;
        private Transform _camTransform;

        private int _slotHits;

        private void Start()
        {
            _camTransform = mainCamera.transform;
            _camTransform.eulerAngles = new Vector3(90, 0, 0);
            _camTransform.position = new Vector3(0, distanceToCamera, 0);

            ObjectToCenter();
            SlotSpawns();
        }

        private void Update()
        {
            _camTransform.position = new Vector3(0, distanceToCamera, 0);

            RotatePizza();

            IngredientSpawnWithClick();

            if (_slotHits != slotCount)
                return;

            // Pizza finished
            Debug.Log("finished");
            wallet.AddMoney(recipe.Bonus);
            // TODO: Fix looping for infinite bonus
        }

        private void OnCollisionEnter(Collision other)
        {
            // If ingredient lands on pizza, stick it to pizza
            if (other.gameObject.CompareTag("Ingredient"))
                other.transform.parent = transform;
        }

        public void AddHit() => _slotHits++;

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
                var slotInstance = Instantiate(slotPrefab, spawnPos, Quaternion.identity, transform);
                var randomIngredient = recipe.GetRandomIngredient();
                slotInstance.Initialize(randomIngredient);
                slotInstance.gameObject.SetActive(true);
                slots.Add(slotInstance);
            }
        }

        private void RotatePizza() => transform.Rotate(new Vector3(0, degreesPerSecond, 0) * Time.deltaTime);

        private void IngredientSpawnWithClick()
        {
            if (!Input.GetMouseButtonDown(0)) return;

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction, Color.black, 100);

            if (!Physics.Raycast(ray, out RaycastHit hit) || !hit.transform.CompareTag("Pizza"))
                return;

            Vector3 fallingPosition = new Vector3(hit.point.x, hit.point.y + fallingDistance, hit.point.z);
            // TODO: scale this list of different ingredients
            Instantiate(ingredientPrefab, fallingPosition, Quaternion.identity);
        }

        private void OnDrawGizmosSelected() => Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}