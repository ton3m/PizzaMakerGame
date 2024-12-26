using System;
using System.Collections;
using PizzaMaker.Code.Configs;
using PizzaMaker.Code.Services.CoroutinePerformer;
using PizzaMaker.Code.Services.Loaders;
using PizzaMaker.Code.Services.Loaders.Scene;
using PizzaMaker.Code.Services.LoadingCurtain;
using PizzaMaker.Code.Services.Logging;
using PizzaMaker.Code.Services.Observers;
using PizzaMaker.Code.Services.StateSwitching.GameplayStates;
using PizzaMaker.Code.Services.StateSwitching.LevelSwitcher;
using PizzaMaker.Code.Services.UI;
using PizzaMaker.Code.UI.Layers;
using PizzaMaker.Code.UI.Root;
using PizzaMaker.Code.UI.Root.StateMachine;
using PizzaMaker.Code.Utils.DI;
using PizzaMaker.Code.Utils.Reactive;
using PizzaMaker.Code.Utils.Reactive.Disposing;
using Unity.VisualScripting;
using UnityEngine;
using Logger = PizzaMaker.Code.Services.Logging.Logger;

namespace PizzaMaker.Code.Entry.Bootstraps
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
            
            _container.Resolve<GameEndObserver>()
                .Result
                .EqualTo(GameResult.Won)
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
                    .EqualTo(GameplayStates.LevelSwitching)
                    .Subscribe(_ => coroutinePerformer.StartPerform(switcher.EnterNextLevel()))
                    .DisposeIn(_disposer);

                return switcher;
            });
        }

        private void SetupStateNotifier()
        {
            var notifier = _container.Resolve<GameplayStateNotifier>();
            
            var stateUi = _container.Resolve<UIStateMachine>().CurrentState;

            var startedUi = stateUi.EqualTo(UIState.Gameplay);
            var levelSwitchedUi = stateUi.EqualTo(UIState.GoToNextLevel);

            startedUi
                .Subscribe(_ => notifier.NotifyThatEntered(GameplayStates.LevelStarted))
                .DisposeIn(_disposer);

            levelSwitchedUi
                .Subscribe(_ => notifier.NotifyThatEntered(GameplayStates.LevelSwitching))
                .DisposeIn(_disposer);
            
            _container.Resolve<GameEndObserver>()
                .Result
                .NotEqualTo(GameResult.None)
                .Subscribe(_ => notifier.NotifyThatEntered(GameplayStates.LevelEnded))
                .DisposeIn(_disposer);
        }

        private void SetupUIStateMachine()
        {
            var stateMachine = _container.Resolve<UIStateMachine>();

            var state = _container.Resolve<GameplayStateNotifier>().State;

            state
                .EqualTo(GameplayStates.Loaded)
                .Subscribe(_ => stateMachine.Enter(UIState.Menu))
                .DisposeIn(_disposer);

            state
                .EqualTo(GameplayStates.LevelEnded)
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
            _container.RegisterAsSingle(c => new GameEndObserver());

        private void RegSceneObjectsLoader() =>
            _container.RegisterAsSingle(c => new SceneObjectLoader());

        private void RegUIHolder() => 
            _container.RegisterAsSingle(c => c.Resolve<SceneObjectLoader>().Load<UIHolder>());

        private void RegUIStateMachine()
        {
            _container.RegisterAsSingle(c =>
                new UIStateMachine(
                    c.Resolve<ILayersActivator>(),
                    c.Resolve<IWindowsService>(),
                    c.Resolve<ILoadingCurtain>(),
                    () => c.Resolve<GameEndObserver>().Result.Value));
        }

        private void RegUIElements()
        {
            _container.RegisterAsSingle<IWindowsService>(c =>
                new WindowsService(c.Resolve<UIHolder>().Windows));

            _container.RegisterAsSingle<ILayersActivator>(c =>
                new LayersActivator(c.Resolve<UIHolder>().Layers));

            _container.RegisterAsSingle(c =>
                new UIElementsInitializer().Init(c)).NonLazy();
        }
    }
}