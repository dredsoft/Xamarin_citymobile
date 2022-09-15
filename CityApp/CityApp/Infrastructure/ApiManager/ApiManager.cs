using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CityApp.Infrastructure.ConnectivityManager.Abstractions;
using CityApp.Models.Models.Base;
using CityApp.Models.Models.Base.JsonOperations;
using CityApp.Utilities.Logging;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CityApp.Infrastructure.ApiManager
{
    public class ApiManager : IApiManager
    {
		#region Fields

	    private readonly HttpClient _client;

	    private readonly IConnectivityManager _connectivityManager;

		#endregion

		#region Constructor

		public ApiManager(IConnectivityManager connectivityManager)
		{
			_connectivityManager = connectivityManager;
			_client = new HttpClient();
			_client.DefaultRequestHeaders.Accept.Clear();
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApiConstants.APPLICATION_JSON));
		}

		#endregion

		#region Properties

		public string BaseAddress
	    {
		    get => _client.BaseAddress.ToString();
		    set => _client.BaseAddress = new Uri(value);
	    }

	    public string BearerToken { get; set; }

	    public ILogger Logger => LogManager.GetLog();

		#endregion

		#region Implementation of ApiManager

		public async Task<IJsonOperationResult<TResponse>> GetAsync<TResponse>(string uri)
		{
			Logger.Trace();

			SetAuthorizationToken();

			var validationResult = ValidateInternetConnection<TResponse>();

			if (validationResult != null)
			{
				return validationResult;
			}

			try
			{
				var response = await _client.GetAsync(new Uri(uri));
				return await DeserializeResponse<TResponse>(response);
			}
			catch (Exception e)
			{
				Crashes.TrackError(e);

				return new JsonOperationResultGeneric<TResponse> { IsSuccessfull = false, Message = e.Message };
			}
		}

		public async Task<IJsonOperationResult<TResponse>> PostAsync<TResponse, TRequest>(string uri, TRequest request)
		{
			Logger.Trace();

			SetAuthorizationToken();

			var validationResult = ValidateInternetConnection<TResponse>();

			if (validationResult != null)
			{
				return validationResult;
			}
			
			var serializedRequst = JsonConvert.SerializeObject(request, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

			try
			{
				var response = await _client.PostAsync(new Uri(uri), new StringContent(serializedRequst, Encoding.UTF8, ApiConstants.APPLICATION_JSON));

				return await DeserializeResponse<TResponse>(response);
			}
			catch (Exception e)
			{
				Crashes.TrackError(e);

				return new JsonOperationResultGeneric<TResponse> { IsSuccessfull = false, Message = e.Message };
			}
		}

		public async Task<IJsonOperationResult<TResponse>> PostAsync<TResponse>(string uri)
		{
			Logger.Trace();

			var validationResult = ValidateInternetConnection<TResponse>();

			if (validationResult != null)
			{
				return validationResult;
			}

			SetAuthorizationToken();
			try
			{
				var response = await _client.PostAsync(new Uri(uri), null);

				return await DeserializeResponse<TResponse>(response);
			}
			catch (Exception e)
			{
				Crashes.TrackError(e);

				return new JsonOperationResultGeneric<TResponse> { IsSuccessfull = false, Message = e.Message };
			}
		}

		#endregion

		#region Public Methods

		public void SetAuthorizationToken()
		{
			Logger.Trace();

			_client.DefaultRequestHeaders.Authorization = BearerToken != null ? new AuthenticationHeaderValue(ApiConstants.BEARER, BearerToken) : null;
		}

		#endregion

		#region Private Methods

	    private IJsonOperationResult<TResponse> ValidateInternetConnection<TResponse>()
	    {
		    if (!_connectivityManager.IsConnected)
		    {
			    return new JsonOperationResultGeneric<TResponse>
			    {
				    IsSuccessfull = false,
				    Errors = new[]
				    {
					    new HttpResponseErrorModel
					    {
						    Message = ApiConstants.CHECK_CONNECTION
						},
				    }
			    };
		    }

		    return null;
	    }

		private async Task<IJsonOperationResult<TResponse>> DeserializeResponse<TResponse>(HttpResponseMessage response)
		{
			Logger.Trace();

			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();

				JsonOperationResultGeneric<TResponse> jsonOperationResultGeneric;

				try
				{
					jsonOperationResultGeneric = JsonConvert.DeserializeObject<JsonOperationResultGeneric<TResponse>>(content);
				}
				catch (Exception e)
				{
					Crashes.TrackError(e);

					return new JsonOperationResultGeneric<TResponse> { IsSuccessfull = false, Message = e.ToString() };
				}
				return jsonOperationResultGeneric;
			}

			return new JsonOperationResultGeneric<TResponse>()
			{
				IsSuccessfull = false,
				
				Data = default(TResponse)
			};
		}

		#endregion
	}
}
