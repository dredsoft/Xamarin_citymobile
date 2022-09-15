using CityApp.Droid.Services;
using CityApp.Droid.Services.GovAppService.Abstractions;
using CityApp.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(GovAppServiceManager))]
namespace CityApp.Droid.Services
{
	public class GovAppServiceManager : IGovAppServiceManager
	{
		#region Private FIelds

		private readonly IGovAppService _service;

		#endregion

		#region Properties

		#endregion

		#region Constructors

		public GovAppServiceManager()
		{
			var activity = (MainActivity)Forms.Context;

			_service = activity.ServiceConnection.Binder.Service;
		}

		#endregion

		#region IGovAppServiceManager of Implementation

		public void UploadVideo(string videoPath, string videoKey, string thumbnailKey)
		{
			_service.UploadVideo(videoPath, videoKey, thumbnailKey);
		}

		#endregion
	}
}