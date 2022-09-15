using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CityApp.Core.ViewModels.Abstractions;
using CityApp.Infrastructure.NavigationManager;
using CityApp.Infrastructure.Storages;
using CityApp.Infrastructure.Storages.Constants;
using CityApp.Models.Models.Account;
using CityApp.Models.Models.Authorization;
using CityApp.Modules.Account.Accounts;
using CityApp.Modules.Account.Login;
using CityApp.Modules.Home;
using CityApp.Utilities.Logging;
using Xamarin.Forms;

namespace CityApp.Modules.Menu
{
    public class MenuViewModel : BaseViewModel
    {
		#region Fields

	    private string _userName;

	    private string _associatedCity;

	    private string _profileImage;

	    private ObservableCollection<MenuItem> _menuItems;

		#endregion

		#region Constructors

		public MenuViewModel(INavigationManager navigationManager) : base(navigationManager)
		{
			Title = "Menu";
			if (Device.RuntimePlatform == Device.iOS)
				Icon = "hamburger.png";
		}

		#endregion

		#region Properties

		public override ILogger Logger => LogManager.GetLog();

	    public string UserName
	    {
		    get => _userName;
		    set => SetProperty(ref _userName, value);
	    }

	    public string AssociatedCity
	    {
		    get => _associatedCity;
		    set => SetProperty(ref _associatedCity, value);
		}

	    public ObservableCollection<MenuItem> MenuItems
	    {
		    get => _menuItems;
		    set => SetProperty(ref _menuItems, value);
	    }

	    public string ProfileImage
	    {
		    get => _profileImage;
		    set => SetProperty(ref _profileImage, value);
		}

	    public MenuItem SelectedMenuItem
	    {
		    get => null;
		    set
		    {
			    if (value != null && value.IsEnabled && value.Command != null)
			    {
				    NavigationManager.CloseDrawerMenu();
				    value.Command.Execute(null);
			    }
		    }
	    }

		#endregion

		#region Commands

	    public ICommand GoHomeCommand => new Command(() => NavigationManager.NavigateToMenuItem<HomeViewModel>());

	    public ICommand GoToAccountsCommand => new Command(() => NavigationManager.NavigateToMenuItem<AccountsListViewModel>());

	    public ICommand LogoutCommand => new Command(Logout);

		#endregion

		#region Public Methods

		public override async Task Init(object args)
		{
			var userContext = SessionStorage.Instance.UserContext;
			var associationAccount = SessionStorage.Instance.Get<AccountAssociationModel>(StorageConstants.ACCOUNT_ASSOCIATION_KEY);

			UserName = userContext.FullName;
			AssociatedCity = associationAccount == null ? string.Empty : associationAccount.Name;
			ProfileImage = string.IsNullOrEmpty(userContext.ProfileImageUrl) ? "profile_icon_placeholder" : userContext.ProfileImageUrl;
			PopulateMenu();

		    await base.Init(args);
	    }

		#endregion

		#region Private Methods

	    private void PopulateMenu()
	    {
		    var startMenuItem = new MenuItem
		    {
			    Command = GoHomeCommand,
			    Title = "Home",
			    Icon = "home_menu_icon"
		    };

		    var tip = new MenuItem
		    {
			    Command = GoToAccountsCommand,
			    Title = "Accounts",
			    Icon = "accounts_menu_icon"
		    };

		    var logout = new MenuItem
		    {
			    Command = LogoutCommand,
			    Title = "Log out",
			    Icon = ""
		    };

		    MenuItems = new ObservableCollection<MenuItem>
		    {
			    startMenuItem,
			    tip,
			    logout,
		    };
	    }

	    private void Logout()
	    {
			SessionStorage.Instance.Clear();

			SettingStorage.Instance.Clear();

			NavigationManager.SetMainViewModel<LoginViewModel>();
	    }

		#endregion
	}
}
