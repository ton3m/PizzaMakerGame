using System;
using PizzaMaker.Code.Core.Collision;
using PizzaMaker.Code.Core.Tags;
using PizzaMaker.Code.Utils.Reactive;

namespace PizzaMaker.Code.Services.GameEndHadnling
{
    public class FinishLinePassedObserver : IDisposable
    {
        private readonly Reactive<bool> _isPassed = new();
        private readonly IDisposable _disposable;

        public FinishLinePassedObserver(ICollisionDetector detector)
        {
            var entered = detector.TriggerEntered;

            _disposable = entered
                .Where(collider => collider.gameObject.GetComponent<FinishLine>() != null)
                .Subscribe(() => _isPassed.Value = true);
        }

        public void Dispose() => _disposable.Dispose();

        public IReadOnlyReactive<bool> IsPassed => _isPassed;
    }
}