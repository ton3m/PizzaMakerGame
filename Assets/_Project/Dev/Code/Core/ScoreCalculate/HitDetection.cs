using TMPro;
using UnityEngine;

namespace PizzaMaker
{
    public class HitDetection : MonoBehaviour
    { 
        [SerializeField] private RectTransform _track;
        [SerializeField] private TMP_Text _text;
        private RectTransform _indicator;
        
        private float _score;
        void Start()
        {
            _indicator = GetComponent<RectTransform>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                float normalizedPosition = _indicator.anchoredPosition.x / Mathf.Abs(_track.rect.width / 2);
                _score = 1 - Mathf.Abs(normalizedPosition);
                _text.text = _score.ToString();
            }
        }
    }
}
