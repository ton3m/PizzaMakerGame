using System;
using UnityEngine;

namespace PizzaMaker.Code.Core.Upgrade
{
    public class DoughUpgradeModel : IUpgradeble
    {
        private int _level = 1;
        private const float BaseMultiplier = 1f;
        private const float BaseUpgradeCost = 100f;
        
        private const float GrowthRateMultiplier = 1.1f;
        private const float GrowthRateUpgradeCost = 1.4f;

        public int Level => _level;
        public float Multiplier => BaseMultiplier * Mathf.Pow(GrowthRateMultiplier, _level - 1);
        public int UpgradeCost => Mathf.RoundToInt(BaseUpgradeCost * Mathf.Pow(GrowthRateUpgradeCost, _level - 1));

        public void Upgrade()
        {
            _level++;
        }
    }
}