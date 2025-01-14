using PizzaMaker.Code.Utils.DI;

namespace PizzaMaker.Code.Services.UI.Root.StateMachine.StatesProvider
{
    public class UIFactoryStatesProvider : IStatesProvider<UIState>
    {
        private UIStatesFactory _factory;
        private DIContainer _container;

        public UIFactoryStatesProvider(UIStatesFactory factory, DIContainer container)
        {
            _container = container;
            _factory = factory;
        }

        public bool Contains(UIState id) => true;

        public IState Get(UIState id) => _factory.Create(id, _container).Value;
    }
}