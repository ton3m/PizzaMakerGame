using System;

namespace PizzaMaker.Code.Utils.Reactive
{
    public interface IObservable<T>
    {
        public IDisposable Subscribe(IObserver<T> observer);
    }
    
    public interface IObservable
    {
        public IDisposable Subscribe(IObserver observer);
    }
}