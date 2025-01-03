using System;
using Object = UnityEngine.Object;

namespace PizzaMaker.Code.Core.Upgrade
{
    public class UpgradeMvpFactory
    {
        public UpgradePresenter OvenUpgradePresenter { get; private set; }
        public UpgradePresenter DoughUpgradePresenter { get; private set; }
        public UpgradeModel CreateOvenUpgradeModel(float baseMultiplier, float baseUpgradeCost, float growthRateMultiplier, float growthRateUpgradeCost)
        {
            UpgradeModel ovenUpgradeModel = new UpgradeModel( baseMultiplier, baseUpgradeCost, growthRateMultiplier, growthRateUpgradeCost);
            OvenUpgradePresenter = CreateUpgradePresenter(ovenUpgradeModel);
            
            return ovenUpgradeModel;
        }
        public UpgradeModel CreateDoughUpgradeModel(float baseMultiplier, float baseUpgradeCost, float growthRateMultiplier, float growthRateUpgradeCost)
        {
            UpgradeModel doughUpgradeModel = new UpgradeModel( baseMultiplier, baseUpgradeCost, growthRateMultiplier, growthRateUpgradeCost);
            DoughUpgradePresenter = CreateUpgradePresenter(doughUpgradeModel);
            
            return doughUpgradeModel;
        }
        
        private UpgradePresenter CreateUpgradePresenter(UpgradeModel ovenUpgradeModel)
        {
            UpgradeView ovenUpgradeView = Object.FindAnyObjectByType<OvenUpgradeView>();
            UpgradePresenter upgradePresenter = new UpgradePresenter(ovenUpgradeModel ,ovenUpgradeView);
            return upgradePresenter;
        }
        
        
    }
}