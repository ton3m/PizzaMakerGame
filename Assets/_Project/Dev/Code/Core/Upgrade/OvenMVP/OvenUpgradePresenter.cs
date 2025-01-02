using System;

namespace PizzaMaker.Code.Core.Upgrade
{
    public class OvenUpgradePresenter : UpgradePresenter
    {
        public override event Action LevelUpgraded;

        public OvenUpgradePresenter(IUpgradeble model, IUpgradeView view) : base(model, view)
        {
        }
    }
}