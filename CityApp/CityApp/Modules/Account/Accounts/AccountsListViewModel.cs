using System;
using System.Windows.Input;
using CityApp.Core.ViewModels.Implementations;
using CityApp.Infrastructure.NavigationManager;
using CityApp.Infrastructure.Storages;
using CityApp.Infrastructure.Storages.Constants;
using CityApp.Models.Models.Account;
using CityApp.Modules.Account.Login;
using CityApp.Modules.Home;
using CityApp.Modules.Menu;
using CityApp.Services.Account;
using CityApp.Utilities.ActivityContext;
using CityApp.Utilities.Logging;
using Xamarin.Forms;

namespace CityApp.Modules.Account.Accounts
{
    public class AccountsListViewModel : ListViewModel<AccountAssociationModel>
    {
		#region Fields

		private readonly IAccountService _accountService;

		#endregion

		#region Constructors

		public AccountsListViewModel(INavigationManager navigationManager, IAccountService accountService) : base(navigationManager)
		{
			Title = "Associated accounts";
			_accountService = accountService;
		}

		#endregion

		#region Properties

		public override ILogger Logger => LogManager.GetLog();

		public override Type DrawerMenuViewModelType => SessionStorage.Instance.Get<AccountAssociationModel>(StorageConstants.ACCOUNT_ASSOCIATION_KEY) != null ? typeof(MenuViewModel) : null;

		#endregion

		#region Commands

	    public ICommand LogOutCommand => new Command(LogOutExecute);

		#endregion

		#region Protected Methods

		protected override async void LoadItemsExecute()
		{
			using (ActivityContext.MakeContext(this))
			{
				var result = await _accountService.GetUserAccountsAsync();

				SetItemsListData(result);
			}
		}

	    protected override void SelectItemExecute(AccountAssociationModel accountAssociationModel)
	    {
		    Logger.Trace();
		    using (ActivityContext.MakeContext(this))
		    {
			    SettingStorage.Instance.AddOrUpdateValue(StorageConstants.ACCOUNT_ASSOCIATION_KEY, accountAssociationModel);
			    SessionStorage.Instance.Set(StorageConstants.ACCOUNT_ASSOCIATION_KEY, accountAssociationModel);
		    }

			NavigationManager.SetMainViewModel<HomeViewModel>();
		}

		#endregion

		#region Private Methods

	    private void LogOutExecute()
	    {
		    SessionStorage.Instance.Clear();

		    SettingStorage.Instance.Clear();

			NavigationManager.SetMainViewModel<LoginViewModel>();
		}

		#endregion
	}
}
