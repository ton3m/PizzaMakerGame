namespace PizzaMaker.Code.UI.Root.StateMachine
{
    public interface IUIStateMachineMediator
    {
        void RequestEnter(object sender, UIState state);
    }
}