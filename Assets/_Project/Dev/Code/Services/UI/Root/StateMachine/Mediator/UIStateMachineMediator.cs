using System;
using PizzaMaker.Code.Utils.Logging;

namespace PizzaMaker.Code.Services.UI.Root.StateMachine.Mediator
{
    public class UIStateMachineMediator : IUIStateMachineMediator
    {
        private readonly ILogger _logger = 
            new Logger(UnityLogThread.NewChild(nameof(UIStateMachineMediator)));
        
        private Action<UIState> _enter;

        public UIStateMachineMediator(Action<UIState> enter)
        {
            _enter = enter;
        }
        
        public void RequestEnter(object sender, UIState state)
        {
            _logger.Log($"Object {sender} requested enter state {state}");
            
            _enter(state);
        }
    }
}