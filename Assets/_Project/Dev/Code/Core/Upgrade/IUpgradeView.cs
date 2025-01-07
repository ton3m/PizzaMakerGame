using System;

namespace PizzaMaker.Code.Core.Upgrade
{
    public interface IUpgradeView
    {
        event Action UpgradeButtonClicked;
        void SetLevel(int level);
        void SetMultiplier(float multiplier);
        void SetUpgradeCost(int cost);
    }
}