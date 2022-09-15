using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CityApp.Infrastructure.ApiManager;
using CityApp.Infrastructure.Storages;
using CityApp.Models.Models.Base.JsonOperations;
using CityApp.Models.Models.Citation;
using CityApp.Models.Models.Violation;
using CityApp.Services.AmazonService;

namespace CityApp.Services.Violation
{
	/// <summary>
	///  Temporary service. Can be deleted when API is modified and contains separate methods for Types, Categories and Violations
	/// </summary>
	public class ViolationService : IViolationService
	{
        private static IEnumerable<ViolationModel> _data;

		#region Fields

		private readonly IApiManager _apiManager;

		private readonly IAWSS3Service _awss3Service;

		#endregion

		#region Constructors

		public ViolationService(IApiManager apiManager, IAWSS3Service awss3Service)
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

		#region Implementations of IViolationService

		public async Task<IJsonOperationResult<IEnumerable<ViolationModel>>> GetViolationsAsync(long accountNumber) =>
			await _apiManager.GetAsync<IEnumerable<ViolationModel>>($"{ApiConstants.API_VERSION_PREFIX}{accountNumber}/Violations");

		public async Task<IJsonOperationResult<BaseCitationModel>> SendViolationAsync(
			long accountNumber,
			CitationRequestModel citationModel) =>
				await _apiManager.PostAsync<BaseCitationModel, CitationRequestModel>($"{ApiConstants.API_VERSION_PREFIX}{accountNumber}/Citations", citationModel);

		public async Task<IJsonOperationResult<AttachmentsModel>> SendAttachmentsAsync(
			long accountNumber,
			Guid citationId,
			AttachmentsModel attachment) =>
				await _apiManager.PostAsync<AttachmentsModel, AttachmentsModel>($"{ApiConstants.API_VERSION_PREFIX}{accountNumber}/Citations/{citationId}/Attachment", attachment);

		public async Task SendViolationFilesToAmazonAsync(string thumbnailKey, string videoKey, Stream video, Stream thumbnail,
			bool isPublic = false)
		{
			await _awss3Service.UploadFile(thumbnail, thumbnailKey, isPublic);
			await _awss3Service.UploadFile(video, videoKey, isPublic);
		}

		#region Temporary methods

		public IEnumerable<ViolationTypeClientModel> GetTypes() => _data
			.GroupBy(model => model.TypeName)
			.Select(model => new ViolationTypeClientModel { Name = model.First().TypeName });

		public IEnumerable<ViolationCategoryClientModel> GetCategories(string type) => _data
			.Where(model => string.Equals(model.TypeName, type))
			.GroupBy(model => model.CategoryName)
			.Select(model => new ViolationCategoryClientModel { Name = model.First().CategoryName });

		public IEnumerable<ViolationClientModel> GetViolationsAsync(string type, string category) => _data
			.Where(model => string.Equals(model.TypeName, type) && string.Equals(model.CategoryName, category))
			.Select(model => new ViolationClientModel { Id = model.Id, Name = model.DisplayName });

		public ViolationModel GetViolationById(Guid id) => _data
			.First(model => Equals(model.Id, id));

		#endregion

		#endregion

		public static void Init(IEnumerable<ViolationModel> violations)
        {	
            _data = violations ?? throw new System.ArgumentNullException(nameof(violations));
        }
	}
}
