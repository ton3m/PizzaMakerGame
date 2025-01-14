namespace PizzaMaker.Code.Utils.Reactive
{
    public interface IReadOnlyReactive<T> : IObservable<T>
    {
        T Value { get; }
    }
}