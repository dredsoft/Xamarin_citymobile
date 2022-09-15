using System.Threading.Tasks;
using System.Windows.Input;
using CityApp.Core.ViewModels.Abstractions;
using CityApp.Infrastructure.NavigationManager;
using CityApp.Infrastructure.Storages;
using CityApp.Infrastructure.Storages.Constants;
using CityApp.Models.Models.Authorization;
using CityApp.Modules.Account.Accounts;
using CityApp.Modules.Account.ForgotPassword;
using CityApp.Modules.Account.Register;
using CityApp.Modules.Printing;
using CityApp.Resources;
using CityApp.Services.Account;
using CityApp.Utilities.ActivityContext;
using CityApp.Utilities.Logging;
using CityApp.Utilities.Validation.Abstractions;
using CityApp.Utilities.Validation.Implementations;
using CityApp.Utilities.Validation.Rules;
using Xamarin.Forms;

namespace CityApp.Modules.Account.Login
{
    public class LoginViewModel : BaseViewModel
    {
        #region Fields

        private readonly IAccountService _accountService;

        private bool _rememberUser = true;

        #endregion

        #region Constructors

        public LoginViewModel(INavigationManager navigationManager, IAccountService accountService) : base(navigationManager)
        {
            _accountService = accountService;
        }

        #endregion

        #region Properties

        public ValidatableObject<string> UserName { get; set; }

        public ValidatableObject<string> Password { get; set; }

        public bool RememberUser
        {
            get => _rememberUser;
            set => SetProperty(ref _rememberUser, value);
        }

        public override ILogger Logger => LogManager.GetLog();

        #endregion

        #region Commands

        public ICommand LoginCommand => new Command(async () => await Login());

        public ICommand NavigateToSignUpPageCommand => new Command(() => NavigationManager.SetMainViewModel<RegisterViewModel>());

        public ICommand NavigateToForgotPasswordPageCommand => new Command(() => NavigationManager.SetMainViewModel<ForgotPasswordViewModel>());

        #endregion

        #region Public Methods

        public override async Task Init(object args)
        {
            await base.Init(args);

            UserName = new ValidatableObject<string>();
            Password = new ValidatableObject<string>();

            AddValidations();
        }

        #endregion

        #region Private Methods

        private async Task Login()
        {
            Logger.Trace();

            if (CheckIfDataValid(UserName) && CheckIfDataValid(Password))
            {
                using (ActivityContext.MakeContext(this))
                {
                    var authorizationModel = new AuthorizationModel
                    {
                        Email = UserName.Value.Trim(),
                        Password = Password.Value,
                    };

                    var result = await _accountService.GetTokenAsync(authorizationModel);

                    if (ValidateResponse(result))
                    {
                        if (RememberUser)
                        {
                            SettingStorage.Instance.AddOrUpdateValue(StorageConstants.USER_CONTEXT_KEY, result.Data);
                        }

                        SessionStorage.Instance.UserContext = result.Data;

                        NavigationManager.SetMainViewModel<AccountsListViewModel>();
                    }
                }
            }
        }

        private void AddValidations()
        {
            UserName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = AppResources.msgEmptyStringError });
            UserName.Validations.Add(new EmailRule<string> { ValidationMessage = AppResources.msgInvalidEmailError });
            Password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = AppResources.msgEmptyPassword });
        }

        #endregion
    }
}
