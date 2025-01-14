using System;

namespace PizzaMaker.Code.Utils.Reactive
{
    public class Reactive<T> : IObservable<T>, IReadOnlyReactive<T>
    {
        private T _value;

        private readonly Func<T, T, bool> _valueChangedCondition;

        private readonly Subject<T> _subject = new();
        
        public Reactive(Func<T, T, bool> valueChangedCondition, T value = default)
        {
            _valueChangedCondition = valueChangedCondition;
            _value = value;
        }

        public Reactive(T value = default) :
            this((previous, current) => current.Equals(previous) == false, value)
        {
        }

        public T Value
        {
            get => _value;
            set
            {
                T oldValue = _value;

                _value = value;

                if (_valueChangedCondition(oldValue, _value))
                    _subject.Notify(_value);
            }
        }

        public IDisposable Subscribe(IObserver<T> observer) => _subject.Subscribe(observer);
    }
}