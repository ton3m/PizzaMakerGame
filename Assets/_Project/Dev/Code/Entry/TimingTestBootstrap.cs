using System.Collections;
using PizzaMaker.Code.Core.Upgrade;
using PizzaMaker.Code.Entry.Bootstraps.SceneBootstraps;
using PizzaMaker.Code.Utils.DI;

namespace PizzaMaker.Code.Entry
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
            _container.RegisterSingle(container => new MoneyService());
        }
        private void RegUpgradeMvpFactory()
        {
            _container.RegisterAsSingle(container => new UpgradeMvpFactory() );
        }
        
        private void RegScoreCalculate()
        {
            UpgradeMvpFactory upgradeMvpFactory = _container.Resolve<UpgradeMvpFactory>();
            
            upgradeMvpFactory.MoneyService = _container.Resolve<MoneyService>();
            
            UpgradeModel ovenUpgradeModel = upgradeMvpFactory.CreateOvenUpgradeModel(1f, 100f, 1.1f, 1.2f);
            UpgradeModel doughUpgradeModel = upgradeMvpFactory.CreateDoughUpgradeModel(1f, 120f, 1.2f, 1.4f);
            
            var timingTest = FindAnyObjectByType<TimingTestView>();
            _container.RegisterAsSingle(c => new ScoreCalculating(() => timingTest.NormalizedPosition, ovenUpgradeModel, doughUpgradeModel));
            
            var scoreCalculating = _container.Resolve<ScoreCalculating>();

            timingTest.IndicatorStoped += scoreCalculating.OnIndicatorStop;
        }

        private void RegLevelChange()
        {
            var upgradeMvpFactory = _container.Resolve<UpgradeMvpFactory>();
            upgradeMvpFactory.OvenUpgradePresenter.LevelUpgraded += () => _container.Resolve<ScoreCalculating>().OnLevelUpgraded();
            upgradeMvpFactory.DoughUpgradePresenter.LevelUpgraded += () => _container.Resolve<ScoreCalculating>().OnLevelUpgraded();
        }
    }
}