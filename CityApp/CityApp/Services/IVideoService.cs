using System.Threading.Tasks;

namespace CityApp.Services
{
    public interface IVideoService
    {
	    byte[] GetVideoThumbnailFromWebUri(string url);

	    byte[] GetVideoThumbnailFromFile(string path);

		string GetPathForNewVideo();

	    byte[] ResizeThumbnail(byte[] thumbnail, int width, int height);

		Task<string> CompressVideo (string path);
    }
}
