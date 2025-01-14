using System;

namespace PizzaMaker.Code.Services.UI.Root.StateMachine
{
    public class ActionState : IState
    {
        private readonly Action _action;

        public ActionState(Action action)
        {
            _action = action;
        }

        public void Enter() => _action();
    }
}