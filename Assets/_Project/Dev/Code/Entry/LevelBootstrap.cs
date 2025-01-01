using System;
using System.Collections;
using System.Collections.Generic;
using PizzaMaker.Code.Core;
using PizzaMaker.Code.Core.Ingredients;
using PizzaMaker.Code.Core.Ingredients.Abstraction;
using PizzaMaker.Code.Core.Upgrade;
using PizzaMaker.Code.Services.GameFinishDetector;
using PizzaMaker.Code.Services.Scene;
using PizzaMaker.Code.Services.UI;
using PizzaMaker.Code.UI.Layers;
using PizzaMaker.Code.UI;
using PizzaMaker.Code.UI.Windows;
using UnityEngine;

namespace PizzaMaker.Code.Entry
{
    public class LevelBootstrap : BaseLevelBootstrap
    {
        private DIContainer _container;

        private readonly List<IUpdateable> _updatables = new();

        private CharacterMovement _characterMovement;
        private IGameUIStatesEvents _uiEvents;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        private static void LaunchFromBootstrap()
        {
            var sceneLoader = new SceneLoader();
            sceneLoader.Load(SceneId.Bootstrap);
        }

        public override IEnumerator Run(DIContainer container, object args)
        {
            Debug.Log("Level bootstrap started.");

            _container = container;

            IFinishLine finishLine = FindAnyObjectByType<FinishLine>();

            _characterMovement = FindAnyObjectByType<CharacterMovement>();
            Debug.Log(_characterMovement);
            CharacterCollisionHandler collision = FindAnyObjectByType<CharacterCollisionHandler>();
            InventoryView view = FindAnyObjectByType<InventoryView>();

            Dictionary<WindowId, GameObject> windows = FindAnyObjectByType<WindowsHolder>()?.Windows;
            Dictionary<LayerId, GameObject[]> layers = FindAnyObjectByType<LayersHolder>()?.Layers;
            UIRoot uiRoot = FindAnyObjectByType<UIRoot>();

            _container.RegisterAsSingle<IWindowsService>(c => new WindowsService(windows));
            _container.RegisterAsSingle<ILayersActivator>(c => new LayersActivator(layers));
            _container.RegisterAsSingle<IHealth>(c => new Health(100));
            
            RegFinishDetector(finishLine);
            InitIngredientCollecting(view, collision);

            uiRoot.Init(_container.Resolve<IGameFinishDetector>(),
                _container.Resolve<IWindowsService>(),
                _container.Resolve<ILayersActivator>(),
                () => _characterMovement.PathProgress);
            
            _uiEvents = uiRoot.Events;

            uiRoot.Enable();
            
            _uiEvents.GameplayStateEntered += OnGameplayStarted;
            _uiEvents.FinishStateEntered += OnGameplayEnded;

            RegScoreCalculate();
            RegLevelChange();
            
            Debug.Log("Level bootstrap finished.");

            yield break;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                _container.Resolve<IHealth>().ApplyDamage(500);
            }
            foreach (var updatable in _updatables)
            {
                updatable.Update();
            }
        }

        private void OnDestroy()
        {
            if(_uiEvents == null) return;
            
            _uiEvents.GameplayStateEntered -= OnGameplayStarted;
            _uiEvents.FinishStateEntered -= OnGameplayEnded;
        }

        private void OnGameplayStarted() => _characterMovement.Enable();

        private void OnGameplayEnded() => _characterMovement.Disable();

        private static void InitIngredientCollecting(InventoryView view, CharacterCollisionHandler collision)
        {
            IInventory inventory = new Inventory();
            InventoryPresenter presenter = new(inventory, view);
            ViewsPull pull = new();
            IIngredientsCollector collector = new IngredientsCollector(inventory, pull);

            view.Init(pull);
            collision.Init(collector);
        }

        private void RegFinishDetector(IFinishLine finishLine)
        {
            _container.RegisterAsSingle<IGameFinishDetector>(c =>
            {
                var detector = new GameFinishDetector(
                    () => finishLine.IsReached,
                    () => c.Resolve<IHealth>().Value <= 0);

                _updatables.Add(detector);

                return detector;
            });
        }

        private void RegScoreCalculate()
        {
            var timingTest = FindAnyObjectByType<TimingTestView>();
            _container.RegisterAsSingle(c => new ScoreCalculating(() => timingTest.NormalizedPosition));
            
            var scoreCalculating = _container.Resolve<ScoreCalculating>();
            

            timingTest.IndicatorStoped += scoreCalculating.OnIndicatorStop;
        }

        private void RegLevelChange()
        {
            var ovenModel = CreateOvenUpgrade();


            ovenModel.LevelUpgraded += () => _container.Resolve<ScoreCalculating>().OnLevelUpgraded();
        }

        private OvenUpgradePresenter CreateOvenUpgrade()
        {
            var ovenModel = new OvenUpgradeModel();
            var ovenView = FindAnyObjectByType<OvenUpgradeView>();
            var ovenPresenter = new OvenUpgradePresenter(ovenModel, ovenView);
            return ovenPresenter;
        }
    }
}