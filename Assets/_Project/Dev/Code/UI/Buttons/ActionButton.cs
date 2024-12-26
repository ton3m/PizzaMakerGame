using System;

namespace PizzaMaker.Code.UI.Buttons
{
    public abstract class ActionButton : OnClickedButton
    {
        private Action _onClicked;

        public void Initialize(Action onClicked) => _onClicked = onClicked;

        protected override void OnClicked() => _onClicked.Invoke();
    }
}