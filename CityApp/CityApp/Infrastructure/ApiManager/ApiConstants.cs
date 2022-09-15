namespace CityApp.Infrastructure.ApiManager
{
    public static class ApiConstants
    {
		#region Common

		public const string APPLICATION_JSON = "application/json";

	    public const string BEARER = "Bearer";

	    public const string API_VERSION_PREFIX = "/api/v1.0/";

		#endregion

		#region URLs

		public const string API_URL = "https://cityappapiqa.azurewebsites.net";

	    public static readonly string AUTHENTICATION_URL = $"{API_VERSION_PREFIX}Authentication/Login";

	    public static readonly string REGISTRATION_URL = $"{API_VERSION_PREFIX}Authentication/Register";

	    public static readonly string RESET_PASSWORD_URL = $"{API_VERSION_PREFIX}Authentication/ResetPasswordAsync";

	    public static readonly string GET_ALL_ACCOUNTS_URL = $"{API_VERSION_PREFIX}Accounts/All";

	    public static readonly string ASSOCIATE_USER_URL = $"{API_VERSION_PREFIX}Accounts/AssociateUser";

	    #endregion

		#region Prefixs

	    public static readonly string USERS_PREFIX_URL = $"{API_VERSION_PREFIX}Users";

	    public const string ACCOUNTS_PREFIX = "/Accounts";

	    public const string GET_CITATIONS = "/Citations/Get";

	    public const string GET_VIOLATIONS = "/Violations";

		#endregion

		#region Api Messages

		public const string SERVER_NOT_AVAILABLE = "Server not available";

	    public const string CHECK_CONNECTION = "Сheck your internet connection";

		#endregion
	}
}
