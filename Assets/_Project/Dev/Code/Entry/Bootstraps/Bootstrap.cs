using System.Collections;
using PizzaMaker.Code.Configs;
using PizzaMaker.Code.Services.Loaders.Scene;
using PizzaMaker.Code.Services.LoadingCurtain;
using PizzaMaker.Code.Services.Logging;
using PizzaMaker.Code.Utils.DI;
using ILogger = PizzaMaker.Code.Services.Logging.ILogger;
using Logger = PizzaMaker.Code.Services.Logging.Logger;

namespace PizzaMaker.Code.Entry.Bootstraps
{
    public class Bootstrap
    {
        private readonly ILogger _logger = new Logger(UnityLogThread.NewChild(nameof(Bootstrap)));

        private DIContainer _container;

        public IEnumerator Run(DIContainer container)
        {
            OnLoadingStarted(container);

            _container = container;
            
            RegSceneLoader();
            RegGameConfig();

            _container.Initialize();

            OnLoadingFinished();
            
            yield return RunGameplayBootstrap();
        }

        private void OnLoadingStarted(DIContainer container)
        {
            _logger.Log("Loading started");

            container.Resolve<ILoadingCurtain>().Show();
        }

        private void OnLoadingFinished()
        {
            _logger.Log("Loading finished.");

            _container.Resolve<ILoadingCurtain>().Hide();
        }

        private IEnumerator RunGameplayBootstrap()
        {
            var bootstrap = new GameplayBootstrap();
            var container = new DIContainer(_container);
            
            yield return bootstrap.Run(container);
        }
        
        private void RegGameConfig() =>
            _container.RegisterAsSingle(c => new GameConfig());

        private void RegSceneLoader() =>
            _container.RegisterAsSingle<ISceneLoader>(c => new SceneLoader());
    }
}