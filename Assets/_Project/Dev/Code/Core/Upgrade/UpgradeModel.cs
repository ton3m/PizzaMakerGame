using System;
using UnityEngine;

namespace PizzaMaker.Code.Core.Upgrade
{
    public class UpgradeModel : IUpgradeble
    {
        private int _level = 1;
        private float _baseMultiplier = 1f;
        private float _baseUpgradeCost = 100f;
        
        private float _growthRateMultiplier = 1.1f;
        private float _growthRateUpgradeCost = 1.4f;
        
        public UpgradeModel(float baseMultiplier, float baseUpgradeCost, float growthRateMultiplier, float growthRateUpgradeCost)
        {
            this._baseMultiplier = baseMultiplier;
            this._baseUpgradeCost = baseUpgradeCost;
            this._growthRateMultiplier = growthRateMultiplier;
            this._growthRateUpgradeCost = growthRateUpgradeCost;
        }

        public int Level => _level;
        public float Multiplier => _baseMultiplier * Mathf.Pow(_growthRateMultiplier, _level - 1);
        public int UpgradeCost => Mathf.RoundToInt(_baseUpgradeCost * Mathf.Pow(_growthRateUpgradeCost, _level - 1));

        public void Upgrade()
        {
            _level++;
        }
    }
}