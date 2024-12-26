using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PizzaMaker.Code.UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public abstract class OnClickedButton : MonoBehaviour
    {
        private UnityAction _onClicked;
    
        private Button _button;

        private void Awake()
        {
            _onClicked = OnClicked;
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            if (_onClicked == null)
                throw new NullReferenceException(nameof(_onClicked));
        
            _button.onClick.AddListener(_onClicked);
        }
        
        private void OnDestroy() => _button.onClick.RemoveAllListeners();
        
        protected abstract void OnClicked();
    }
}