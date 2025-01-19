using System.Collections;
using System.Collections.Generic;
using PizzaMaker.Code.Core.ScoreCalculate;
using PizzaMaker.Code.Core.Upgrade;
using PizzaMaker.Code.Services;
using PizzaMaker.Code.Utils.DI;
using PizzaMaker.Code.Utils.Reactive;
using PizzaMaker.Code.Utils.Reactive.Disposing;

namespace PizzaMaker.Code.Entry.Bootstraps.Test
{
    public class TimingTestBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        
        public override IEnumerator Run(DIContainer container)
        {
            _container = container;
            
            RegMoneyService();
            RegUpgradeMvpFactory();
            RegScoreCalculate();
            RegLevelChange();
            
            _container.Initialize();
            
            yield return null;
        }

        private void RegMoneyService()
        {
            _container.RegisterAsSingle(container => new MoneyService());
        }
        private void RegUpgradeMvpFactory()
        {
            _container.RegisterAsSingle(container => new UpgradeMvpFactory());
        }
        
        private void RegScoreCalculate()
        {
            UpgradeMvpFactory upgradeMvpFactory = _container.Resolve<UpgradeMvpFactory>();
            
            upgradeMvpFactory.MoneyService = _container.Resolve<MoneyService>();
            
            List<UpgradeModel> upgradeModels = upgradeMvpFactory.CreateModels();
            
            var timingTest = FindAnyObjectByType<TimingTestView>();
            _container.RegisterAsSingle(c => new ScoreCalculating(() => timingTest.NormalizedPosition, upgradeModels));
            
            var scoreCalculating = _container.Resolve<ScoreCalculating>();

            timingTest.IndicatorStoped += scoreCalculating.OnIndicatorStop;
        }

        private void RegLevelChange()
        {
            var upgradeMvpFactory = _container.Resolve<UpgradeMvpFactory>();

            System.Action action = () => _container.Resolve<ScoreCalculating>().OnLevelUpgraded();

            upgradeMvpFactory.OvenUpgradePresenter.LevelUpgraded.Subscribe(action).DisposeIn(_container.Resolve<IDisposer>());;
            upgradeMvpFactory.DoughUpgradePresenter.LevelUpgraded.Subscribe(action).DisposeIn(_container.Resolve<IDisposer>());;
            upgradeMvpFactory.SousUpgradePresenter.LevelUpgraded.Subscribe(action).DisposeIn(_container.Resolve<IDisposer>());;
            

        }
    }
}