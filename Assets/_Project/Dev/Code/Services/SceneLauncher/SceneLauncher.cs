using System;
using System.Collections;
using PizzaMaker.Code.Entry.Bootstraps;
using PizzaMaker.Code.Entry.Bootstraps.Main;
using PizzaMaker.Code.Services.Loaders.Scene;
using PizzaMaker.Code.Utils.DI;
using PizzaMaker.Code.Utils.Logging;

namespace PizzaMaker.Code.Services.SceneLauncher
{
    public class SceneLauncher
    {
        private readonly ISceneLoader _sceneLoader;

        public SceneLauncher(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public IEnumerator LaunchScene(string sceneName, DIContainer container)
        {
            switch (sceneName)
            {
                case var _ when sceneName == SceneId.Bootstrap.ToString():
                    throw new Exception($"Bootstrap scene can't be loaded from {typeof(SceneLauncher)}");

                case var _ when sceneName == SceneId.Gameplay.ToString():
                    yield return LaunchGameplayScene(container);
                    break;
                
                default:
                    yield return LaunchDefaultScene(sceneName, container);
                    break;
            }
        }

        private IEnumerator LaunchGameplayScene(DIContainer container)
        {
            _sceneLoader.Load(SceneId.Gameplay);

            var bootstrap = new GameplayBootstrap();

            yield return bootstrap.Run(container);
        }

        private IEnumerator LaunchDefaultScene(string name, DIContainer container)
        {
            yield return _sceneLoader.LoadAsync(name);
            
            var sceneBootstrap = UnityEngine.Object.FindAnyObjectByType<SceneBootstrap>();

            if (sceneBootstrap == null)
            {
                UnityLogThread.Instance.Write($"Scene bootstrap for {name} not found", LogType.Warning);
                yield break;
            }
            
            yield return sceneBootstrap.Run(container);
        }
    }
}