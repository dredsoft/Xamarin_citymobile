using System.Threading.Tasks;
using CityApp.Core.ViewModels.Implementations;
using CityApp.Infrastructure.NavigationManager;
using CityApp.Infrastructure.Storages;
using CityApp.Infrastructure.Storages.Constants;
using CityApp.Models.Models.Account;
using CityApp.Modules.Home;
using CityApp.Services.Account;
using CityApp.Utilities.ActivityContext;
using CityApp.Utilities.Logging;

namespace CityApp.Modules.Account.ChooseAccount
{
    public class ChooseAccountViewModel : ListViewModel<AccountModel>
    {
		#region Fields

	    private readonly IAccountService _accountService;

		#endregion

		#region Constructors

		public ChooseAccountViewModel(INavigationManager navigationManager, IAccountService accountService) : base(navigationManager)
	    {
		    _accountService = accountService;
	    }

		#endregion

		#region Properties

	    public override ILogger Logger => LogManager.GetLog();

		#endregion

		#region Public Methods

		public override async Task Init(object args)
		{
			await base.Init(args);

			Title = "Select a City";
		}

	    protected override async void SelectItemExecute(AccountModel accountModel)
	    {
		    Logger.Trace();
		    using (ActivityContext.MakeContext(this))
		    {
			    var accountAssociation = await _accountService.ChooseAccountAsync(new AccountAssociationRequestModel
			    {
				    AccountId = accountModel.AccountId,
				    UserId = SessionStorage.Instance.UserContext.Id
			    });

			    if (ValidateResponse(accountAssociation))
			    {
				    SettingStorage.Instance.AddOrUpdateValue(StorageConstants.ACCOUNT_ASSOCIATION_KEY, accountAssociation.Data);
				    SessionStorage.Instance.Set(StorageConstants.ACCOUNT_ASSOCIATION_KEY, accountAssociation.Data);
			    }
		    }

		    NavigationManager.SetMainViewModel<HomeViewModel>();
		}

		#endregion

		#region Protected Methods

		protected override async void LoadItemsExecute()
	    {
		    using (ActivityContext.MakeContext(this))
		    {
				var result = await _accountService.GetAllAccountsAsync();

				SetItemsListData(result);
		    }
	    }

	    #endregion

	}
}
