using System.Collections.Generic;
using Idle.Cook;
using TMPro;
using UnityEngine;

namespace Singletons
{
    public class CookManager : Singleton<CookManager>
    {
        [SerializeField] private TMP_Text cookCountLabel;
        [SerializeField] private float tickSeconds = 3f;

        private float _previousTick;
        private readonly List<Cook> _cooks = new List<Cook>();

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
        }
    }
}