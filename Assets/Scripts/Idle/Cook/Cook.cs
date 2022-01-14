using System;
using PizzaGame;
using Singletons;
using Util;
using Random = UnityEngine.Random;

namespace Idle.Cook
{
    public class Cook
    {
        private float _errorRate;
        private float _efficiency;

        #region Pizza Data

        private readonly int _slotCount;
        private Recipe _currentRecipe;
        private int _filledIngredientSlots;

        #endregion

        #region Events

        public delegate void IngredientEvent(IngredientType ingredient);

        public event IngredientEvent HitEvent;
        public event IngredientEvent MissEvent;

        public delegate void PizzaEvent(Recipe pizzaRecipe);

        public event PizzaEvent CompletedEvent;

        #endregion

        public Cook(float errorRate, float efficiency, int slotCount = 10)
        {
            _errorRate = errorRate;
            _efficiency = efficiency;
            _currentRecipe = KitchenManagement.GetAvailableRecipes().Random();
            _slotCount = slotCount;
        }

        public void DoTick()
        {
            PutIngredient();
            // Check if current pizza is finished
            if (_filledIngredientSlots != _slotCount)
                return;
            // Pizza is finished, generate new pizza and start over
            CompletePizza();
            GenerateNewPizza();
        }

        private void PutIngredient()
        {
            // Generate new ingredient for next try
            IngredientType ingredient = _currentRecipe.GetRandomIngredient();

            // Check if ingredient throw is successful
            bool success = Random.value >= _errorRate;
            if (success)
                IngredientHit(ingredient);
            else
                IngredientMiss(ingredient);
        }

        private void IngredientHit(IngredientType ingredient)
        {
            _filledIngredientSlots++;
            Wallet.AddMoney((int)Math.Ceiling(ingredient.Value * _efficiency));
            HitEvent?.Invoke(ingredient);
        }

        private void IngredientMiss(IngredientType ingredient) => MissEvent?.Invoke(ingredient);

        private void CompletePizza()
        {
            Wallet.AddMoney((int)Math.Ceiling(_currentRecipe.Bonus * _efficiency));
            CompletedEvent?.Invoke(_currentRecipe);
        }

        private void GenerateNewPizza()
        {
            _currentRecipe = KitchenManagement.GetAvailableRecipes().Random();
            _filledIngredientSlots = 0;
        }
    }
}