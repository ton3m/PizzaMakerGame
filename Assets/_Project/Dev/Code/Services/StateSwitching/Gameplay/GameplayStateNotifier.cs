using PizzaMaker.Code.Utils.Logging;
using PizzaMaker.Code.Utils.Reactive;

namespace PizzaMaker.Code.Services.StateSwitching.Gameplay
{
    public class GameplayStateNotifier
    {
        private readonly Reactive<GameplayState> _state = new();

        public IReadOnlyReactive<GameplayState> State => _state;

        private readonly ILogger _logger = new Logger(UnityLogThread.NewChild(nameof(GameplayStateNotifier)));
        
        public void NotifyThatEntered(GameplayState state)
        {
            _logger.Log($"State entered: {state}");
            _state.Value = state;
        }
    }
}