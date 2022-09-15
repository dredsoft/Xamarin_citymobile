using System.Collections.Generic;
using System.Threading.Tasks;
using CityApp.Models.Models.Account;
using CityApp.Models.Models.Authorization;
using CityApp.Models.Models.Base.JsonOperations;

namespace CityApp.Services.Account
{
	public interface IAccountService
	{
		#region Methods

		Task<IJsonOperationResult<AuthorizatioUserModel>> GetTokenAsync(AuthorizationModel authorizationModel);

	    Task<IJsonOperationResult<RegisteredUserModel>> RegisterAsync(RegistrationModel registrationModel);

	    Task<IJsonOperationResult<string>> ResetPasswordAsync(ResetPasswordRequestModel resetPasswordModel);

	    Task<IJsonOperationResult<IEnumerable<AccountModel>>> GetAllAccountsAsync(GetAccoutsRequestModel requestModel = null);

	    Task<IJsonOperationResult<AccountAssociationModel>> ChooseAccountAsync(AccountAssociationRequestModel associationRequestModel);

	    Task<IJsonOperationResult<IEnumerable<AccountAssociationModel>>> GetUserAccountsAsync();

		#endregion
	}
}
