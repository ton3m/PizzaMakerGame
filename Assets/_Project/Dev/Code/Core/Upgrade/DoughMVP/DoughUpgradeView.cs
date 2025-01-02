using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace PizzaMaker.Code.Core.Upgrade
{
    public class DoughUpgradeView: MonoBehaviour, IUpgradeView
    {
        public event Action UpgradeButtonClicked;
        
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private TMP_Text _multiplierText;
        [SerializeField] private TMP_Text _costText;
        [SerializeField] private Button _upgradeButton;
        
        private void OnEnable()
        {
            _upgradeButton.onClick.AddListener(OnClicked);
        }

        private void OnDisable()
        {
            _upgradeButton.onClick.RemoveListener(OnClicked);
        }

        private void OnClicked()
        {
            UpgradeButtonClicked?.Invoke();
        }
        
        public void SetLevel(int level)
        {
            _levelText.text = $"Level: {level}";
        }

        public void SetMultiplier(float multiplier)
        {
            _multiplierText.text = $"Multiplier: x{multiplier:F2}";
        }

        public void SetUpgradeCost(int cost)
        {
            _costText.text = $"Cost: {cost}";
        }
    }
}