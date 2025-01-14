using System;

namespace PizzaMaker.Code.Utils.Reactive
{
    public class ActionObserver<T> : IObserver<T>
    {
        private readonly Action<T> _action;

        public ActionObserver(Action<T> action)
        {
            _action = action;
        }

        public ActionObserver(Action action)
        {
            _action = _ => action();
        }

        public void Notify(T value) => _action(value);
    }
    
    public class ActionObserver : IObserver
    {
        private readonly Action _action;

        public ActionObserver(Action action)
        {
            _action = action;
        }

        public void Notify() => _action();
    }
}