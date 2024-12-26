using PizzaMaker.Code.Utils.Reactive.Main.Abstraction;

namespace PizzaMaker.Code.Utils.Reactive.Main
{
    public interface IReadOnlyReactive<T> : IObservable<T>
    {
        T Value { get; }
    }
}