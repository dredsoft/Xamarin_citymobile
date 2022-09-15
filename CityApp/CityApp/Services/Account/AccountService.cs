using System.Collections.Generic;
using System.Threading.Tasks;

using CityApp.Infrastructure.ApiManager;
using CityApp.Infrastructure.Storages;
using CityApp.Models.Models.Account;
using CityApp.Models.Models.Authorization;
using CityApp.Models.Models.Base.JsonOperations;

namespace CityApp.Services.Account
{
	public class AccountService : IAccountService
    {
		#region Fields

	    private readonly IApiManager _apiManager;

		#endregion

	    #region Constructors

	    public AccountService(IApiManager apiManager)
	    {
		    _apiManager = apiManager;
		    _apiManager.BaseAddress = ApiConstants.API_URL;

		    var userContext = SessionStorage.Instance.UserContext;

			if (userContext != null)
		    {
			    _apiManager.BearerToken = userContext.Token;
		    }
	    }

		#endregion

		#region Implementation of IAccountService

		public async Task<IJsonOperationResult<AuthorizatioUserModel>> GetTokenAsync(AuthorizationModel authorizationModel) =>
			await _apiManager.PostAsync<AuthorizatioUserModel, AuthorizationModel>
				(ApiConstants.AUTHENTICATION_URL, authorizationModel);

		public async Task<IJsonOperationResult<RegisteredUserModel>> RegisterAsync(RegistrationModel registrationModel) =>
		    await _apiManager.PostAsync<RegisteredUserModel, RegistrationModel>
			    (ApiConstants.REGISTRATION_URL, registrationModel);

		public async Task<IJsonOperationResult<string>> ResetPasswordAsync(ResetPasswordRequestModel resetPasswordModel) =>
		    await _apiManager.PostAsync<string, ResetPasswordRequestModel>
			    (ApiConstants.RESET_PASSWORD_URL, resetPasswordModel);

		public async Task<IJsonOperationResult<IEnumerable<AccountModel>>> GetAllAccountsAsync(GetAccoutsRequestModel requestModel = null) =>
		    await _apiManager.PostAsync<IEnumerable<AccountModel>, GetAccoutsRequestModel>
			    (ApiConstants.GET_ALL_ACCOUNTS_URL, requestModel);

		public async Task<IJsonOperationResult<AccountAssociationModel>> ChooseAccountAsync(AccountAssociationRequestModel associationRequestModel) =>
		    await _apiManager.PostAsync<AccountAssociationModel, AccountAssociationRequestModel>
			    (ApiConstants.ASSOCIATE_USER_URL, associationRequestModel);

		public async Task<IJsonOperationResult<IEnumerable<AccountAssociationModel>>> GetUserAccountsAsync() =>
		    await _apiManager.GetAsync<IEnumerable<AccountAssociationModel>>
				($"/api/v1.0/Users/{SessionStorage.Instance.UserContext.Id}/Accounts");

		#endregion
	}
}
