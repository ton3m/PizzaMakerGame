using System;

namespace PizzaMaker.Code.Utils.Reactive
{
    public class ActionObserver<T> : Main.Abstraction.IObserver<T>
    {
        private readonly Action<T, T> _action;

        public ActionObserver(Action<T, T> action)
        {
            _action = action;
        }

        public ActionObserver(Action<T> action)
        {
            _action = (_, value) => action(value);
        }

        public ActionObserver(Action action)
        {
            _action = (_, _) => action();
        }

        public void Notify(T previous, T current) =>
            _action(previous, current);
    }
}