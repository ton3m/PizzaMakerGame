using System;
using System.Globalization;
using PizzaMaker.Code.Utils.Reactive;
using TMPro;
using UnityEngine;

namespace PizzaMaker.Code.Core.Health
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _valueText;
        
        private IHealth _health;
        private IDisposable _disposable;
        
        public void Init(IHealth health)
        {
            _health = health;
        }

        private void Start()
        {
            if (_health == null)
                throw new NullReferenceException(nameof(_health));
                
            UpdateValue(0, _health.HP.Value);
            
            _disposable = _health.HP.Subscribe(UpdateValue);
        }

        private void OnDestroy() => _disposable?.Dispose();

        private void UpdateValue(float old, float current) =>
            _valueText.text = current.ToString(CultureInfo.InvariantCulture);
    }
}