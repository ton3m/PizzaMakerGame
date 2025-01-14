namespace PizzaMaker.Code.Services.UI.Root.StateMachine.Mediator
{
    public interface IUIStateMachineMediator
    {
        void RequestEnter(object sender, UIState state);
    }
}