using PizzaMaker.Code.Consts;
using PizzaMaker.Code.Services.Loaders;
using PizzaMaker.Code.Services.UI;
using PizzaMaker.Code.Services.UI.Layers;
using PizzaMaker.Code.Services.UI.Root;
using PizzaMaker.Code.Services.UI.Root.StateMachine;
using PizzaMaker.Code.Services.UI.Root.StateMachine.StatesProvider;
using PizzaMaker.Code.Services.UI.Windows;
using PizzaMaker.Code.Utils.DI;

namespace PizzaMaker.Code.Services.Factories
{
    public class UIFactory
    {
        private readonly DIContainer _container;

        public UIFactory(DIContainer container)
        {
            _container = container;
        }

        public UIRoot CreateUIRoot()
        {
            var prefab = _container.Resolve<ResourcesLoader>().Load<UIRoot>(ResourcesPaths.UIRoot);

            return UnityEngine.Object.Instantiate(prefab);
        }

        public IStateMachine<UIState> CreateUIStateMachine()
        {
            var c = new DIContainer(_container);
            
            c.RegisterAsSingle(_ => new UIFactory(c).CreateUIRoot());
            c.RegisterAsSingle(_ => new UIElementsInitializer(c.Resolve<UIRoot>().transform));

            c.RegisterAsSingle<ILayersActivator>(_ => new LayersActivator(c.Resolve<UIRoot>().Layers));
            c.RegisterAsSingle<IWindowsService>(_ => new WindowsService(c.Resolve<UIRoot>().Windows));

            var provider = new UIFactoryStatesProvider(new UIStatesFactory(), c);

            return new UIStateMachine(provider);
        }
    }
}