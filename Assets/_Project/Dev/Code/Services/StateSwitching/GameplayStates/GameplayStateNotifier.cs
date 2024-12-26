using PizzaMaker.Code.Services.Logging;
using PizzaMaker.Code.Utils.Reactive.Main;

namespace PizzaMaker.Code.Services.StateSwitching.GameplayStates
{
    public class GameplayStateNotifier
    {
        private readonly Reactive<GameplayStates> _state = new();

        public IReadOnlyReactive<GameplayStates> State => _state;

        private readonly ILogger _logger = new Logger(UnityLogThread.NewChild(nameof(GameplayStateNotifier)));
        
        public void NotifyThatEntered(GameplayStates state)
        {
            _logger.Log($"State entered: {state}");
            _state.Value = state;
        }
    }
}