using System;

namespace PizzaMaker.Code.Utils.Common
{
    public class UpdatableAction : IUpdatable
    {
        private readonly Action _action;

        public UpdatableAction(Action action)
        {
            _action = action;
        }

        public void Update() => _action();
    }
}