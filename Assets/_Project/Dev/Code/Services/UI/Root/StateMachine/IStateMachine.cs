using PizzaMaker.Code.Utils.Reactive;

namespace PizzaMaker.Code.Services.UI.Root.StateMachine
{
    public interface IStateMachine<T>
    {
        public Subject<T> State { get; }

        void Enter(T state);
    }
}