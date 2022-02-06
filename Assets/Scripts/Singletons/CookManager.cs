using System.Collections.Generic;
using System.Linq;
using Idle.Cook;
using TMPro;
using UnityEngine;

namespace Singletons
{
    public class CookManager : Singleton<CookManager>
    {
        [SerializeField] private TMP_Text cookCountLabel;
        [SerializeField] private float tickSeconds = 3f;
        [SerializeField] private Transform[] cookPlaces;

        private float _previousTick;
        private Dictionary<CookGenerator, List<Cook>> _cooks = new Dictionary<CookGenerator, List<Cook>>();
        private List<CookVisualisation> _cookVisualisations = new List<CookVisualisation>();

        private void Start() => cookCountLabel.text = "0";

        private void Update()
        {
            // Check if tickSeconds have passed since last tick
            if (Time.time < _previousTick + tickSeconds)
                return;

            foreach (var cook in _cooks.Values.SelectMany(cooks => cooks))
                cook.DoTick();

            _previousTick = Time.time;
        }

        public static int GetCookCountForType(CookGenerator type) =>
            Instance._cooks.TryGetValue(type, out List<Cook> cooks) ? cooks.Count : 0;

        public static void AddCook(CookGenerator generator, CookVisualisation cookPrefab)
        {
            // Create new cook and add to list
            Cook cook = new Cook(generator.ErrorRate, generator.Efficiency);
            if (!Instance._cooks.ContainsKey(generator))
                Instance._cooks.Add(generator, new List<Cook>());
            Instance._cooks[generator].Add(cook);

            // Update cook counter UI
            Instance.cookCountLabel.text = Instance._cooks.SelectMany(c => c.Value).Count().ToString();

            if (Instance._cooks.Count > Instance.cookPlaces.Length) return;

            CookVisualisation visualisation = Instantiate(cookPrefab,
                Instance.cookPlaces[Instance._cooks.Count - 1].position, Quaternion.identity);
            cook.HitEvent += visualisation.showHit;
            cook.MissEvent += visualisation.missHit;
            cook.CompletedEvent += _ => visualisation.pizzaComplete();
            Instance._cookVisualisations.Add(visualisation);
        }

        public static void DeleteAllCooks()
        {
            Instance._cooks = new Dictionary<CookGenerator, List<Cook>>();
            foreach (CookVisualisation visualisation in Instance._cookVisualisations)
                Destroy(visualisation.gameObject);
            Instance._cookVisualisations = new List<CookVisualisation>();
            Instance.cookCountLabel.text = "0";
        }
    }
}