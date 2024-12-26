using System;

namespace PizzaMaker.Code.Utils.Reactive.Deprecated
{
    public class ReactiveVar<T> : IReadOnlyReactiveVar<T> where T : IEquatable<T>
    {
        public event Action<T, T> Changed;

        private T _value;

        public ReactiveVar() => _value = default(T);

        public ReactiveVar(T value) => _value = value;

        public T Value
        {
            get => _value;
            set
            {
                T oldValue = _value;

                _value = value;

                if(_value.Equals(oldValue) == false)
                    Changed?.Invoke(oldValue, value);
            }
        }
    }
}
