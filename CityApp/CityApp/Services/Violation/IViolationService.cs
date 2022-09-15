using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CityApp.Models.Models.Base.JsonOperations;
using CityApp.Models.Models.Citation;
using CityApp.Models.Models.Violation;

namespace CityApp.Services.Violation
{
    public interface IViolationService
    {
		#region Methods

		Task<IJsonOperationResult<IEnumerable<ViolationModel>>> GetViolationsAsync(long accountNumber);

	    Task<IJsonOperationResult<BaseCitationModel>> SendViolationAsync(long accountNumber, CitationRequestModel citationModel);

	    Task<IJsonOperationResult<AttachmentsModel>> SendAttachmentsAsync(
		    long accountNumber,
		    Guid citationId, 
		    AttachmentsModel attachment);

	    Task SendViolationFilesToAmazonAsync(
		    string thumbnailKey, 
		    string videoKey, 
		    Stream video, 
		    Stream thumbnail,
		    bool isPublic = false);

		#endregion

		#region  Temporary methods

		IEnumerable<ViolationTypeClientModel> GetTypes();

	    IEnumerable<ViolationCategoryClientModel> GetCategories(string type);

	    IEnumerable<ViolationClientModel> GetViolationsAsync(string type, string category);

	    ViolationModel GetViolationById(Guid id);

	    #endregion
    }
}
