using System;
using PizzaMaker.Code.Services.UI.Root.StateMachine.StatesProvider;
using PizzaMaker.Code.Utils.Reactive;

namespace PizzaMaker.Code.Services.UI.Root.StateMachine
{
    public class UIStateMachine : IStateMachine<UIState>
    {
        private readonly Reactive<UIState> _state = new();

        private readonly IStatesProvider<UIState> _states;

        public UIStateMachine(IStatesProvider<UIState> states)
        {
            _states = states;
        }

        public Subject<UIState> State => new(_state, Enter);

        public void Enter(UIState state)
        {
            if (state == UIState.None)
                throw new ArgumentException("State is None");

            if (_state.Value == state)
                throw new ArgumentException("State already entered");

            if (!_states.Contains(state))
                throw new ArgumentException("State not found");

            _states.Get(state).Enter();
            _state.Value = state;
        }
    }
}