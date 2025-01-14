using System;
using System.Collections.Generic;
using System.Linq;
using PizzaMaker.Code.Utils.Reactive.Disposing;

namespace PizzaMaker.Code.Utils.Reactive
{
    public class Subject<T> : IObservable<T>, IObserver<T>
    {
        private readonly IObserver<T> _observer;
        private readonly IObservable<T> _observable;
        
        public Subject(IObservable<T> observable, IObserver<T> observer)
        {
            _observable = observable;
            _observer = observer;
        }

        public Subject(IObservable<T> observable, Action<T> action)
        {
            _observable = observable;
            _observer = new ActionObserver<T>(action);
        }
        
        public Subject()
        {
            List<IObserver<T>> observers = new();

            Func<IObserver<T>, IDisposable> subscribe = observer =>
            {
                observers.Add(observer);
                return new DisposableAction(() => observers.Remove(observer));
            };

            Action<T> notify = value => observers.ForEach(observer => observer.Notify(value));
            
            _observable = new Observable<T>(subscribe);
            _observer = new ActionObserver<T>(notify);
        }

        public IDisposable Subscribe(IObserver<T> observer) => _observable.Subscribe(observer);

        public void Notify(T value) => _observer.Notify(value);
    }
    
    public class Event : IObservable, IObserver
    {
        private readonly List<IObserver> _observers = new();
        
        public IDisposable Subscribe(IObserver observer)
        {
            _observers.Add(observer);

            return new DisposableAction(() => _observers.Remove(observer));
        }

        public void Notify()
        {
            _observers.ToList().ForEach(observer => observer.Notify());
        }
    }
}