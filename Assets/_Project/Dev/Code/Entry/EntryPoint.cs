using PizzaMaker.Code.Consts;
using PizzaMaker.Code.Entry.Bootstraps;
using PizzaMaker.Code.Entry.Bootstraps.Main;
using PizzaMaker.Code.Services.CoroutinePerformer;
using PizzaMaker.Code.Services.Loaders;
using PizzaMaker.Code.Services.Loaders.Scene;
using PizzaMaker.Code.Services.Loaders.Scene.PizzaMaker.Code.Services.Loaders.Scene;
using PizzaMaker.Code.Services.LoadingCurtain;
using PizzaMaker.Code.Utils.DI;
using PizzaMaker.Code.Utils.Logging;
using UnityEngine;

namespace PizzaMaker.Code.Entry
{
    public class EntryPoint : MonoBehaviour
    {
        private DIContainer _container;

        private void Awake()
        {
            SetupAppSettings();
            
           _container = new DIContainer();
            
            RegLogging();
            RegCoroutinePerformer();

            RegResourcesLoader();
            RegSceneLoader();
            RegLoadingCurtain();
            
            _container.Initialize();
            
            RunBootstrap();
        }

        private void RunBootstrap()
        {
            var bootstrap = new Bootstrap();
            var performer = _container.Resolve<ICoroutinePerformer>();
            
            performer.StartPerform(bootstrap.Run(_container));
        }

        private void SetupAppSettings()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }
        
        private void RegLogging() =>
            _container.RegisterAsSingle(c => new UnityLogThread()).NonLazy();

        private void RegResourcesLoader() => 
            _container.RegisterAsSingle(c => new ResourcesLoader());

        private void RegSceneLoader() =>
            _container.RegisterAsSingle<ISceneLoader>(c => new SceneLoader());

        private void RegLoadingCurtain()
        {
            _container.RegisterAsSingle<ILoadingCurtain>(c =>
            {
                var loader = _container.Resolve<ResourcesLoader>();
                LoadingCurtain prefab = loader.Load<LoadingCurtain>(ResourcesPaths.LoadingCurtain);
                return Instantiate(prefab);
            });
        }

        private void RegCoroutinePerformer()
        {
            _container.RegisterAsSingle<ICoroutinePerformer>(c =>
            {
                var loader = _container.Resolve<ResourcesLoader>();
                CoroutinePerformer prefab = loader.Load<CoroutinePerformer>(ResourcesPaths.CoroutinePerformer);
                return Instantiate(prefab);
            });
        }
    }
}