using System;
using System.Collections.Generic;
using PizzaMaker.Code.Utils.Reactive.Disposing;

namespace PizzaMaker.Code.Utils.Reactive
{
    public static class ReactiveExtensions
    {
        public static IDisposable Subscribe(this IObservable observable, Action action) =>
            observable.Subscribe(new ActionObserver(action));

        public static IObservable ToEvent<T>(this IObservable<T> observable)
        {
            Func<IObserver, IDisposable> subscribe = obs => 
                observable.Subscribe(new ActionObserver<T>(obs.Notify));
            
            return new Observable(subscribe);
        }

        public static IDisposable Subscribe<T>(this IObservable<T> observable, Action action) =>
            observable.Subscribe(new ActionObserver<T>(action));

        public static IDisposable Subscribe<T>(this IObservable<T> observable, Action<T> action) =>
            observable.Subscribe(new ActionObserver<T>(action));

        public static IObservable<T> EqualTo<T>(this IObservable<T> observable, T value) =>
            observable.Where(currentValue => currentValue.Equals(value));

        public static IObservable<T> NotEqualTo<T>(this IObservable<T> observable, T value) => 
            observable.Where(currentValue => !currentValue.Equals(value));

        public static IObservable<T> Where<T>(this IObservable<T> observable, Func<T, bool> condition)
        {
            Func<IObserver<T>, IDisposable> subscribe = observer =>
                observable.Subscribe(new ConditionalObserverWrap<T>(observer, condition));

            IObservable<T> whereObservable = new Observable<T>(subscribe);
            
            return whereObservable;
        }

        public static T DisposeIn<T>(this T disposable, List<IDisposable> disposables)
            where T : IDisposable
        {
            disposables.Add(disposable);
            return disposable;
        }
        
        public static IDisposable DisposeIn(this IDisposable disposable, List<IDisposable> disposables)
        {
            disposables.Add(disposable);
            return disposable;
        }

        public static IDisposable DisposeIn(this IDisposable disposable, IDisposer disposer)
        {
            disposer.Add(disposable);
            return disposable;
        }       
        
        public static T DisposeIn<T>(this T disposable, IDisposer disposer) where T : IDisposable
        {
            disposer.Add(disposable);
            return disposable;
        }
    }
}