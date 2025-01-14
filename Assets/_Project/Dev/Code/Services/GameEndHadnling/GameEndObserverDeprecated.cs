using PizzaMaker.Code.Utils.Reactive;

namespace PizzaMaker.Code.Services.GameEndHadnling
{
    public class GameEndObserverDeprecated : ISingleObserver<GameEndResult>
    {
        private readonly Reactive<GameEndResult> _result = new();

        public IReadOnlyReactive<GameEndResult> Result => _result;

        public void Notify(GameEndResult value) => _result.Value = value;
    }
}