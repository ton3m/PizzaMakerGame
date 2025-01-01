using System;
using PizzaMaker.Code.Core.Upgrade;
using UnityEngine;

namespace PizzaMaker
{
    public class ScoreCalculating
    {
        private  OvenUpgradeModel _ovenUpgradeModel;
        
        private readonly Func<float> _normalizedPosition;
        public float TotalScore { get; private set; }
        private float _ingredientsCost;
        
        private float _timingTestScore;
        private float _ovenMultiplier = 1;
        public ScoreCalculating (Func <float> normalizedPosition)
        {
            _normalizedPosition = normalizedPosition;
            
        }
        
        // TODO: hui
        public ScoreCalculating (OvenUpgradeModel ovenUpgradeModel)
         {
             _ovenUpgradeModel = ovenUpgradeModel;
        }
       
        public void TotalScoreCalculate()
        {
            TotalScore = _timingTestScore + _ingredientsCost;
        }

        public void OnLevelUpgraded()
        {
            _ovenMultiplier = _ovenUpgradeModel.Multiplier;
        }
        
        public void OnIndicatorStop()
        {
            float position = _normalizedPosition();
            TimingTestScoreCalculate(position);
        }

        public void TimingTestScoreCalculate(float normalizedPosition)
        {
            _timingTestScore = (1 - Mathf.Abs(normalizedPosition)) * 100* _ovenMultiplier;
            Debug.Log($"Timing test score: {_timingTestScore}");
        }
        
        public void IngredientsCostCalculate()
        {
            throw new System.NotImplementedException();
        }
        
    }
}