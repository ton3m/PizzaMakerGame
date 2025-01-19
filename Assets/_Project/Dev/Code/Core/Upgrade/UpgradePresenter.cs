using System;
using PizzaMaker.Code.Services;
using PizzaMaker.Code.Utils.Reactive;

namespace PizzaMaker.Code.Core.Upgrade
{
	public class UpgradePresenter : IDisposable
	{
		private IUpgradeble _model;
		private IUpgradeView  _view;
		private MoneyService _moneyService;

		private Event _levelUpgraded = new();

		public UpgradePresenter(IUpgradeble model, IUpgradeView view, MoneyService moneyService)
		{
			_model = model;
			_view = view;
			_moneyService = moneyService;

			_view.UpgradeButtonClicked += OnUpgradeButtonClicked;
			
			UpdateView();
		}

		public IObservable LevelUpgraded => _levelUpgraded;

		public void Dispose()
		{
			_view.UpgradeButtonClicked -= OnUpgradeButtonClicked;
		}

		public void OnUpgradeButtonClicked()
		{
			if (_moneyService.TryRemoveMoney(_model.UpgradeCost))
			{
				_model.Upgrade();
				 _levelUpgraded.Notify();
				 UpdateView();
			}
		}

		private void UpdateView()
		{
			_view.SetLevel(_model.Level);
			_view.SetMultiplier(_model.Multiplier);
			_view.SetUpgradeCost(_model.UpgradeCost);
		}
	}
}