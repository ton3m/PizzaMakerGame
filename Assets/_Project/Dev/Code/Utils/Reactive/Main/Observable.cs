using System;

namespace PizzaMaker.Code.Utils.Reactive.Main
{
    public class Observable<T> : Abstraction.IObservable<T>
    {
        private readonly Func<Abstraction.IObserver<T>, IDisposable> _subscribe;

        public Observable(Func<Abstraction.IObserver<T>, IDisposable> subscribe)
        {
            _subscribe = subscribe;
        }

        public IDisposable Subscribe(Abstraction.IObserver<T> observer) => _subscribe(observer);
    }
}