using System.IO;
using Android.App;
using Android.Content;
using Android.OS;
using CityApp.Droid.Services.GovAppService.Abstractions;
using CityApp.Services;
using CityApp.Services.AmazonService;

namespace CityApp.Droid.Services.GovAppService
{
	[Service(Name = "com.text2ticket.texttoticket.GovAppService"
		//Process = ":videouploadservice_process"
		
		)]
	public class GovAppService: Service , IGovAppService
	{
		#region Private Methods

		private readonly IAWSS3Service _awss3Service;

		private readonly IVideoService _videoService;

		#endregion

		#region Constructors

		public GovAppService()
		{
			_awss3Service = new AWSS3Service();

			_videoService = new VideoService();
		}

		#endregion

		#region Properties

		
		public IBinder Binder { get; private set; }

		#endregion

		#region Public Methods

		public override IBinder OnBind(Intent intent)
		{
			Binder = new GovAppServiceBinder(this);

			return Binder;
		}

		public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
		{
			base.OnStartCommand(intent, flags, startId);

			return StartCommandResult.ContinuationMask;
		}

		#endregion

		#region IGovAppService of Implementation

		public async void UploadVideo(string path, string videoKey, string thumbnailKey)
		{
			var compressedVidePath = await _videoService.CompressVideo(path);

			var videoStream = File.Open(compressedVidePath, FileMode.Open);

			var thumbnail = _videoService.GetVideoThumbnailFromFile(compressedVidePath);

			await _awss3Service.UploadFile(new MemoryStream(thumbnail), thumbnailKey, false);

			await _awss3Service.UploadFile(videoStream, videoKey, false);
		}

		#endregion
	}
}