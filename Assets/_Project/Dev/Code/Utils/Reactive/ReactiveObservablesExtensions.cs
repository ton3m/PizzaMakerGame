using System;
using System.Collections.Generic;
using PizzaMaker.Code.Utils.Reactive.Disposing;
using PizzaMaker.Code.Utils.Reactive.Main;

namespace PizzaMaker.Code.Utils.Reactive
{
    public static class ReactiveObservablesExtensions
    {
        public static IDisposable Subscribe<T>(this Main.Abstraction.IObservable<T> observable, Action action) =>
            observable.Subscribe(new ActionObserver<T>(action));

        public static IDisposable Subscribe<T>(this Main.Abstraction.IObservable<T> observable, Action<T> action) =>
            observable.Subscribe(new ActionObserver<T>(action));

        public static IDisposable Subscribe<T>(this Main.Abstraction.IObservable<T> observable, Action<T, T> action) =>
            observable.Subscribe(new ActionObserver<T>(action));

        public static Main.Abstraction.IObservable<T> EqualTo<T>(this Main.Abstraction.IObservable<T> observable, T value) =>
            observable.Where(currentValue => currentValue.Equals(value));
        
        public static Main.Abstraction.IObservable<T> NotEqualTo<T>(this Main.Abstraction.IObservable<T> observable, T value) => 
            observable.Where(currentValue => !currentValue.Equals(value));

        public static Main.Abstraction.IObservable<T> Where<T>(this Main.Abstraction.IObservable<T> observable, Func<T, bool> condition)
        {
            Func<T, T, bool> conditionWrap = (_, current) => condition(current);

            Func<Main.Abstraction.IObserver<T>, IDisposable> subscribe = observer =>
                observable.Subscribe(new ConditionalObserverWrap<T>(observer, conditionWrap));

            Main.Abstraction.IObservable<T> whereObservable = new Observable<T>(subscribe);
            
            return whereObservable;
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