using System.Threading.Tasks;
using CityApp.Infrastructure.ApiManager;
using CityApp.Infrastructure.Storages;
using CityApp.Models.Models.Base.JsonOperations;
using CityApp.Models.Models.Citation;
using CityApp.Services.AmazonService;

namespace CityApp.Services.Citation
{
	public class CitationsService : ICitationsService
	{
		#region Fields

		private readonly IApiManager _apiManager;

		private readonly IAWSS3Service _awss3Service;

		#endregion

		#region Constructors

		public CitationsService(IApiManager apiManager, IAWSS3Service awss3Service)
		{
			_apiManager = apiManager;
			_awss3Service = awss3Service;

			_apiManager.BaseAddress = ApiConstants.API_URL;

			var token = SessionStorage.Instance.UserContext.Token;

			if (!string.IsNullOrEmpty(token))
			{
				_apiManager.BearerToken = token;
			}
		}

		#endregion

		#region Implementation of ICitationsService

		public async Task<IJsonOperationResult<CitationsModel>> GetCitationsAsync(long accountNumber, long pageSize, long page) =>
			await _apiManager.PostAsync<CitationsModel, object>($"{ApiConstants.API_VERSION_PREFIX}{accountNumber}/Citations/Get", new { CreatedBy = SessionStorage.Instance.UserContext.Id, PageSize = pageSize, Page = page});

		public string ReadAttachmentFileFromAmazon(string key) => _awss3Service.ReadFileUrl(key);

		#endregion
	}
}
