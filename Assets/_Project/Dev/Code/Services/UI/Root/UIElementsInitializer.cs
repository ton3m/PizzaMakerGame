using System;
using System.Collections.Generic;
using System.Linq;
using PizzaMaker.Code.Services.StateSwitching.Level;
using PizzaMaker.Code.Services.UI.Root.ProgressBar;
using PizzaMaker.Code.Services.UI.Root.StateMachine;
using PizzaMaker.Code.Services.UI.Root.StateMachine.Mediator;
using PizzaMaker.Code.Services.UI.Windows;
using PizzaMaker.Code.UI.Buttons;
using PizzaMaker.Code.Utils.DI;
using PizzaMaker.Code.Utils.Reactive;
using UnityEngine;

namespace PizzaMaker.Code.Services.UI.Root
{
    public class UIElementsInitializer : IDisposable
    {
        private readonly List<IDisposable> _disposables = new();
        
        private readonly Transform _root;

        public UIElementsInitializer(Transform root)
        {
            _root = root;
        }
        
        public UIElementsInitializer Init(DIContainer container)
        {
            InitBase(container.Resolve<IWindowsService>(), container.Resolve<IStateMachine<UIState>>().State);
            InitLevelProgressBars(container.Resolve<LevelData>());

            return this;
        }

        public void Dispose() =>
            _disposables.ForEach(disposable => disposable.Dispose());

        public void InitLevelProgressBars(LevelData data)
        {
            List<ProgressBar.ProgressBar> levelProgressBars =
                _root.GetComponentsInChildren<ProgressBar.ProgressBar>(true).ToList();

            new LevelProgressBarsPresenter(levelProgressBars, data.Level, data.Progress)
                .DisposeIn(_disposables);
        }

        public void InitBase(IWindowsService windowsService, Subject<UIState> uiState)
        {
            var stateMachineMediator = new UIStateMachineMediator(uiState.Notify);
            var windowsServiceMediator = new WindowsServiceMediator(windowsService);
            
            List<OpenWindowButton> openWindowButtons = _root.GetComponentsInChildren<OpenWindowButton>(true).ToList();
            List<StartGameButton> startGameButtons = _root.GetComponentsInChildren<StartGameButton>(true).ToList();
            List<NextLevelButton> nextLevelButtons = _root.GetComponentsInChildren<NextLevelButton>(true).ToList();

            startGameButtons.ForEach(button => button.Initialize(() =>
                stateMachineMediator.RequestEnter(button, UIState.Gameplay)));

            openWindowButtons.ForEach(button => button.Initialize((windowId) =>
                windowsServiceMediator.RequestOpen(button, windowId)));

            nextLevelButtons.ForEach(button => button.Initialize(() =>
                stateMachineMediator.RequestEnter(button, UIState.HideAll)));
        }
    }
}