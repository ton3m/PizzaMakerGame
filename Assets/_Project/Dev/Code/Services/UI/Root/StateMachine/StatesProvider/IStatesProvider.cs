namespace PizzaMaker.Code.Services.UI.Root.StateMachine.StatesProvider
{
    public interface IStatesProvider<TId>
    {
        public bool Contains(TId id);
        public IState Get(TId id);
    }
}