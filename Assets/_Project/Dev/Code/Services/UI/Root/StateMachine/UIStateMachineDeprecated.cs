using System;
using PizzaMaker.Code.Services.GameEndHadnling;
using PizzaMaker.Code.Services.LoadingCurtain;
using PizzaMaker.Code.Services.UI.Layers;
using PizzaMaker.Code.Services.UI.Windows;
using PizzaMaker.Code.Utils.DI;
using PizzaMaker.Code.Utils.Reactive;

namespace PizzaMaker.Code.Services.UI.Root.StateMachine
{
    //State init and call
    //State change
    //State Factory

    public class UIStateMachineDeprecated
    {
        private readonly Reactive<UIState> _state = new();

        private readonly ILayersActivator _layersActivator;
        private readonly IWindowsService _windowsService;
        private readonly ILoadingCurtain _curtain;
        private DIContainer _container;

        public UIStateMachineDeprecated(DIContainer container)
        {
            _container = container;
            _curtain = container.Resolve<ILoadingCurtain>();
        }

        public Subject<UIState> State => new(_state, Enter);

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
                    EnterFinishState(_container.Resolve<GameEndObserver>().Result.Value == GameEndResult.Won);
                    break;
                case UIState.HideAll:
                    EnterGoToNextLevelState();
                    break;
            }

            _state.Value = state;
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

        private void EnterFinishState(bool isWin)
        {
            _layersActivator.DisableAll();

            _windowsService.Open(isWin ? WindowId.Win : WindowId.Lose);
        }
    }
}