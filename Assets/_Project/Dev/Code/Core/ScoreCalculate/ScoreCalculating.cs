using System;
using System.Collections.Generic;
using PizzaMaker.Code.Core.Upgrade;
using System.Linq;
using UnityEngine;

namespace PizzaMaker.Code.Core.ScoreCalculate
{
    public class ScoreCalculating
    {
        private readonly Func<float> _normalizedPosition;
        public float TotalScore { get; private set; }
        private float _ingredientsCost;
        
        private float _timingTestScore;

        private IReadOnlyList<UpgradeModel> _upgradeModels = new List<UpgradeModel>();
        private float _sumMultiplier = 1;

        
        public ScoreCalculating (Func <float> normalizedPosition, IReadOnlyList<UpgradeModel> upgradeModels)
        {
            _normalizedPosition = normalizedPosition;
            _upgradeModels = upgradeModels;
        }

        public void TotalScoreCalculate()
        {
            TotalScore = _timingTestScore + _ingredientsCost;
        }

        public void OnLevelUpgraded()
        {
            _upgradeModels.ToList().ForEach(upgradeModel => _sumMultiplier += upgradeModel.Multiplier * upgradeModel.Level);
        }
        
        public void OnIndicatorStop()
        {
            float position = _normalizedPosition();
            TimingTestScoreCalculate(position);
        }

        public void TimingTestScoreCalculate(float normalizedPosition)
        {
            _timingTestScore = (1 - Mathf.Abs(normalizedPosition)) * 100 * _sumMultiplier;
            Debug.Log($"Timing test score: {_timingTestScore}");
        }
        
        public void IngredientsCostCalculate()
        {
            throw new System.NotImplementedException();
        }
        
    }
}