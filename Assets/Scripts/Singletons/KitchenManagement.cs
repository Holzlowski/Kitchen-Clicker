using System.Collections.Generic;
using System.Collections;
using PizzaGame;
using UnityEngine;
using Util;

namespace Singletons
{
    public class KitchenManagement : Singleton<KitchenManagement>
    {
        [SerializeField] private Recipe startRecipe;
        [SerializeField] private Pizza pizzaPrefab;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private float distanceToCamera = 0.85f;
        [SerializeField] private List<ParticleSystem> particleEffects = new List<ParticleSystem>();
        [SerializeField] private float degreesPerSecond = 25f;
        private Transform _camTransform;
        private Pizza _currentPizza;
        private int _level = 1;
        [SerializeField] private int slotCount = 5;
        private List<Recipe> _availableRecipes;
        private bool _upgradeFlag = false;

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
            Quaternion camRotation = _camTransform.rotation;
            _camTransform.position = new Vector3(camRotation.x, distanceToCamera, camRotation.z);
            GetCurrentIngredientSprite();
            StartCoroutine(DeceleratorUpgrade());
        }

        public static int GetLevel() => Instance._level;
        public static void AddRecipe(Recipe recipe) => Instance._availableRecipes.Add(recipe);
        public static Camera GetMainCamera() => Instance.mainCamera;
        public static bool GetUpgradeFlag() => Instance._upgradeFlag;
        public static void SetUpgradeFlag(bool flag) => Instance._upgradeFlag = flag;
        public static float GetPizzaRotationSpeed() => Instance.degreesPerSecond;
        public static void SetPizzaRotationSpeed(float degreesPerSecond) => Instance.degreesPerSecond = degreesPerSecond;

        public static List<Recipe> GetAvailableRecipes() => Instance._availableRecipes;

        public static void GenerateRandomPizza()
        {
            Instance._currentPizza = Instantiate(Instance.pizzaPrefab);
            float scale = 1 + Instance._level * 0.1f;
            Instance._currentPizza.Initialize(Instance._availableRecipes.Random(), scale, Instance.slotCount);
        }

        public static Sprite GetCurrentIngredientSprite() => Instance._currentPizza.CurrentIngredient.ingredientImage;

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
            Instance._availableRecipes = new List<Recipe> { Instance.startRecipe };
            Wallet.ResetWallet();
            CookManager.DeleteAllCooks();
            Instance._level++;
            Instance.slotCount++;
            DestroyPizza();
            GenerateRandomPizza();
        }

        IEnumerator DeceleratorUpgrade()
        {
            while (_upgradeFlag) {
                SetPizzaRotationSpeed(10f);
                yield return new WaitForSeconds(15);
                SetUpgradeFlag(false);
                SetPizzaRotationSpeed(25f);
            }
        }
    }
}