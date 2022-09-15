using CityApp.Droid.Services;
using CityApp.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(BackgroundVideoUploadService))]
namespace CityApp.Droid.Services
{
	public class BackgroundVideoUploadService : IBackgroundVideoUploadService
	{
		public void Start(string videoPath, string videoKey, string thumbnailKey)
		{
			var activity = (MainActivity)Forms.Context;

			activity.ServiceConnection.Binder.Service.UploadVideo(videoPath, videoKey, thumbnailKey);
		}
	}
}