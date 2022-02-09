using System.Collections;
using System.Collections.Generic;
using PizzaGame;
using UnityEngine;
using Util;

namespace Singletons
{
    public class KitchenManagement : Singleton<KitchenManagement>
    {
        [SerializeField] private Recipe startRecipe;
        [SerializeField] private Pizza pizzaPrefab;
        [SerializeField] private List<ParticleSystem> particleEffects = new List<ParticleSystem>();
        [SerializeField] private float degreesPerSecond = 25f;
        private Pizza _currentPizza;
        private int _level = 1;
        [SerializeField] private int slotCount = 5;
        private List<Recipe> _availableRecipes;
        private bool _upgradeFlag;

        private void Start()
        {
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
            GetCurrentIngredientSprite();
            StartCoroutine(DeceleratorUpgrade());
        }

        public static int GetLevel() => Instance._level;
        public static void AddRecipe(Recipe recipe) => Instance._availableRecipes.Add(recipe);
        public static bool GetUpgradeFlag() => Instance._upgradeFlag;
        public static void SetUpgradeFlag(bool flag) => Instance._upgradeFlag = flag;
        public static float GetPizzaRotationSpeed() => Instance.degreesPerSecond;

        public static void SetPizzaRotationSpeed(float degreesPerSecond) =>
            Instance.degreesPerSecond = degreesPerSecond;

        public static List<Recipe> GetAvailableRecipes() => Instance._availableRecipes;

        public static void GenerateRandomPizza()
        {
            Instance._currentPizza = Instantiate(Instance.pizzaPrefab, Vector3.zero, Quaternion.identity);
            float scale = 1 + Instance._level * 0.1f;
            Instance._currentPizza.Initialize(Instance._availableRecipes.Random(), scale, Instance.slotCount);
        }

        public static Sprite GetCurrentIngredientSprite()
        {
            return !Instance._currentPizza.CurrentIngredient
                ? null
                : Instance._currentPizza.CurrentIngredient.ingredientImage;
        }

        public static void DestroyPizza()
        {
            Destroy(Instance._currentPizza.gameObject);
            Instance.particleEffects.Random().Play();
        }

        public static void DestroyAllIngredients()
        {
            GameObject[] ingredients = GameObject.FindGameObjectsWithTag("Ingredient");
            foreach (GameObject ingredient in ingredients)
                Destroy(ingredient);
        }

        public static void LevelUp()
        {
            DestroyPizza();
            DestroyAllIngredients();
            ResetRecipes();
            Wallet.ResetWallet();
            CookManager.DeleteAllCooks();

            IEnumerator NextLevel()
            {
                yield return new WaitForEndOfFrame();
                Instance._level++;
                Instance.slotCount++;
                GenerateRandomPizza();
            }

            Instance.StartCoroutine(NextLevel());
        }

        private static void ResetRecipes() => Instance._availableRecipes = new List<Recipe> { Instance.startRecipe };

        private IEnumerator DeceleratorUpgrade()
        {
            while (_upgradeFlag)
            {
                SetPizzaRotationSpeed(10f);
                yield return new WaitForSeconds(15);
                SetUpgradeFlag(false);
                SetPizzaRotationSpeed(25f);
            }
        }
    }
}