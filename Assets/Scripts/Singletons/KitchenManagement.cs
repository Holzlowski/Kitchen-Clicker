using System.Collections.Generic;
using PizzaGame;
using UnityEngine;

namespace Singletons
{
    public class KitchenManagement : Singleton<KitchenManagement>
    {
        [SerializeField] private Recipe startRecipe;
        [SerializeField] private Pizza pizzaPrefab;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private float distanceToCamera = 0.85f;

        private Transform _camTransform;
        private Pizza _currentPizza;

        private List<Recipe> _availableRecipes;
        // TODO: Add list of other bought items (e.g. cooks, stoves -> passive generators)

        private void Start()
        {
            Instance._camTransform = Instance.mainCamera.transform;
            Instance._camTransform.eulerAngles = new Vector3(90, 0, 0);
            Instance._camTransform.position = new Vector3(0, distanceToCamera, 0);

            GenerateRandomPizza();
        }

        protected override void Awake()
        {
            base.Awake();
            _availableRecipes = new List<Recipe>
            {
                startRecipe,
            };
        }

        private void Update()
        {
            _camTransform.position = new Vector3(_camTransform.rotation.x, distanceToCamera, _camTransform.rotation.z);
            getCurrentIngredientSprite();

            // TODO: Add loop for passive currency generation based on bought items
        }

        public static void AddRecipe(Recipe recipe) => Instance._availableRecipes.Add(recipe);
        public static Camera GetMainCamera() => Instance.mainCamera;

        public static List<Recipe> GetAvailableRecipes() => Instance._availableRecipes;

        public static void GenerateRandomPizza()
        {
            int randomIndex = Random.Range(0, Instance._availableRecipes.Count);
            Instance._currentPizza = Instantiate(Instance.pizzaPrefab);
            Instance._currentPizza.AddRecipe(Instance._availableRecipes[randomIndex]);
        }

        public static Sprite getCurrentIngredientSprite()
        {
            return Instance._currentPizza.getCurrentIngredient.ingredientImage;
        }

        public static void DestroyFinishedPizza()
        {
            Destroy(GameObject.FindWithTag("Pizza"));
        }

        public static void DestroyAllIngredients()
        {
            GameObject[] ingredients = GameObject.FindGameObjectsWithTag("Ingredient");
            foreach (GameObject ingredient in ingredients)
            {
                Destroy(ingredient);
            }
        }
    }
}