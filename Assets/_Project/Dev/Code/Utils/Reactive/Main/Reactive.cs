using System;

namespace PizzaMaker.Code.Utils.Reactive.Main
{
    public class Reactive<T> : Abstraction.IObservable<T>, IReadOnlyReactive<T>
    {
        private T _value;

        private readonly Func<T, T, bool> _valueChangedCondition;

        private readonly Subject<T> _subject = new();
        
        public Reactive(Func<T, T, bool> valueChangedCondition)
        {
            _valueChangedCondition = valueChangedCondition;
        }

        public Reactive() :
            this((previous, current) => current.Equals(previous) == false)
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
                    _subject.Notify(oldValue, _value);
            }
        }

        public IDisposable Subscribe(Abstraction.IObserver<T> observer) => _subject.Subscribe(observer);
    }
}