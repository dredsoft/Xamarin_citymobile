using CityApp.Models.Models.Base.JsonOperations;
using CityApp.Models.Models.Citation;
using System.Threading.Tasks;

namespace CityApp.Services.Citation
{
	public interface ICitationsService
    {
		Task<IJsonOperationResult<CitationsModel>> GetCitationsAsync(long accountNumber, long pageSize, long page);

		string ReadAttachmentFileFromAmazon(string key);
	}
}
