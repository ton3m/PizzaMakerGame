using System;
using System.Collections.Generic;
using System.Linq;
using PizzaMaker.Code.Services.UI;
using PizzaMaker.Code.UI.Buttons;
using PizzaMaker.Code.UI.Root.ProgressBar;
using PizzaMaker.Code.UI.Root.StateMachine;
using PizzaMaker.Code.UI.Windows;
using PizzaMaker.Code.Utils.DI;
using PizzaMaker.Code.Utils.Reactive.Deprecated;

namespace PizzaMaker.Code.UI.Root
{
    public class UIElementsInitializer : IDisposable
    {
        private List<IDisposable> _disposables = new();

        private IUIStateMachineMediator _stateMachineMediator;
        private IWindowsServiceMediator _windowsServiceMediator;
        private UIHolder _holder;

        private int Level => 0;
        private IReadOnlyReactiveVar<float> LevelProgress { get; } = new ReactiveVar<float>(0);

        public UIElementsInitializer Init(DIContainer container)
        {
            _holder = container.Resolve<UIHolder>();
            
            var windowsService = container.Resolve<IWindowsService>();
            var stateMachine = container.Resolve<UIStateMachine>();
            
            _windowsServiceMediator = new WindowsServiceMediator(windowsService);
            _stateMachineMediator = new UIStateMachineMediator(stateMachine);
            
            InitElements();

            return this;
        }

        public void Dispose() =>
            _disposables.ForEach(disposable => disposable.Dispose());

        private void InitElements()
        {
            List<OpenWindowButton> openWindowButtons = _holder.GetComponentsInChildren<OpenWindowButton>(true).ToList();
            List<StartGameButton> startGameButtons = _holder.GetComponentsInChildren<StartGameButton>(true).ToList();

            List<ProgressBar.ProgressBar> levelProgressBars = _holder.GetComponentsInChildren<ProgressBar.ProgressBar>(true).ToList();
            List<NextLevelButton> nextLevelButtons = _holder.GetComponentsInChildren<NextLevelButton>(true).ToList();
            
            LevelProgressBarsPresenter levelProgressBarsPresenter = new(
                levelProgressBars, Level, LevelProgress);
            
            _disposables.Add(levelProgressBarsPresenter);
            
            nextLevelButtons.ForEach(button => button.Initialize(() =>
                _stateMachineMediator.RequestEnter(button, UIState.GoToNextLevel)));

            startGameButtons.ForEach(button => button.Initialize(() =>
                _stateMachineMediator.RequestEnter(button, UIState.Gameplay)));

            openWindowButtons.ForEach(button => button.Initialize((windowId) =>
                _windowsServiceMediator.RequestOpen(button, windowId)));
        }
    }
}