using System;
using PizzaMaker.Code.Services.LoadingCurtain;
using PizzaMaker.Code.Services.Observers;
using PizzaMaker.Code.Services.UI;
using PizzaMaker.Code.UI.Layers;
using PizzaMaker.Code.UI.Windows;
using PizzaMaker.Code.Utils.Reactive.Main;

namespace PizzaMaker.Code.UI.Root.StateMachine
{
    public class UIStateMachine
    {
        private readonly Reactive<UIState> _currentState = new();
        
        private readonly ILayersActivator _layersActivator;
        private readonly IWindowsService _windowsService;
        private readonly ILoadingCurtain _curtain;
        private readonly Func<GameResult> _getGameResult;

        public UIStateMachine(
            ILayersActivator layersActivator,
            IWindowsService windowsService,
            ILoadingCurtain curtain,
            Func<GameResult> getGameResult)
        {
            _layersActivator = layersActivator;
            _windowsService = windowsService;
            _curtain = curtain;
            _getGameResult = getGameResult;
        }

        public IReadOnlyReactive<UIState> CurrentState => _currentState;
        
        public void Enter(UIState state)
        {
            if (state == UIState.None)
                throw new ArgumentException("State is None");
            
            switch (state)
            {
                case UIState.Menu:
                    EnterMenuState();
                    break;
                case UIState.Gameplay:
                    EnterGameplayState();
                    break;
                case UIState.Finish:
                    EnterFinishState();
                    break;
                case UIState.GoToNextLevel:
                    EnterGoToNextLevelState();
                    break;
            }
         
            _currentState.Value = state;
        }

        private void EnterGoToNextLevelState()
        {
            _layersActivator.DisableAll();
            _windowsService.CloseAll();
        }

        private void EnterMenuState()
        {
            _layersActivator.DisableAll();
            _layersActivator.Enable(LayerId.Menu);
        }

        private void EnterGameplayState()
        {
            _layersActivator.DisableAll();
            _layersActivator.Enable(LayerId.Game);
        }

        private void EnterFinishState()
        {
            var gameResult = _getGameResult();
            
            if (gameResult == GameResult.None)
                throw new Exception("GameResult is None");

            bool isWin = gameResult == GameResult.Won;

            _layersActivator.DisableAll();

            _windowsService.Open(isWin ? WindowId.Win : WindowId.Lose);
        }
    }
}