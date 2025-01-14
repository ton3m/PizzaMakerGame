using System;
using PizzaMaker.Code.Services.UI.Windows;
using UnityEngine;

namespace PizzaMaker.Code.UI.Buttons
{
    public class OpenWindowButton : OnClickedButton
    {
        [SerializeField] private WindowId _windowId;
        private Action<WindowId> _openWindow;

        public void Initialize(Action<WindowId> openWindow)
        {
            _openWindow = openWindow;
        }

        protected override void OnClicked() => _openWindow.Invoke(_windowId);
    }
}

