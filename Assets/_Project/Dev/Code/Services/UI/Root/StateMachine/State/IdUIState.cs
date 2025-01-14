namespace PizzaMaker.Code.Services.UI.Root.StateMachine
{
    public struct IdUIState : IIdState<UIState>
    {
        public UIState Id { get; }
        public IState Value { get; }

        public IdUIState(UIState id, IState value)
        {
            Id = id;
            Value = value;
        }
    }

    public interface IIdState<T>
    {
        public T Id { get; }
        public IState Value { get; }
    }
}