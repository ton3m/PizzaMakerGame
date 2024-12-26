using System;
using System.Collections.Generic;

namespace PizzaMaker.Code.Utils.Reactive.Deprecated
{
    public class ReactiveEnum<T> : IReadOnlyReactiveVar<T> where T : Enum
    {
        public event Action<T, T> Changed;

        private T _value;

        public ReactiveEnum() => _value = default(T);

        public ReactiveEnum(T value) => _value = value;

        public T Value
        {
            get => _value;
            set
            {
                T oldValue = _value;

                _value = value;

                if (!EqualityComparer<T>.Default.Equals(_value, oldValue))
                    Changed?.Invoke(oldValue, value);
            }
        }
    }
}