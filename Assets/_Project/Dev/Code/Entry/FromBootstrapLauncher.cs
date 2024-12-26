using PizzaMaker.Code.Configs;
using PizzaMaker.Code.Consts;
using PizzaMaker.Code.Services.Loaders;
using PizzaMaker.Code.Services.Loaders.Scene;
using UnityEngine;

namespace PizzaMaker.Code.Entry
{
    public class FromBootstrapLauncher
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        private static void Launch()
        {
            var resourceLoader = new ResourcesLoader();
            var config = resourceLoader.LoadResource<GameLaunchConfig>(ResourcesPaths.GameLaunchConfig);

            if (config.StartFromBootstrapScene)
            {
                var sceneLoader = new SceneLoader();

                sceneLoader.Load(SceneId.Empty);
                sceneLoader.Load(SceneId.Bootstrap);
            }
        }
    }
}