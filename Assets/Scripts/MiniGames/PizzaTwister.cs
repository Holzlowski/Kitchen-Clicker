using UnityEngine;

namespace MiniGames
{
    public class PizzaTwister : MonoBehaviour
    {
        [SerializeField] private GameObject ingredientPrefab;

        [SerializeField] private float distanceToCamera = 0.85f;
        [SerializeField] private float degreesPerSecond = 20f;
        [SerializeField] private float fallingDistance = 0.5f;

        private Camera mainCamera;

        // Start is called before the first frame update
        private void Start()
        {
            mainCamera = Camera.main;
            mainCamera.transform.eulerAngles = new Vector3(90, 0, 0);
            mainCamera.transform.position = new Vector3(0, distanceToCamera, 0);

            ObjectToCenter();
        }

        // Update is called once per frame
        private void Update()
        {
            mainCamera.transform.position = new Vector3(0, distanceToCamera, 0);

            PizzaRotation();

            IngredientSpawnWithClick();
        }

        private void ObjectToCenter()
        {
            transform.position = Vector3.zero;
        }

        private void PizzaRotation()
        {
            transform.Rotate(new Vector3(0, degreesPerSecond, 0) * Time.deltaTime);
        }

        private void IngredientSpawnWithClick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                Debug.DrawRay(ray.origin, ray.direction, Color.black, 100);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.transform.CompareTag("Pizza"))
                    {
                        Vector3 fallingPosition = new Vector3(hit.point.x, hit.point.y + fallingDistance, hit.point.z);
                        // TODO: scale this list of different ingredients
                        Instantiate(ingredientPrefab, fallingPosition, Quaternion.identity);
                    }
                }
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Ingredient"))
            {
                other.transform.parent = transform;
            }
        }
    }
}