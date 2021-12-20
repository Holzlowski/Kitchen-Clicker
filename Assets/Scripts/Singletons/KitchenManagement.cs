using System;
using System.Collections.Generic;
using PizzaGame;
using UnityEngine;

namespace Singletons
{
    public class KitchenManagement : MonoBehaviour
    {
        [SerializeField] private Recipe startRecipe;

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
                startRecipe
            };
        }

        private void Update()
        {
            // TODO: Add loop for passive currency generation based on bought items
        }

        public static void AddRecipe(Recipe recipe) => Instance._availableRecipes.Add(recipe);

        public static List<Recipe> GetAvailableRecipes() => Instance._availableRecipes;

        public void GenerateRandomPizza()
        {
            // TODO: Generate a random pizza from the list of available recipes
            throw new NotImplementedException();
        }
    }
}