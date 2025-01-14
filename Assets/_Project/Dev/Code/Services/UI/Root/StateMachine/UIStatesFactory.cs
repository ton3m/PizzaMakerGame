using System;
using PizzaMaker.Code.Services.GameEndHadnling;
using PizzaMaker.Code.Services.StateSwitching.Level;
using PizzaMaker.Code.Services.UI.Layers;
using PizzaMaker.Code.Services.UI.Windows;
using PizzaMaker.Code.Utils.DI;
using PizzaMaker.Code.Utils.Reactive;

namespace PizzaMaker.Code.Services.UI.Root.StateMachine
{
    public class UIStatesFactory
    {
        private IWindowsService _windowsService;
        private ILayersActivator _layersActivator;
        private UIElementsInitializer _initializer;

        public IdUIState Create(UIState id, DIContainer from)
        {
            InitServices(from);
            IdUIState state;

            switch (id)
            {
                case UIState.Menu:
                    state = CreateMenu(from.Resolve<IStateMachine<UIState>>().State);
                    break;
                case UIState.Gameplay:
                    state = CreateGameplay(from.Resolve<LevelData>());
                    break;
                case UIState.HideAll:
                    state = CreateHideAll();
                    break;
                case UIState.Finish:
                    state = CreateFinish(() => from.Resolve<GameEndObserver>().Result.Value == GameEndResult.Won);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(id), id, null);
            }

            CleanupServices();
            return state;
        }

        private void InitServices(DIContainer container)
        {
            _layersActivator = container.Resolve<ILayersActivator>();
            _windowsService = container.Resolve<IWindowsService>();
            _initializer = container.Resolve<UIElementsInitializer>();
        }
        
        private void CleanupServices()
        {
            _windowsService = null;
            _layersActivator = null;
            _initializer = null;
        }

        private IdUIState CreateMenu(Subject<UIState> uiState)
        {
            _initializer.InitBase(_windowsService, uiState);
            var layersActivator = _layersActivator;
            
            IState state = new ActionState(() =>
            {
                layersActivator.DisableAll();
                layersActivator.Enable(LayerId.Menu);
            });
            return new IdUIState(UIState.Menu, state);
        }

        private IdUIState CreateGameplay(LevelData data)
        {
            _initializer.InitLevelProgressBars(data);
            var layersActivator = _layersActivator;
            
            IState state = new ActionState(() =>
            {
                layersActivator.DisableAll();
                layersActivator.Enable(LayerId.Game);
            });
            return new IdUIState(UIState.Gameplay, state);
        }

        private IdUIState CreateHideAll()
        {
            var layersActivator = _layersActivator;
            var windowsService = _windowsService;
            
            IState state = new ActionState(() =>
            {
                layersActivator.DisableAll();
                windowsService.CloseAll();
            });
            return new IdUIState(UIState.HideAll, state);
        }

        private IdUIState CreateFinish(Func<bool> isWin)
        {
            var layersActivator = _layersActivator;
            var windowsService = _windowsService;
            
            IState state = new ActionState(() =>
            {
                layersActivator.DisableAll();
                windowsService.Open(isWin() ? WindowId.Win : WindowId.Lose);
            });
            return new IdUIState(UIState.Finish, state);
        }
    }
}