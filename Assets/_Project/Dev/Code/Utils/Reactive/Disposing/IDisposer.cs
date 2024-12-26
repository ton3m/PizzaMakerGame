using System;

namespace PizzaMaker.Code.Utils.Reactive.Disposing
{
    public interface IDisposer : IDisposable
    {
        void Add(IDisposable disposable);
    }
}