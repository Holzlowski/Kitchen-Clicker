using System.Collections.Generic;
using Idle.Cook;
using PizzaGame;
using TMPro;
using UnityEngine;

namespace Singletons
{
    public class CookManager : Singleton<CookManager>
    {
        [SerializeField] private TMP_Text cookCountLabel;
        [SerializeField] private float tickSeconds = 3f;
        [SerializeField] private CookVisualisation cookPrefab;
        [SerializeField] private Transform[] cookPlaces;

        private float _previousTick;
        private List<Cook> _cooks = new List<Cook>();
        private List<CookVisualisation> _cookVisualisations = new List<CookVisualisation>();

        private void Start() => cookCountLabel.text = "0";

        private void Update()
        {
            // Check if tickSeconds have passed since last tick
            if (Time.time < _previousTick + tickSeconds)
                return;

            foreach (Cook cook in _cooks)
                cook.DoTick();

            _previousTick = Time.time;
        }

        public static void AddCook(float errorRate, float efficiency)
        {
            // Create new cook and add to list
            Cook cook = new Cook(errorRate, efficiency);
            Instance._cooks.Add(cook);
            // Update cook counter UI
            Instance.cookCountLabel.text = Instance._cooks.Count.ToString();

            if(Instance._cooks.Count > Instance.cookPlaces.Length) return;

            CookVisualisation visualisation = Instantiate(Instance.cookPrefab, Instance.cookPlaces[Instance._cooks.Count - 1].position, Quaternion.identity);
            cook.HitEvent += visualisation.showHit;
            cook.MissEvent += visualisation.missHit;
            cook.CompletedEvent += _ => visualisation.pizzaComplete();
            Instance._cookVisualisations.Add(visualisation);
        }

        public static void DeleteAllCooks()
        {       
            Instance._cooks = new List<Cook>();
            foreach (CookVisualisation visualisation in Instance._cookVisualisations)
            {
                Destroy(visualisation.gameObject);
            }
            Instance._cookVisualisations = new List<CookVisualisation>();
            Instance.cookCountLabel.text = "0";
        }
    }
}