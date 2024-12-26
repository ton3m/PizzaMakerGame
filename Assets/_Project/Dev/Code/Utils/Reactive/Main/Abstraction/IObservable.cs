using System;

namespace PizzaMaker.Code.Utils.Reactive.Main.Abstraction
{
    public interface IObservable<T>
    {
        public IDisposable Subscribe(IObserver<T> observer);
    }
}