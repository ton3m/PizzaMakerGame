using System.Collections.Generic;
using PizzaMaker.Code.Core.Upgrade.UpgradeViews;
using PizzaMaker.Code.Services; 
using Object = UnityEngine.Object;

namespace PizzaMaker.Code.Core.Upgrade
{
    public class UpgradeMvpFactory
    {
        public MoneyService MoneyService { get; set; }
        public UpgradePresenter OvenUpgradePresenter { get; private set; }
        public UpgradePresenter DoughUpgradePresenter { get; private set; }
        public UpgradePresenter SousUpgradePresenter { get; private set; }


        public List<UpgradeModel> CreateModels()
        {
            List<UpgradeModel> upgradeModels = new(){
                CreateOvenUpgradeModel(1f, 100f, 1.1f, 1.2f),
                CreateDoughUpgradeModel(1f, 120f, 1.2f, 1.4f),
                CreateSauceUpgradeModel(1f, 120f, 1.2f, 1.4f)
            };

            return upgradeModels;
        }

        private UpgradeModel CreateOvenUpgradeModel(float baseMultiplier, float baseUpgradeCost, float growthRateMultiplier, float growthRateUpgradeCost)
        {
            UpgradeModel ovenUpgradeModel = new UpgradeModel(baseMultiplier, baseUpgradeCost, growthRateMultiplier, growthRateUpgradeCost);
            OvenUpgradePresenter = CreateUpgradePresenter(ovenUpgradeModel);

            return ovenUpgradeModel;
        }

        private UpgradeModel CreateDoughUpgradeModel(float baseMultiplier, float baseUpgradeCost, float growthRateMultiplier, float growthRateUpgradeCost)
        {
            UpgradeModel doughUpgradeModel = new UpgradeModel(baseMultiplier, baseUpgradeCost, growthRateMultiplier, growthRateUpgradeCost);
            DoughUpgradePresenter = CreateUpgradePresenter(doughUpgradeModel);

            return doughUpgradeModel;
        }

        private UpgradeModel CreateSauceUpgradeModel(float baseMultiplier, float baseUpgradeCost, float growthRateMultiplier, float growthRateUpgradeCost)
        {
            UpgradeModel sauceUpgradeModel = new UpgradeModel(baseMultiplier, baseUpgradeCost, growthRateMultiplier, growthRateUpgradeCost);
            SousUpgradePresenter = CreateUpgradePresenter(sauceUpgradeModel);

            return sauceUpgradeModel;
        }

        private UpgradePresenter CreateUpgradePresenter(UpgradeModel ovenUpgradeModel)
        {
            UpgradeView ovenUpgradeView = Object.FindAnyObjectByType<OvenUpgradeView>();
            UpgradePresenter upgradePresenter = new UpgradePresenter(ovenUpgradeModel ,ovenUpgradeView, MoneyService);
            return upgradePresenter;
        }
        
    }
}