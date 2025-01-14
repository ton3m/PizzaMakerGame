using System;
using System.Collections;
using PathCreation;
using PizzaMaker.Code.Configs;
using PizzaMaker.Code.Consts;
using PizzaMaker.Code.Core;
using PizzaMaker.Code.Core.Character;
using PizzaMaker.Code.Core.Health;
using PizzaMaker.Code.Core.Tags;
using PizzaMaker.Code.Services.GameEndHadnling;
using PizzaMaker.Code.Services.Loaders;
using PizzaMaker.Code.Services.StateSwitching.Gameplay;
using PizzaMaker.Code.Utils.DI;
using PizzaMaker.Code.Utils.Reactive;
using PizzaMaker.Code.Utils.Reactive.Disposing;
using UnityEngine;

namespace PizzaMaker.Code.Entry.Bootstraps.Main
{
    public class LevelBootstrap : IDisposable
    {
        private DIContainer _container;

        private readonly IDisposer _disposer = new Disposer();

        public IEnumerator Run(DIContainer container)
        {
            _container = container;

            RegDisposer();
            RegCharacter();
            RegLinePassedObserver();
            RegPathCreator();

            SetupGameEndObserver();
            SetupCameraFollower();

            _container.Initialize();

            _container.Resolve<GameplayStateNotifier>()
                .NotifyThatEntered(GameplayState.Loaded);

            yield break;
        }

        public void Dispose() => _container.Dispose();

        private void SetupCameraFollower()
        {
            var loader = _container.Resolve<SceneObjectLoader>();

            var cameraFollower = loader.Load<CameraFollower>();

            Transform target = _container.Resolve<CharacterInstaller>().transform;

            cameraFollower.Init(target);
        }

        private void SetupGameEndObserver()
        {
            var observer = _container.Resolve<GameEndObserverDeprecated>();

            observer.Notify(GameEndResult.None);

            var isDead = _container.Resolve<CharacterInstaller>().Health.IsDead;
            var isFinishPassed = _container.Resolve<FinishLinePassedObserver>().IsPassed;

            var winObservable = isFinishPassed.EqualTo(true);
            var loseObservable = isDead.EqualTo(true);

            winObservable.Subscribe(() => observer.Notify(GameEndResult.Won)).DisposeIn(_disposer);
            loseObservable.Subscribe(() => observer.Notify(GameEndResult.Lost)).DisposeIn(_disposer);
        }

        private void RegPathCreator()
        {
            _container.RegisterAsSingle(c =>
                _container.Resolve<SceneObjectLoader>().Load<PathCreator>());
        }

        private void RegLinePassedObserver()
        {
            _container.RegisterAsSingle(c =>
            {
                var collisionDetector = c.Resolve<CharacterInstaller>().CollisionDetector;

                var observer = new FinishLinePassedObserver(collisionDetector);
                
                return observer.DisposeIn(_disposer);;
            });
        }

        private void RegCharacter()
        {
            _container.RegisterAsSingle(c =>
            {
                var hp = c.Resolve<GameConfig>().Health;
                var health = new Health(hp);

                var loader = _container.Resolve<SceneObjectLoader>();
                var spawnPoint = loader.Load<CharacterSpawnPoint>();

                var prefab = _container.Resolve<ResourcesLoader>()
                    .Load<CharacterInstaller>(ResourcesPaths.CharacterPrefab);

                var instance =
                    UnityEngine.Object.Instantiate(prefab, spawnPoint.transform.position, Quaternion.identity);

                instance.Init(health, c.Resolve<PathCreator>());
                instance.Disable();

                var state = _container.Resolve<GameplayStateNotifier>().State;

                state
                    .EqualTo(GameplayState.Started)
                    .Subscribe(_ => instance.Enable())
                    .DisposeIn(_disposer);

                state
                    .EqualTo(GameplayState.Ended)
                    .Subscribe(_ => instance.Disable())
                    .DisposeIn(_disposer);

                new DisposableAction(() =>
                        UnityEngine.Object.Destroy(instance.gameObject))
                    .DisposeIn(_disposer);

                return instance;
            });
        }

        private void RegDisposer() =>
            _container.RegisterAsSingle(c => _disposer).NonLazy();
    }
}