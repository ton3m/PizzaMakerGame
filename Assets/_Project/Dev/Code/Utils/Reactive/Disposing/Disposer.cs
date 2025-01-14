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

        public IDisposer Add(IDisposable disposable)
        {
            _disposables.Add(disposable);
            return this;
        }

        public IDisposer AddRange(IEnumerable<IDisposable> disposables)
        {
            _disposables.AddRange(disposables);
            return this;
        }
    }
}