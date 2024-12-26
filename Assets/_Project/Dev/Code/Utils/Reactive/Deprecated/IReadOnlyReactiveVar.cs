using System;

namespace PizzaMaker.Code.Utils.Reactive.Deprecated
{
    public interface IReadOnlyReactiveVar<T>
    {
        event Action<T, T> Changed;

        T Value { get; }
    }
}
