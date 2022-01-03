using System;
using System.Collections.Generic;
using PizzaGame;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Singletons
{
    public class KitchenManagement : MonoBehaviour
    {
        [SerializeField] private Recipe startRecipe;
        [SerializeField] private Pizza pizzaPrefab;
        [SerializeField] private Camera _mainCamera;
        private Transform _camTransform;
        [SerializeField] private float distanceToCamera = 0.85f;
        // TODO: Delete this and do it in Pizza.cs
        [SerializeField] private GameObject ingredientPrefab;
        public Pizza currentPizza;

        private static KitchenManagement Instance
        {
            get
            {
                if (instance != null)
                    return instance;
                Debug.LogError("KitchenManagement instance doesn't exist in scene (may cause a NullPointerException)");
                return null;
            }
        }

        private static KitchenManagement instance;

        private List<Recipe> _availableRecipes;
        // TODO: Add list of other bought items (e.g. cooks, stoves -> passive generators)

        private void Start()
        {
            Instance._camTransform = Instance._mainCamera.transform;
            Instance._camTransform.eulerAngles = new Vector3(90, 0, 0);
            Instance._camTransform.position = new Vector3(0, distanceToCamera, 0);
            
            GenerateRandomPizza();
        }

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("There is more than one KitchenManagement instance in this scene");
                Destroy(this);
            }

            instance = this;

            _availableRecipes = new List<Recipe>
            {
                startRecipe,
            };
        }

        private void Update()
        {
            _camTransform.position = new Vector3(0, distanceToCamera, 0);

            // TODO: Add loop for passive currency generation based on bought items
        }

        public static void AddRecipe(Recipe recipe) => Instance._availableRecipes.Add(recipe);
        public static Camera GetMainCamera() => Instance._mainCamera;

        public static List<Recipe> GetAvailableRecipes() => Instance._availableRecipes;

        public static void GenerateRandomPizza()
        {
            int randomIndex = Random.Range(0, Instance._availableRecipes.Count);
            Instance.currentPizza = Instantiate(Instance.pizzaPrefab);
            Instance.currentPizza.AddRecipe(Instance._availableRecipes[randomIndex]);
            // TODO: Remove here and do it in pizza dependent on the ingredientlist of the recipe
            Instance.currentPizza.AddIngredienPrefab(Instance.ingredientPrefab);

            // TODO: Add function to set GameObject name (maybe to recipe name + "Pizza" addtion)
            // Instance.currentPizza.name = Instance._availableRecipes[randomIndex].GetName;
        }

        public static void DestoryFinishedPizza()
        {
            Destroy(GameObject.FindWithTag("Pizza"));
        }
        public static void DestroyAllIngredients(){
            GameObject[] ingredients = GameObject.FindGameObjectsWithTag("Ingredient");
            foreach(GameObject ingredient in ingredients){
                Destroy(ingredient);
            }
                
        }
    }
}