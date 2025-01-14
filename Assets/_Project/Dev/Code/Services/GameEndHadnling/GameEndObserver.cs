using System;
using PizzaMaker.Code.Utils.Reactive;
using PizzaMaker.Code.Utils.Reactive.Disposing;

namespace PizzaMaker.Code.Services.GameEndHadnling
{
    public class GameEndObserver : IDisposable
    {
        private readonly Reactive<GameEndResult> _result = new();

        private readonly Disposer _disposer = new();

        public GameEndObserver(
            Utils.Reactive.IObservable<bool> winCondition,
            Utils.Reactive.IObservable<bool> loseCondition)
        {
            winCondition.Subscribe(_ => _result.Value = GameEndResult.Won).DisposeIn(_disposer);
            loseCondition.Subscribe(_ => _result.Value = GameEndResult.Lost).DisposeIn(_disposer);
        }

        public IReadOnlyReactive<GameEndResult> Result => _result;

        public IObservable Won => _result.EqualTo(GameEndResult.Won).ToEvent();
        public IObservable Lost => _result.EqualTo(GameEndResult.Lost).ToEvent();
        
        public void Dispose() => _disposer.Dispose();
    }
}