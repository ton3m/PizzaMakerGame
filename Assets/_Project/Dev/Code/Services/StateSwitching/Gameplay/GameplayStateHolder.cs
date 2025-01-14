using PizzaMaker.Code.Utils.Reactive;

namespace PizzaMaker.Code.Services.StateSwitching.Gameplay
{
    public class GameplayStateHolder
    {
        public Subject<GameplayState> State => new(_state, Set);
        
        private readonly Reactive<GameplayState> _state = new();

        private void Set(GameplayState state) => _state.Value = state;
    }
}