using System;
using PizzaMaker.Code.Core.Character;
using PizzaMaker.Code.Services.GameEndHadnling;
using PizzaMaker.Code.Services.StateSwitching.Gameplay;
using PizzaMaker.Code.Services.UI.Root.StateMachine;
using PizzaMaker.Code.Utils.DI;
using PizzaMaker.Code.Utils.Reactive;
using PizzaMaker.Code.Utils.Reactive.Disposing;

namespace PizzaMaker.Code.Services.EventsHandling
{
    public class EventsHandleFactory
    {
        public IDisposable HandleAll(DIContainer container)
        {
            var disposer = new Disposer();
            var c = container;

            HandleGameLoaded(
                c.Initialized,
                c.Resolve<GameplayStateHolder>().State).DisposeIn(disposer);

            HandleGameAndUIStates(
                c.Resolve<IStateMachine<UIState>>().State,
                c.Resolve<GameplayStateHolder>().State).DisposeIn(disposer);

            HandleGameEnd(
                c.Resolve<GameEndObserver>().Result.ToEvent(),
                c.Resolve<GameplayStateHolder>().State).DisposeIn(disposer);

            HandleCharacterActivation(
                c.Resolve<Character>(),
                c.Resolve<GameplayStateHolder>().State).DisposeIn(disposer);
            
            return disposer;
        }

        public IDisposable HandleGameLoaded(IObservable loaded, Subject<GameplayState> gameplayState) =>
            loaded.Subscribe(() => gameplayState.Notify(GameplayState.Loaded));

        public IDisposable HandleGameAndUIStates(Subject<UIState> uiState, Subject<GameplayState> gameplayState)
        {
            var disposer = new Disposer();

            gameplayState.Loaded().Subscribe(() => uiState.Notify(UIState.Menu)).DisposeIn(disposer);
            gameplayState.Ended().Subscribe(() => uiState.Notify(UIState.Finish)).DisposeIn(disposer);

            uiState.Gameplay().Subscribe(() => gameplayState.Notify(GameplayState.Started)).DisposeIn(disposer);

            return disposer;
        }

        public IDisposable HandleGameEnd(IObservable gameEnded, Subject<GameplayState> gameplayState) =>
            gameEnded.Subscribe(() => gameplayState.Notify(GameplayState.Ended));

        public IDisposable HandleCharacterActivation(Character character,
            Utils.Reactive.IObservable<GameplayState> gameplayState)
        {
            var disposer = new Disposer();

            gameplayState.Started().Subscribe(() => character.Enabled = true).DisposeIn(disposer);
            gameplayState.Ended().Subscribe(() => character.Enabled = false).DisposeIn(disposer);

            return disposer;
        }
    }
}