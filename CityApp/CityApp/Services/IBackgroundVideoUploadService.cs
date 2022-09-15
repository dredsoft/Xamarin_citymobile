namespace CityApp.Services
{
    public interface IBackgroundVideoUploadService
    {
	    void Start(string videoPath, string videoKey, string thumbnailKey);
    }
}
