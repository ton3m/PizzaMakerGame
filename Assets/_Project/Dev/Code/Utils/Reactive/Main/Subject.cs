using System;
using System.Collections.Generic;
using System.Linq;
using PizzaMaker.Code.Utils.Reactive.Disposing;

namespace PizzaMaker.Code.Utils.Reactive.Main
{
    public class Subject<T> : Abstraction.IObservable<T>, Abstraction.IObserver<T>
    {
        private readonly List<Abstraction.IObserver<T>> _observers = new();
        
        public IDisposable Subscribe(Abstraction.IObserver<T> observer)
        {
            _observers.Add(observer);

            return new DisposableAction(() => _observers.Remove(observer));
        }

        public void Notify(T previous, T current)
        {
            _observers.ToList().ForEach(observer => observer.Notify(previous, current));
        }
    }
}