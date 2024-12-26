using PizzaMaker.Code.Utils.Reactive.Main;
using PizzaMaker.Code.Utils.Reactive.Main.Abstraction;

namespace PizzaMaker.Code.Services.Observers
{
    public class GameEndObserver : ISingleObserver<GameResult>
    {
        private readonly Reactive<GameResult> _result = new();
        
        public IReadOnlyReactive<GameResult> Result => _result;

        public void Notify(GameResult value) => _result.Value = value;
    }
}