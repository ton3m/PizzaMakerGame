using System.Collections;
using PizzaMaker.Code.Configs;
using PizzaMaker.Code.Consts;
using PizzaMaker.Code.Services.Loaders;
using PizzaMaker.Code.Services.Loaders.Scene;
using PizzaMaker.Code.Services.LoadingCurtain;
using PizzaMaker.Code.Services.SceneLauncher;
using PizzaMaker.Code.Utils.DI;
using PizzaMaker.Code.Utils.Logging;
using ILogger = PizzaMaker.Code.Utils.Logging.ILogger;
using Logger = PizzaMaker.Code.Utils.Logging.Logger;

namespace PizzaMaker.Code.Entry.Bootstraps.Main
{
    public class Bootstrap
    {
        private readonly ILogger _logger = new Logger(UnityLogThread.NewChild(nameof(Bootstrap)));

        private DIContainer _container;

        public IEnumerator Run(DIContainer container)
        {
            OnLoadingStarted(container);

            _container = container;


            RegSceneSwitcher();
            RegGameLaunchConfig();
            RegGameConfig();

            _container.Initialize();

            OnLoadingFinished();

            yield return EnterNextState();
        }

        private void RegSceneSwitcher() =>
            _container.RegisterAsSingle(c => new SceneLauncher(c.Resolve<ISceneLoader>()));

        private void RegGameLaunchConfig()
        {
            _container.RegisterAsSingle(c =>
                c.Resolve<ResourcesLoader>()
                    .Load<GameLaunchConfig>(ResourcesPaths.GameLaunchConfig));
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

        private IEnumerator EnterNextState()
        {
            var sceneSwitcher = _container.Resolve<SceneLauncher>();
            var targetScene = _container.Resolve<GameLaunchConfig>().TargetScene;

            var subContainer = new DIContainer(_container);

            yield return sceneSwitcher.LaunchScene(targetScene, subContainer);
        }
        
        private void RegGameConfig() =>
            _container.RegisterAsSingle(c => new GameConfig());
    }
}