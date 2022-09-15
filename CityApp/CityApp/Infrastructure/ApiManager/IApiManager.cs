using System.Threading.Tasks;
using CityApp.Models.Models.Base;
using CityApp.Models.Models.Base.JsonOperations;
using CityApp.Utilities.Logging;

namespace CityApp.Infrastructure.ApiManager
{
    public interface IApiManager : ILogTarget
	{
		#region Properties

		string BaseAddress { get; set; }

		string BearerToken { get; set; }

		#endregion

		#region Methods

		Task<IJsonOperationResult<TResponse>> GetAsync<TResponse>(string uri);

		Task<IJsonOperationResult<TResponse>> PostAsync<TResponse, TRequest>(string uri, TRequest request);

		Task<IJsonOperationResult<TResponse>> PostAsync<TResponse>(string uri);

		#endregion
	}
}
