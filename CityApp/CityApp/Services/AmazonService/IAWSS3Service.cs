using System.IO;
using System.Threading.Tasks;

namespace CityApp.Services.AmazonService
{
	public interface IAWSS3Service
    {
		#region Methods

		string ReadFileUrl(string fileName);

	    Task<bool> UploadFile(Stream file, string fileName, bool isPublic);

	    #endregion
    }
}
