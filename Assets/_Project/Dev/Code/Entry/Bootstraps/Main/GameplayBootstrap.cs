using System;
using System.Collections;
using PizzaMaker.Code.Configs;
using PizzaMaker.Code.Services.CoroutinePerformer;
using PizzaMaker.Code.Services.GameEndHadnling;
using PizzaMaker.Code.Services.Loaders;
using PizzaMaker.Code.Services.Loaders.Scene;
using PizzaMaker.Code.Services.LoadingCurtain;
using PizzaMaker.Code.Services.StateSwitching.Gameplay;
using PizzaMaker.Code.Services.StateSwitching.Level;
using PizzaMaker.Code.Services.UI.Layers;
using PizzaMaker.Code.Services.UI.Root;
using PizzaMaker.Code.Services.UI.Root.StateMachine;
using PizzaMaker.Code.Services.UI.Windows;
using PizzaMaker.Code.Utils.DI;
using PizzaMaker.Code.Utils.Logging;
using PizzaMaker.Code.Utils.Reactive;
using PizzaMaker.Code.Utils.Reactive.Disposing;
using Unity.VisualScripting;
using UnityEngine;
using Logger = PizzaMaker.Code.Utils.Logging.Logger;

namespace PizzaMaker.Code.Entry.Bootstraps.Main
{
    public class GameplayBootstrap : IDisposable
    {
        private DIContainer _container;

        private readonly Logger _logger = new(UnityLogThread.NewChild(nameof(GameplayBootstrap)));

        private readonly IDisposer _disposer = new Disposer();

        public IEnumerator Run(DIContainer container)
        {
            _container = container;

            OnLoadingStarted();

            yield return LoadGameplayScene();
            
            RegSceneObjectsLoader();
            RegUIHolder();
            
            RegLevelSwitcher();

            RegDisposer();
            RegStateNotifier();
            RegGameEndObserver();

            RegUIStateMachine();
            RegUIElements();

            SetupStateNotifier();
            SetupUIStateMachine();
            SetupLevelPassing();

            _container.Initialize();
            
            yield return LoadLevel();
            
            OnLoadingFinished();
        }

        private void SetupLevelPassing()
        {
            Action pass = () => _container.Resolve<GameConfig>().OnLevelPassed();
            
            _container.Resolve<GameEndObserverDeprecated>()
                .Result
                .EqualTo(GameEndResult.Won)
                .Subscribe(pass)
                .DisposeIn(_disposer);
        }

        public void Dispose() => _container?.Dispose();

        private void OnLoadingStarted()
        {
            _logger.Log("Loading started.");

            _container.Resolve<ILoadingCurtain>().Show();
        }

        private void OnLoadingFinished()
        {
            _container.Resolve<ICoroutinePerformer>().StartPerform(Update());

            _container.Resolve<ILoadingCurtain>().Hide();

            _logger.Log("Loading finished.");
        }

        private IEnumerator LoadGameplayScene() => 
            _container.Resolve<ISceneLoader>().LoadAsync(SceneId.Gameplay);

        private IEnumerator Update()
        {
            while (true)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                    yield return _container.Resolve<LevelSwitcher>().ExitCurrentLevel();

                yield return new WaitForNextFrameUnit();
            }
        }

        private void RegLevelSwitcher()
        {
            _container.RegisterAsSingle(c =>
            {
                var switcher = new LevelSwitcher(_container);

                var notifier = _container.Resolve<GameplayStateNotifier>();
                var coroutinePerformer = _container.Resolve<ICoroutinePerformer>();

                notifier.State
                    .EqualTo(GameplayState.LevelSwitching)
                    .Subscribe(_ => coroutinePerformer.StartPerform(switcher.EnterNextLevel()))
                    .DisposeIn(_disposer);

                return switcher;
            });
        }

        private void SetupStateNotifier()
        {
            var notifier = _container.Resolve<GameplayStateNotifier>();
            
            var stateUi = _container.Resolve<UIStateMachineDeprecated>().State;

            var startedUi = stateUi.EqualTo(UIState.Gameplay);
            var levelSwitchedUi = stateUi.EqualTo(UIState.HideAll);

            startedUi
                .Subscribe(_ => notifier.NotifyThatEntered(GameplayState.Started))
                .DisposeIn(_disposer);

            levelSwitchedUi
                .Subscribe(_ => notifier.NotifyThatEntered(GameplayState.LevelSwitching))
                .DisposeIn(_disposer);
            
            _container.Resolve<GameEndObserverDeprecated>()
                .Result
                .NotEqualTo(GameEndResult.None)
                .Subscribe(_ => notifier.NotifyThatEntered(GameplayState.Ended))
                .DisposeIn(_disposer);
        }

        private void SetupUIStateMachine()
        {
            var stateMachine = _container.Resolve<UIStateMachineDeprecated>();

            var state = _container.Resolve<GameplayStateNotifier>().State;

            state
                .EqualTo(GameplayState.Loaded)
                .Subscribe(_ => stateMachine.Enter(UIState.Menu))
                .DisposeIn(_disposer);

            state
                .EqualTo(GameplayState.Ended)
                .Subscribe(_ => stateMachine.Enter(UIState.Finish))
                .DisposeIn(_disposer);
        }

        private IEnumerator LoadLevel()
        {
            var levelSwitcher = _container.Resolve<LevelSwitcher>();
            var coroutinePerformer = _container.Resolve<ICoroutinePerformer>();
            
            yield return coroutinePerformer.StartPerform(levelSwitcher.EnterNextLevel());
        }

        private void RegDisposer() =>
            _container.RegisterAsSingle(c => _disposer).NonLazy();

        private void RegStateNotifier() =>
            _container.RegisterAsSingle(c => new GameplayStateNotifier());

        private void RegGameEndObserver() => 
            _container.RegisterAsSingle(c => new GameEndObserverDeprecated());

        private void RegSceneObjectsLoader() =>
            _container.RegisterAsSingle(c => new SceneObjectLoader());

        private void RegUIHolder() => 
            _container.RegisterAsSingle(c => c.Resolve<SceneObjectLoader>().Load<UIRoot>());

        private void RegUIStateMachine() =>
            _container.RegisterAsSingle(c =>
                new UIStateMachineDeprecated(c));

        private void RegUIElements()
        {
            _container.RegisterAsSingle<IWindowsService>(c =>
                new WindowsService(c.Resolve<UIRoot>().Windows));

            _container.RegisterAsSingle<ILayersActivator>(c =>
                new LayersActivator(c.Resolve<UIRoot>().Layers));

            _container.RegisterAsSingle(c =>
                new UIElementsInitializer(c.Resolve<UIRoot>().transform).Init(c)).NonLazy();
        }
    }
}