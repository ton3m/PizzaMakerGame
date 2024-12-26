using System;
using System.Collections.Generic;

namespace PizzaMaker.Code.Utils.Reactive.Disposing
{
    public class Disposer : IDisposable, IDisposer
    {
        private readonly List<IDisposable> _disposables = new();

        public void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }

        public void Add(IDisposable disposable) => _disposables.Add(disposable);
    }
}