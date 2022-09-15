using System.Threading.Tasks;
using System.Windows.Input;
using CityApp.Core.ViewModels.Abstractions;
using CityApp.Infrastructure.NavigationManager;
using CityApp.Models.Models.Account;
using CityApp.Modules.Account.ChooseAccount;
using CityApp.Modules.Account.Login;
using CityApp.Resources;
using CityApp.Services.Account;
using CityApp.Utilities.Logging;
using CityApp.Utilities.Validation.Abstractions;
using CityApp.Utilities.Validation.Implementations;
using CityApp.Utilities.Validation.Rules;
using Xamarin.Forms;

namespace CityApp.Modules.Account.Register
{
	public class RegisterViewModel : BaseViewModel
    {
		#region Fields

		private readonly IAccountService _accountService;

		#endregion

		#region Constructors

		public RegisterViewModel(INavigationManager navigationManager, IAccountService accountService) : base(navigationManager)
		{
			_accountService = accountService;
		}

		#endregion

		#region Properties

	    public override ILogger Logger => LogManager.GetLog();

	    public ValidatableObject<string> UserName { get; set; }

	    public ValidatableObject<string> Password { get; set; }

	    public ValidatableObject<string> PasswordConfirmation { get; set; }

		#endregion

		#region Commands

		public ICommand NavigateToLoginPageCommand => new Command(() => NavigationManager.SetMainViewModel<LoginViewModel>());

	    public ICommand RegisterCommand => new Command(RegisteredUser);

		#endregion

		#region Public Methods

		public override async Task Init(object args)
		{
			await base.Init(args);

			UserName = new ValidatableObject<string>();
			Password = new ValidatableObject<string>();
			PasswordConfirmation = new ValidatableObject<string>();

			AddValidations();
		}

		#endregion

		#region Private Methods

		private async void RegisteredUser()
		{
			Logger.Trace();

			if(CheckIfDataValid(UserName) && CheckIfDataValid(Password) && CheckIfDataValid(PasswordConfirmation))
			{
				var authorizationModel = new RegistrationModel()
				{
					Email = UserName.Value.Trim(),
					Password = Password.Value,
				};

				var result = await _accountService.RegisterAsync(authorizationModel);

				if (ValidateResponse(result))
				{
					NavigationManager.SetMainViewModel<ChooseAccountViewModel>();
				}
			}
		}

		private void AddValidations()
		{
			UserName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = AppResources.msgEmptyStringError });
			UserName.Validations.Add(new EmailRule<string> { ValidationMessage = AppResources.msgInvalidEmailError });
			Password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = AppResources.msgEmptyPassword });
			PasswordConfirmation.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = AppResources.msgEmptyPassword });

			var passwordMatchingValidationRule = new PasswordMatchingRule
			{
				PasswordToCompare = Password.Value,
				ValidationMessage = AppResources.msgPasswordsShouldMatch
			};

			Password.PropertyChanged += (sender, args) =>
			{
				if (string.Equals(args.PropertyName, nameof(Password.Value)))
				{
					passwordMatchingValidationRule.PasswordToCompare = Password.Value;
				}
			};

			PasswordConfirmation.Validations.Add(passwordMatchingValidationRule);
		}

		#endregion

	}
}
