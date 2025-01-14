using PizzaMaker.Code.Services.StateSwitching.Gameplay;
using PizzaMaker.Code.Services.UI.Root.StateMachine;
using PizzaMaker.Code.Utils.Reactive;

namespace PizzaMaker.Code.Services.EventsHandling
{
    public static class EventsExtensions
    {
        public static IObservable Loaded(this IObservable<GameplayState> gameplayState) =>
            gameplayState.EqualTo(GameplayState.Loaded).ToEvent();

        public static IObservable Started(this IObservable<GameplayState> gameplayState) =>
            gameplayState.EqualTo(GameplayState.Started).ToEvent();
        
        public static IObservable Ended(this IObservable<GameplayState> gameplayState) =>
            gameplayState.EqualTo(GameplayState.Ended).ToEvent();

        public static IObservable Gameplay(this IObservable<UIState> gameplayState) =>
            gameplayState.EqualTo(UIState.Gameplay).ToEvent();
    }
}