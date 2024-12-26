using PizzaMaker.Code.Services.Logging;

namespace PizzaMaker.Code.UI.Root.StateMachine
{
    public class UIStateMachineMediator : IUIStateMachineMediator
    {
        private readonly ILogger _logger = 
            new Logger(UnityLogThread.NewChild(nameof(UIStateMachineMediator)));
        
        private readonly UIStateMachine _stateMachine;

        public UIStateMachineMediator(UIStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public void RequestEnter(object sender, UIState state)
        {
            _logger.Log($"Object {sender.ToString()} requested enter state {state}");
            
            _stateMachine.Enter(state);
        }
    }
}