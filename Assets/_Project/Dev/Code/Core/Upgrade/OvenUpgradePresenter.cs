using System;

namespace PizzaMaker.Code.Core.Upgrade
{
    public class OvenUpgradePresenter :IDisposable
    {
        public event Action LevelUpgraded;
        
        private readonly IUpgradeble _model;
        private readonly IUpgradeView  _view;

        public OvenUpgradePresenter(IUpgradeble model, IUpgradeView view)
        {
            _model = model;
            _view = view;

            _view.UpgradeButtonClicked += OnUpgradeButtonClicked;
            
            UpdateView();
        }
        
        public void Dispose()
        {
            _view.UpgradeButtonClicked -= OnUpgradeButtonClicked;
        }
        
        public void OnUpgradeButtonClicked()
        {
            if (CanUpgrade())
            { 
                _model.Upgrade();
                LevelUpgraded?.Invoke();
                UpdateView();
            }
        }
        private bool CanUpgrade()
        {
            // TODO: add a check for the ability to purchase
            return true;
        }
        private void UpdateView()
        {
            _view.SetLevel(_model.Level);
            _view.SetMultiplier(_model.Multiplier);
            _view.SetUpgradeCost(_model.UpgradeCost);
        }

        
    }
}