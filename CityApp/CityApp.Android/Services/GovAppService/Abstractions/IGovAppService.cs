namespace CityApp.Droid.Services.GovAppService.Abstractions
{
	public interface IGovAppService
	{
		void UploadVideo(string path, string videoKey, string thumbnailKey);
	}
}