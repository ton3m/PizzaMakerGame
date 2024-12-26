using System;

namespace PizzaMaker.Code.Utils.Reactive
{
    public class ConditionalObserverWrap<T> : Main.Abstraction.IObserver<T>
    {
        private readonly Main.Abstraction.IObserver<T> _observer;
        private readonly Func<T, T, bool> _notifyCondition;
        
        public ConditionalObserverWrap(Main.Abstraction.IObserver<T> observer, Func<T, T, bool> notifyCondition)
        {
            _observer = observer;
            _notifyCondition = notifyCondition;
        }

        public void Notify(T previous, T current)
        {
            if (_notifyCondition(previous, current))
                _observer.Notify(previous, current);
        }
    }
}