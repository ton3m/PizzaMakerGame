using System;
using TMPro;
using UnityEngine;

namespace PizzaMaker.Code.Services
{
    public class MoneyService: MonoBehaviour
    {
        [SerializeField] private TMP_Text _moneyText;
        private int _money;
        private int Money
        {
            get => _money;
            set
            {
                _money = value;
                UpdateMoneyText();
            }
        }

        private void Start()
        {
            _moneyText = GetComponent<TMP_Text>();
            UpdateMoneyText();
        }

        private void UpdateMoneyText()
        {
            _moneyText.text = _money.ToString();
        }

        public void AddMoney(int money)
        {
            _money += money;
        }

        public bool HasEnoughMoney(int cost)
        {
            return _money >= cost;
        }

        public void TryRemoveMoney(int cost)
        {
            if (HasEnoughMoney(cost))
            {
                _money -= cost;
            }
        }
    }
}