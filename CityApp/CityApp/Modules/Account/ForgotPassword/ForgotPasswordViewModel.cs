using System.Threading.Tasks;
using System.Windows.Input;
using CityApp.Core.ViewModels.Abstractions;
using CityApp.Infrastructure.NavigationManager;
using CityApp.Models.Models;
using CityApp.Models.Models.Account;
using CityApp.Modules.Account.Login;
using CityApp.Resources;
using CityApp.Services.Account;
using CityApp.Utilities.ActivityContext;
using CityApp.Utilities.Logging;
using CityApp.Utilities.UserDialogs;
using CityApp.Utilities.UserDialogs.Components.Alert;
using CityApp.Utilities.Validation.Abstractions;
using CityApp.Utilities.Validation.Implementations;
using CityApp.Utilities.Validation.Rules;
using Xamarin.Forms;

namespace CityApp.Modules.Account.ForgotPassword
{
    public class ForgotPasswordViewModel : BaseViewModel
    {
		#region Fields

		private readonly IAccountService _accountService;

		#endregion

		#region Constructors

		public ForgotPasswordViewModel(INavigationManager navigationManager, IAccountService accountService) : base(navigationManager)
		{
			_accountService = accountService;
		}

		#endregion

		#region Properties

		public override ILogger Logger => LogManager.GetLog();

	    public ValidatableObject<string> Email { get; set; }

		#endregion

		#region Commands

		public ICommand NavigateToLoginPageCommand => new Command(() => NavigationManager.SetMainViewModel<LoginViewModel>());

	    public ICommand ResetPasswordCommand => new Command(RestPassword);

		#endregion

		#region Public Methods

		public override async Task Init(object args)
		{
			await base.Init(args);

			Email = new ValidatableObject<string>();

			AddValidations();
		}

		#endregion

		#region Private Methods

		private async void RestPassword()
		{
			if (CheckIfDataValid(Email))
			{
				using (ActivityContext.MakeContext(this))
				{
					var result = await _accountService.ResetPasswordAsync(new ResetPasswordRequestModel { Email = Email.Value });

					ValidateResponse(result);
				}
			}
		}

        private void AddValidations()
        {
            Email.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = AppResources.msgEmptyStringError });

            Email.Validations.Add(new EmailRule<string> { ValidationMessage = AppResources.msgShouldBeEmailAddress });
        }

        #endregion
    }
}
