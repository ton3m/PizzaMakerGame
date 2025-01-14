using System;

namespace PizzaMaker.Code.Utils.Reactive
{
    public class ConditionalObserverWrap<T> : IObserver<T>
    {
        private readonly IObserver<T> _observer;
        private readonly Func<T, bool> _notifyCondition;

        public ConditionalObserverWrap(IObserver<T> observer, Func<T, bool> notifyCondition)
        {
            _observer = observer;
            _notifyCondition = notifyCondition;
        }

        public void Notify(T value)
        {
            if (_notifyCondition(value))
                _observer.Notify(value);
        }
    }
}