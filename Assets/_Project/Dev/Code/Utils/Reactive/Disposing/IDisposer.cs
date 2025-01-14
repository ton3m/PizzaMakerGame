using System;
using System.Collections.Generic;

namespace PizzaMaker.Code.Utils.Reactive.Disposing
{
    public interface IDisposer : IDisposable
    {
        IDisposer Add(IDisposable disposable);
        IDisposer AddRange(IEnumerable<IDisposable> disposables);
    }
}