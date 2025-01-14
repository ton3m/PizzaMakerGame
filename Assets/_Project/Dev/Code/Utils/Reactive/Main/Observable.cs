using System;

namespace PizzaMaker.Code.Utils.Reactive
{
    public class Observable<T> : IObservable<T>
    {
        private readonly Func<IObserver<T>, IDisposable> _subscribe;

        public Observable(Func<IObserver<T>, IDisposable> subscribe)
        {
            _subscribe = subscribe;
        }

        public IDisposable Subscribe(IObserver<T> observer) => _subscribe(observer);
    }
    
    public class Observable : IObservable
    {
        private readonly Func<IObserver, IDisposable> _subscribe;

        public Observable(Func<IObserver, IDisposable> subscribe)
        {
            _subscribe = subscribe;
        }

        public IDisposable Subscribe(IObserver observer) => _subscribe(observer);
    }
}