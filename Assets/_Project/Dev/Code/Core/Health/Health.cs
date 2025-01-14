using System;
using PizzaMaker.Code.Utils.Reactive;
using UnityEngine;

namespace PizzaMaker.Code.Core.Health
{
    public class Health : IHealth
    {
        private readonly float _maxValue;
        
        private readonly Reactive<float> _health = new();
        private readonly Reactive<bool> _isDead = new();

        public Health(float maxValue)
        {
            _maxValue = maxValue;
            _health.Value = maxValue;
        }

        public Health(float maxValue, float value)
        {
            _maxValue = maxValue;

            if (value > _maxValue)
                throw new ArgumentException("Current health cannot be greater than max health");

            _health.Value = value;
        }

        public float MaxValue => _maxValue;
        
        public IReadOnlyReactive<bool> IsDead => _isDead;
        public IReadOnlyReactive<float> HP => _health;

        private float Value
        {
            get => _health.Value;
            set
            {
                if (_isDead.Value) return;

                _health.Value = Mathf.Clamp(value, 0, _maxValue);

                if (Value <= 0)
                    _isDead.Value = true;
            }
        }

        public void ApplyDamage(float amount)
        {
            if (amount < 0)
                throw new ArgumentException("Damage value cannot be negative");

            if (IsDead.Value || amount == 0) return;

            Value -= amount;
        }

        public void Heal(int amount)
        {
            if (amount < 0)
                throw new ArgumentException("Heal amount cannot be negative");

            if (IsDead.Value || amount == 0) return;

            Value += amount;
        }
    }
}