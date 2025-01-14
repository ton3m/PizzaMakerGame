using System;
using UnityEngine;

namespace PizzaMaker.Code.Core.ScoreCalculate
{
    public class TimingTestView : MonoBehaviour
    {
        public event Action IndicatorStoped;

        [SerializeField] private RectTransform _track;
        private RectTransform _indicator;
        
        [SerializeField] private float _indicatorSpeed = 200f;
        private bool _isMovingRight = true;
        public float NormalizedPosition { get; private set; }

        private float _trackWidth => _track.rect.width / 2;
        
        void Start()
        {
            _indicator = GetComponent<RectTransform>();
        }

        void Update()
        {
            MoveIndicator();
            IndicatorStop();
        }

        private void IndicatorStop()
        {
            if (Input.GetMouseButtonDown(0))
            {
                NormalizedPosition = _indicator.anchoredPosition.x / Mathf.Abs(_track.rect.width / 2);
                //_indicatorSpeed = 0;
                
                IndicatorStoped?.Invoke();
            }
        }

        private void MoveIndicator()
        {
            float movement = _indicatorSpeed * Time.deltaTime * (_isMovingRight ? 1 : -1);
            _indicator.anchoredPosition += new Vector2(movement, 0);

            if (_isMovingRight && _indicator.anchoredPosition.x >= _trackWidth)
            {
                _isMovingRight = false;
            }
            else if (!_isMovingRight && _indicator.anchoredPosition.x <= -_trackWidth)
            {
                _isMovingRight = true;
            }
        }
    }
}
